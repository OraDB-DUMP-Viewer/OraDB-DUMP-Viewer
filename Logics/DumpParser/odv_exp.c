/*****************************************************************************
    OraDB DUMP Viewer

    odv_exp.c
    Legacy EXP (conventional export) format dump file parsing

    EXP format structure:
      - 256-byte binary header (version, mode, charset)
      - DDL text (CREATE TABLE, INSERT INTO)
      - Binary record data (2-byte LE length + column data)

    Copyright (C) 2026 YANAI Taketo
 *****************************************************************************/

#include "odv_types.h"
#include <stdio.h>
#include <ctype.h>

/*---------------------------------------------------------------------------
    Constants
 ---------------------------------------------------------------------------*/
#define EXP_HEADER_SIZE     0x100
#define EXP_WORD_BUF_SIZE   8192
#define EXP_DDL_BUF_SIZE    262144   /* 256KB for DDL text */

/*---------------------------------------------------------------------------
    Forward declarations
 ---------------------------------------------------------------------------*/
static int parse_exp_header(ODV_SESSION *s, FILE *fp);
static int parse_exp_ddl_and_data(ODV_SESSION *s, FILE *fp, int list_only);
static int parse_column_type(const char *type_str, ODV_COLUMN *col);
static void trim_right(char *str);

/*---------------------------------------------------------------------------
    Helper: case-insensitive prefix match
 ---------------------------------------------------------------------------*/
static int starts_with_ci(const char *str, const char *prefix)
{
    while (*prefix) {
        if (toupper((unsigned char)*str) != toupper((unsigned char)*prefix))
            return 0;
        str++;
        prefix++;
    }
    return 1;
}

/*---------------------------------------------------------------------------
    Helper: skip whitespace
 ---------------------------------------------------------------------------*/
static const char *skip_ws(const char *p)
{
    while (*p && ((unsigned char)*p <= ' ')) p++;
    return p;
}

/*---------------------------------------------------------------------------
    Helper: extract quoted identifier or bare word
    Returns pointer past the extracted token.
 ---------------------------------------------------------------------------*/
static const char *extract_identifier(const char *p, char *out, int max_len)
{
    int len = 0;
    p = skip_ws(p);

    if (*p == '"') {
        p++; /* skip opening quote */
        while (*p && *p != '"' && len < max_len - 1) {
            out[len++] = *p++;
        }
        if (*p == '"') p++; /* skip closing quote */
    } else {
        while (*p && *p != ' ' && *p != ',' && *p != '(' && *p != ')'
               && *p != '\t' && *p != '\n' && *p != '\r'
               && len < max_len - 1) {
            out[len++] = *p++;
        }
    }
    out[len] = '\0';
    return p;
}

/*---------------------------------------------------------------------------
    parse_exp_header

    Reads the 256-byte EXP header and extracts:
    - Oracle version
    - Export mode (TABLE/USER/DATABASE)
    - Character sets (client, database, NLS)
 ---------------------------------------------------------------------------*/
static int parse_exp_header(ODV_SESSION *s, FILE *fp)
{
    unsigned char hdr[EXP_HEADER_SIZE];
    int i;

    odv_fseek(fp, 0, SEEK_SET);
    if (fread(hdr, 1, EXP_HEADER_SIZE, fp) != EXP_HEADER_SIZE) {
        odv_strcpy(s->last_error, "Cannot read EXP header", ODV_MSG_LEN);
        return ODV_ERROR_FREAD;
    }

    /* Extract Oracle version from near offset 6 */
    s->exp_state.oracle_version = 0;
    for (i = 3; i < 20; i++) {
        if (hdr[i] == 'V' || hdr[i] == 'v') {
            s->exp_state.oracle_version = atoi((char *)&hdr[i + 1]);
            break;
        }
    }

    /* Extract export mode from around offset 0x20-0x40 */
    s->exp_state.exp_mode = EXP_MODE_TABLE;
    for (i = 16; i < 64; i++) {
        if (hdr[i] == 'R' && i + 6 < EXP_HEADER_SIZE) {
            if (memcmp(&hdr[i], "RTABLES", 7) == 0) {
                s->exp_state.exp_mode = EXP_MODE_TABLE;
                break;
            } else if (memcmp(&hdr[i], "RUSERS", 6) == 0) {
                s->exp_state.exp_mode = EXP_MODE_USER;
                break;
            } else if (memcmp(&hdr[i], "RENTIRE", 7) == 0) {
                s->exp_state.exp_mode = EXP_MODE_DATABASE;
                break;
            }
        }
    }

    /* Extract character set from header (~offset 96-127) */
    /* Three charset indicators at specific positions */
    for (i = 96; i < 128; i++) {
        unsigned char b = hdr[i];
        if (b >= 0x60 && b <= 0x6F) {
            s->table.dump_charset = CHARSET_UTF8;
            break;
        } else if (b >= 0x40 && b <= 0x4F) {
            s->table.dump_charset = CHARSET_SJIS;
            break;
        } else if (b >= 0x30 && b <= 0x3F) {
            s->table.dump_charset = CHARSET_EUC;
            break;
        }
    }

    /* Check for direct export mode: look for "D\n" in first 32 bytes */
    for (i = 0; i < 31; i++) {
        if (hdr[i] == 'D' && hdr[i + 1] == 0x0A) {
            s->dump_type = DUMP_EXP_DIRECT;
            break;
        }
    }

    s->exp_state.header_size = EXP_HEADER_SIZE;
    return ODV_OK;
}

/*---------------------------------------------------------------------------
    parse_create_table

    Parses a CREATE TABLE DDL statement to extract table name and columns.
    Returns 1 if a table was found, 0 otherwise.
 ---------------------------------------------------------------------------*/
static int parse_create_table(ODV_SESSION *s, const char *ddl)
{
    const char *p;
    char schema[ODV_OBJNAME_LEN + 1] = {0};
    char table_name[ODV_OBJNAME_LEN + 1] = {0};
    char col_name[ODV_OBJNAME_LEN + 1];
    char type_str[256];
    int col_count = 0;
    int paren_depth;

    /* Find "CREATE TABLE" */
    p = ddl;
    while (*p) {
        if (starts_with_ci(p, "CREATE") && ((unsigned char)p[6] <= ' ')) {
            p += 6;
            p = skip_ws(p);
            if (starts_with_ci(p, "TABLE") && ((unsigned char)p[5] <= ' ')) {
                p += 5;
                break;
            }
        }
        p++;
    }
    if (!*p) return 0;

    /* Extract table name: "schema"."table" or "table" */
    p = extract_identifier(p, table_name, ODV_OBJNAME_LEN);
    p = skip_ws(p);

    if (*p == '.') {
        /* schema.table format */
        odv_strcpy(schema, table_name, ODV_OBJNAME_LEN);
        p++;
        p = extract_identifier(p, table_name, ODV_OBJNAME_LEN);
        p = skip_ws(p);
    }

    /* Store table info */
    memset(&s->table, 0, sizeof(ODV_TABLE));
    odv_strcpy(s->table.schema, schema, ODV_OBJNAME_LEN);
    odv_strcpy(s->table.name, table_name, ODV_OBJNAME_LEN);
    s->table.dump_charset = s->dump_charset;
    s->table.col_count = 0;
    s->table.lob_col_count = 0;

    /* Expect opening parenthesis */
    if (*p != '(') return 0;
    p++;

    /* Parse column definitions */
    while (*p && col_count < ODV_MAX_COLUMNS) {
        int type_len = 0;

        p = skip_ws(p);
        if (*p == ')') break;

        /* Extract column name */
        col_name[0] = '\0';
        p = extract_identifier(p, col_name, ODV_OBJNAME_LEN);
        if (col_name[0] == '\0') break;

        /* Skip constraint keywords that aren't column definitions */
        if (starts_with_ci(col_name, "CONSTRAINT") ||
            starts_with_ci(col_name, "PRIMARY") ||
            starts_with_ci(col_name, "UNIQUE") ||
            starts_with_ci(col_name, "FOREIGN") ||
            starts_with_ci(col_name, "CHECK")) {
            /* Skip to next comma at same depth or closing paren */
            paren_depth = 0;
            while (*p) {
                if (*p == '(') paren_depth++;
                else if (*p == ')') {
                    if (paren_depth == 0) break;
                    paren_depth--;
                }
                else if (*p == ',' && paren_depth == 0) { p++; break; }
                p++;
            }
            continue;
        }

        p = skip_ws(p);

        /* Extract type string (up to comma, closing paren, or constraint) */
        type_str[0] = '\0';
        type_len = 0;
        paren_depth = 0;

        while (*p && type_len < 250) {
            if (*p == '(') {
                paren_depth++;
                type_str[type_len++] = *p++;
            } else if (*p == ')') {
                if (paren_depth > 0) {
                    paren_depth--;
                    type_str[type_len++] = *p++;
                } else {
                    break; /* End of column list */
                }
            } else if (*p == ',' && paren_depth == 0) {
                p++; /* skip comma */
                break;
            } else if (starts_with_ci(p, "NOT ") || starts_with_ci(p, "DEFAULT ") ||
                       starts_with_ci(p, "CONSTRAINT ")) {
                /* Stop at constraint keywords */
                /* Skip to next comma or closing paren */
                while (*p && *p != ',' && *p != ')') {
                    if (*p == '(') {
                        /* Skip nested parens in DEFAULT expressions */
                        int d = 1;
                        p++;
                        while (*p && d > 0) {
                            if (*p == '(') d++;
                            else if (*p == ')') d--;
                            p++;
                        }
                    } else {
                        p++;
                    }
                }
                if (*p == ',') p++;
                break;
            } else {
                type_str[type_len++] = *p++;
            }
        }
        type_str[type_len] = '\0';
        trim_right(type_str);

        /* Store column */
        if (col_name[0] != '\0' && type_str[0] != '\0') {
            ODV_COLUMN *col = &s->table.columns[col_count];
            memset(col, 0, sizeof(ODV_COLUMN));
            odv_strcpy(col->name, col_name, ODV_OBJNAME_LEN);
            parse_column_type(type_str, col);
            col_count++;
        }
    }

    s->table.col_count = col_count;

    /* Count LOB columns */
    {
        int i;
        for (i = 0; i < col_count; i++) {
            int t = s->table.columns[i].type;
            if (t == COL_BLOB || t == COL_CLOB || t == COL_NCLOB ||
                t == COL_BFILE || t == COL_LONG_RAW || t == COL_LONG) {
                s->table.lob_col_count++;
            }
        }
    }

    return 1;
}

/*---------------------------------------------------------------------------
    parse_column_type

    Parses a type string like "VARCHAR2(100)" into ODV_COLUMN fields.
 ---------------------------------------------------------------------------*/
static int parse_column_type(const char *type_str, ODV_COLUMN *col)
{
    char upper[256];
    int i;
    const char *p;

    /* Copy and uppercase for matching */
    for (i = 0; type_str[i] && i < 255; i++) {
        upper[i] = (char)toupper((unsigned char)type_str[i]);
    }
    upper[i] = '\0';

    /* Store original type string */
    odv_strcpy(col->type_str, type_str, 63);

    /* Match type and extract parameters */
    if (starts_with_ci(upper, "VARCHAR2") || starts_with_ci(upper, "VARCHAR")) {
        col->type = COL_VARCHAR;
    } else if (starts_with_ci(upper, "NVARCHAR2") || starts_with_ci(upper, "NVARCHAR")) {
        col->type = COL_NVARCHAR;
    } else if (starts_with_ci(upper, "NCHAR")) {
        col->type = COL_NCHAR;
    } else if (starts_with_ci(upper, "CHAR")) {
        col->type = COL_CHAR;
    } else if (starts_with_ci(upper, "NUMBER")) {
        col->type = COL_NUMBER;
    } else if (starts_with_ci(upper, "FLOAT")) {
        col->type = COL_FLOAT;
    } else if (starts_with_ci(upper, "BINARY_FLOAT")) {
        col->type = COL_BIN_FLOAT;
    } else if (starts_with_ci(upper, "BINARY_DOUBLE")) {
        col->type = COL_BIN_DOUBLE;
    } else if (starts_with_ci(upper, "TIMESTAMP") && strstr(upper, "LOCAL")) {
        col->type = COL_TIMESTAMP_LTZ;
    } else if (starts_with_ci(upper, "TIMESTAMP") && strstr(upper, "TIME ZONE")) {
        col->type = COL_TIMESTAMP_TZ;
    } else if (starts_with_ci(upper, "TIMESTAMP")) {
        col->type = COL_TIMESTAMP;
    } else if (starts_with_ci(upper, "DATE")) {
        col->type = COL_DATE;
    } else if (starts_with_ci(upper, "INTERVAL") && strstr(upper, "YEAR")) {
        col->type = COL_INTERVAL_YM;
    } else if (starts_with_ci(upper, "INTERVAL") && strstr(upper, "DAY")) {
        col->type = COL_INTERVAL_DS;
    } else if (starts_with_ci(upper, "LONG RAW")) {
        col->type = COL_LONG_RAW;
    } else if (starts_with_ci(upper, "LONG")) {
        col->type = COL_LONG;
    } else if (starts_with_ci(upper, "RAW")) {
        col->type = COL_RAW;
    } else if (starts_with_ci(upper, "BLOB")) {
        col->type = COL_BLOB;
    } else if (starts_with_ci(upper, "NCLOB")) {
        col->type = COL_NCLOB;
    } else if (starts_with_ci(upper, "CLOB")) {
        col->type = COL_CLOB;
    } else if (starts_with_ci(upper, "BFILE")) {
        col->type = COL_BFILE;
    } else if (starts_with_ci(upper, "XMLTYPE")) {
        col->type = COL_XMLTYPE;
    } else if (starts_with_ci(upper, "ROWID") || starts_with_ci(upper, "UROWID")) {
        col->type = COL_ROWID;
    } else {
        col->type = COL_VARCHAR;  /* default fallback */
    }

    /* Extract length/precision/scale from parentheses */
    p = strchr(upper, '(');
    if (p) {
        p++;
        col->length = atoi(p);

        /* For NUMBER(p,s) */
        p = strchr(p, ',');
        if (p) {
            p++;
            col->scale = atoi(p);
            col->precision = col->length;
        }
    }

    /* Default lengths for types without explicit size */
    if (col->length == 0) {
        switch (col->type) {
        case COL_DATE:          col->length = 7; break;
        case COL_TIMESTAMP:     col->length = 11; break;
        case COL_TIMESTAMP_TZ:  col->length = 13; break;
        case COL_TIMESTAMP_LTZ: col->length = 11; break;
        case COL_BIN_FLOAT:     col->length = 4; break;
        case COL_BIN_DOUBLE:    col->length = 8; break;
        case COL_ROWID:         col->length = 18; break;
        default: break;
        }
    }

    return ODV_OK;
}

/*---------------------------------------------------------------------------
    notify_exp_table

    Notifies via table callback and adds to table list.
 ---------------------------------------------------------------------------*/
static void notify_exp_table(ODV_SESSION *s)
{
    const char *col_names[ODV_MAX_COLUMNS];
    const char *col_types[ODV_MAX_COLUMNS];
    int i;

    if (s->table_count < ODV_MAX_TABLES) {
        ODV_TABLE_ENTRY *e = &s->table_list[s->table_count];
        odv_strcpy(e->schema, s->table.schema, ODV_OBJNAME_LEN);
        odv_strcpy(e->name, s->table.name, ODV_OBJNAME_LEN);
        e->type = 0;
        e->col_count = s->table.col_count;
        e->row_count = 0;
        s->table_count++;
    }

    if (s->table_cb) {
        for (i = 0; i < s->table.col_count && i < ODV_MAX_COLUMNS; i++) {
            col_names[i] = s->table.columns[i].name;
            col_types[i] = s->table.columns[i].type_str;
        }
        s->table_cb(
            s->table.schema,
            s->table.name,
            s->table.col_count,
            col_names,
            col_types,
            0,
            s->table_ud
        );
    }
}

/*---------------------------------------------------------------------------
    decode_exp_column

    Decodes a single EXP column value from binary data.
    Stores result as string in the record value.
 ---------------------------------------------------------------------------*/
static int decode_exp_column(ODV_SESSION *s, int col_idx,
                             const unsigned char *data, int data_len)
{
    ODV_COLUMN *col;
    ODV_VALUE *val;
    char tmp[1024];
    int rc;

    if (col_idx >= s->table.col_count) return ODV_OK;

    col = &s->table.columns[col_idx];
    val = &s->record.values[col_idx];

    switch (col->type) {
    case COL_NUMBER:
    case COL_FLOAT:
        rc = decode_oracle_number(data, data_len, tmp, sizeof(tmp));
        if (rc == ODV_OK) {
            set_value_string(val, tmp, (int)strlen(tmp));
        } else {
            set_value_null(val);
        }
        break;

    case COL_DATE:
        rc = decode_oracle_date(data, data_len, tmp, sizeof(tmp), DATE_FMT_SLASH);
        if (rc == ODV_OK) {
            set_value_string(val, tmp, (int)strlen(tmp));
        } else {
            set_value_null(val);
        }
        break;

    case COL_TIMESTAMP:
    case COL_TIMESTAMP_TZ:
    case COL_TIMESTAMP_LTZ:
        rc = decode_oracle_timestamp(data, data_len, tmp, sizeof(tmp), DATE_FMT_SLASH);
        if (rc == ODV_OK) {
            set_value_string(val, tmp, (int)strlen(tmp));
        } else {
            set_value_null(val);
        }
        break;

    case COL_BIN_FLOAT:
        rc = decode_binary_float(data, tmp, sizeof(tmp));
        if (rc == ODV_OK) {
            set_value_string(val, tmp, (int)strlen(tmp));
        } else {
            set_value_null(val);
        }
        break;

    case COL_BIN_DOUBLE:
        rc = decode_binary_double(data, tmp, sizeof(tmp));
        if (rc == ODV_OK) {
            set_value_string(val, tmp, (int)strlen(tmp));
        } else {
            set_value_null(val);
        }
        break;

    case COL_RAW:
    case COL_ROWID: {
        /* Hex output: "0x" + hex bytes */
        int needed = data_len * 2 + 3;
        rc = ensure_value_buf(val, needed);
        if (rc == ODV_OK) {
            int j, pos = 0;
            val->data[pos++] = '0';
            val->data[pos++] = 'x';
            for (j = 0; j < data_len; j++) {
                static const char hex[] = "0123456789ABCDEF";
                val->data[pos++] = hex[(data[j] >> 4) & 0x0F];
                val->data[pos++] = hex[data[j] & 0x0F];
            }
            val->data[pos] = '\0';
            val->data_len = pos;
            val->is_null = 0;
        }
        break;
    }

    case COL_BLOB:
    case COL_LONG_RAW: {
        /* LOB: placeholder */
        const char *ph = "%BLOB%";
        set_value_string(val, ph, (int)strlen(ph));
        break;
    }

    case COL_CLOB:
    case COL_LONG: {
        /* CLOB: store as string with charset conversion if needed */
        if (s->table.dump_charset != s->out_charset &&
            s->table.dump_charset != CHARSET_UNKNOWN) {
            int out_len = 0;
            rc = ensure_value_buf(val, data_len * 4 + 1);
            if (rc == ODV_OK) {
                rc = convert_charset((const char *)data, data_len,
                                     s->table.dump_charset,
                                     (char *)val->data, val->buf_size,
                                     s->out_charset, &out_len);
                if (rc == ODV_OK) {
                    val->data[out_len] = '\0';
                    val->data_len = out_len;
                    val->is_null = 0;
                } else {
                    /* Fallback: copy raw */
                    set_value_string(val, (const char *)data, data_len);
                }
            }
        } else {
            set_value_string(val, (const char *)data, data_len);
        }
        break;
    }

    case COL_NCLOB: {
        const char *ph = "%NCLOB%";
        set_value_string(val, ph, (int)strlen(ph));
        break;
    }

    case COL_BFILE: {
        const char *ph = "%BFILE%";
        set_value_string(val, ph, (int)strlen(ph));
        break;
    }

    default:
        /* String types: CHAR, VARCHAR, NCHAR, NVARCHAR, etc. */
        if (s->table.dump_charset != s->out_charset &&
            s->table.dump_charset != CHARSET_UNKNOWN) {
            int out_len = 0;
            rc = ensure_value_buf(val, data_len * 4 + 1);
            if (rc == ODV_OK) {
                rc = convert_charset((const char *)data, data_len,
                                     s->table.dump_charset,
                                     (char *)val->data, val->buf_size,
                                     s->out_charset, &out_len);
                if (rc == ODV_OK) {
                    val->data[out_len] = '\0';
                    val->data_len = out_len;
                    val->is_null = 0;
                } else {
                    set_value_string(val, (const char *)data, data_len);
                }
            }
        } else {
            set_value_string(val, (const char *)data, data_len);
        }

        /* Trim trailing spaces for CHAR types */
        if ((col->type == COL_CHAR || col->type == COL_NCHAR) &&
            val->data && val->data_len > 0) {
            while (val->data_len > 0 && val->data[val->data_len - 1] == ' ') {
                val->data_len--;
            }
            val->data[val->data_len] = '\0';
        }
        break;
    }

    return ODV_OK;
}

/*---------------------------------------------------------------------------
    parse_exp_records

    Reads binary records from EXP dump.
    Each column: 2-byte LE length + data
    Record end: 0x0000
    Table end: 0xFFFF
    NULL: 0xFFFE
 ---------------------------------------------------------------------------*/
static int parse_exp_records(ODV_SESSION *s, FILE *fp, int64_t data_start)
{
    unsigned char len_buf[2];
    unsigned char *col_buf = NULL;
    int col_buf_size = 0;
    int col_idx = 0;
    int col_len;
    int rc = ODV_OK;
    int64_t row_count = 0;

    /* Seek to data start */
    odv_fseek(fp, data_start, SEEK_SET);

    /* Ensure record can hold all columns */
    if (s->table.col_count > s->record.max_columns) {
        /* grow_record is static in odv_record.c, use realloc approach */
        ODV_VALUE *new_vals;
        int new_max = s->table.col_count + 64;
        new_vals = (ODV_VALUE *)realloc(s->record.values,
                                        new_max * sizeof(ODV_VALUE));
        if (!new_vals) return ODV_ERROR_MALLOC;
        memset(new_vals + s->record.max_columns, 0,
               (new_max - s->record.max_columns) * sizeof(ODV_VALUE));
        s->record.values = new_vals;
        s->record.max_columns = new_max;
    }

    /* Allocate column data buffer */
    col_buf_size = ODV_EXP_READ_BUF_LEN;
    col_buf = (unsigned char *)malloc(col_buf_size);
    if (!col_buf) return ODV_ERROR_MALLOC;

    reset_record(&s->record);
    col_idx = 0;

    while (!s->cancelled) {
        /* Read 2-byte length prefix */
        if (fread(len_buf, 1, 2, fp) != 2) {
            break; /* EOF */
        }

        col_len = (int)len_buf[0] | ((int)len_buf[1] << 8);

        /* Special markers */
        if (col_len == 0x0000) {
            /* Record end */
            if (col_idx > 0) {
                s->record.col_count = col_idx;
                deliver_row(s);
                row_count++;
            }
            reset_record(&s->record);
            col_idx = 0;
            continue;
        }

        if (col_len == 0xFFFF) {
            /* Table data end */
            break;
        }

        if (col_len == 0xFFFE) {
            /* NULL column */
            if (col_idx < s->table.col_count) {
                set_value_null(&s->record.values[col_idx]);
            }
            col_idx++;
            continue;
        }

        /* Handle large data: 0xFF00 means 4-byte length follows */
        if (col_len == 0xFF00) {
            unsigned char big_len[4];
            if (fread(big_len, 1, 4, fp) != 4) break;
            col_len = (int)big_len[0]
                    | ((int)big_len[1] << 8)
                    | ((int)big_len[2] << 16)
                    | ((int)big_len[3] << 24);
        }

        /* Ensure buffer is large enough */
        if (col_len > col_buf_size) {
            unsigned char *new_buf = (unsigned char *)realloc(col_buf, col_len + 1);
            if (!new_buf) { rc = ODV_ERROR_MALLOC; break; }
            col_buf = new_buf;
            col_buf_size = col_len + 1;
        }

        /* Read column data */
        if (col_len > 0) {
            if ((int)fread(col_buf, 1, col_len, fp) != col_len) {
                break; /* Truncated */
            }
        }

        /* Decode and store */
        if (col_idx < s->table.col_count) {
            decode_exp_column(s, col_idx, col_buf, col_len);
        }
        col_idx++;
    }

    /* Update row count in table list */
    if (s->table_count > 0) {
        s->table_list[s->table_count - 1].row_count = row_count;
    }

    free(col_buf);

    if (s->cancelled) return ODV_ERROR_CANCELLED;
    return rc;
}

/*---------------------------------------------------------------------------
    parse_exp_ddl_and_data

    Reads EXP file after header, searching for CREATE TABLE + INSERT INTO
    patterns, then parsing record data.
 ---------------------------------------------------------------------------*/
static int parse_exp_ddl_and_data(ODV_SESSION *s, FILE *fp, int list_only)
{
    char *ddl_buf;
    int ddl_len = 0;
    int ddl_buf_size;
    unsigned char read_buf[4096];
    int read_len;
    int in_ddl = 0;     /* 1 = accumulating DDL text */
    int found_insert = 0;
    int64_t data_start = 0;
    int rc = ODV_OK;
    int i;

    /* Allocate DDL buffer */
    ddl_buf_size = EXP_DDL_BUF_SIZE;
    ddl_buf = (char *)malloc(ddl_buf_size);
    if (!ddl_buf) return ODV_ERROR_MALLOC;

    /* Start reading after header */
    odv_fseek(fp, EXP_HEADER_SIZE, SEEK_SET);

    /*
     * EXP files contain ASCII DDL text mixed with binary data.
     * Strategy: read blocks, scan for CREATE TABLE / INSERT INTO / CONNECT
     * as text, then switch to binary record parsing at INSERT INTO boundary.
     *
     * EXP DDL text is typically terminated by INSERT INTO statement,
     * after which binary record data begins.
     */

    ddl_len = 0;
    in_ddl = 0;

    /* Read entire file into DDL buffer to parse text sections */
    /* For EXP, DDL is relatively small; we read until we find binary data */
    while (!s->cancelled) {
        read_len = (int)fread(read_buf, 1, sizeof(read_buf), fp);
        if (read_len <= 0) break;

        for (i = 0; i < read_len; i++) {
            unsigned char c = read_buf[i];

            /* EXP DDL is printable ASCII text.
               Binary data sections have many non-printable bytes. */
            if (ddl_len < ddl_buf_size - 1) {
                ddl_buf[ddl_len++] = (char)c;
            }
        }
    }
    ddl_buf[ddl_len] = '\0';

    /*
     * Now parse the DDL buffer for CREATE TABLE and INSERT INTO patterns.
     * In EXP format:
     *   - CONNECT schema; indicates schema switch
     *   - CREATE TABLE defines table structure
     *   - INSERT INTO indicates data follows (binary records)
     */
    {
        char *pos = ddl_buf;
        char *create_start;
        char *insert_pos;
        char current_schema[ODV_OBJNAME_LEN + 1] = {0};

        while (*pos && !s->cancelled) {
            pos = (char *)skip_ws(pos);
            if (!*pos) break;

            /* Look for CONNECT schema */
            if (starts_with_ci(pos, "CONNECT ")) {
                char sch[ODV_OBJNAME_LEN + 1];
                const char *ep;
                pos += 8;
                ep = extract_identifier(pos, sch, ODV_OBJNAME_LEN);
                if (sch[0] != '\0') {
                    odv_strcpy(current_schema, sch, ODV_OBJNAME_LEN);
                }
                pos = (char *)ep;
                /* Skip to end of statement (semicolon or newline) */
                while (*pos && *pos != ';' && *pos != '\n') pos++;
                if (*pos) pos++;
                continue;
            }

            /* Look for CREATE TABLE */
            if (starts_with_ci(pos, "CREATE ")) {
                create_start = pos;

                /* Find the end of CREATE TABLE ... (...) */
                /* Look for matching INSERT INTO or next CREATE */
                insert_pos = NULL;
                {
                    char *scan = pos + 7;
                    while (*scan) {
                        if (starts_with_ci(scan, "INSERT INTO")) {
                            insert_pos = scan;
                            break;
                        }
                        /* Also stop at next CREATE that's not part of this DDL */
                        if (scan != pos && starts_with_ci(scan, "CREATE ") &&
                            starts_with_ci(skip_ws(scan + 7), "TABLE")) {
                            break;
                        }
                        scan++;
                    }
                }

                /* Parse the CREATE TABLE */
                if (parse_create_table(s, create_start)) {
                    /* If no schema in DDL, use CONNECT schema */
                    if (s->table.schema[0] == '\0' && current_schema[0] != '\0') {
                        odv_strcpy(s->table.schema, current_schema, ODV_OBJNAME_LEN);
                    }

                    notify_exp_table(s);

                    /* If not list_only, find INSERT INTO and parse records */
                    if (!list_only && insert_pos) {
                        /* Data starts after INSERT INTO ... VALUES ( */
                        /* The binary data starts right after the INSERT INTO marker.
                           In EXP format, the INSERT INTO is followed by binary records. */
                        int64_t offset_in_buf = insert_pos - ddl_buf;
                        char *vals;

                        /* Skip "INSERT INTO ..." to find start of binary data */
                        vals = insert_pos;
                        /* Skip to end of the INSERT INTO text line */
                        while (*vals && *vals != '\n' && *vals != '\r') vals++;
                        while (*vals == '\n' || *vals == '\r') vals++;

                        /* Calculate file offset for binary data */
                        data_start = EXP_HEADER_SIZE + (vals - ddl_buf);

                        /* Parse records */
                        rc = parse_exp_records(s, fp, data_start);
                        if (rc != ODV_OK && rc != ODV_ERROR_CANCELLED) {
                            /* Non-fatal: continue to next table */
                        }
                    }
                }

                /* Advance past this CREATE TABLE block */
                if (insert_pos) {
                    pos = insert_pos;
                    /* Skip past INSERT INTO and its data */
                    while (*pos && !starts_with_ci(pos, "CONNECT ") &&
                           !starts_with_ci(pos, "CREATE ")) {
                        pos++;
                    }
                } else {
                    /* Skip the CREATE TABLE statement */
                    while (*pos && *pos != ';') pos++;
                    if (*pos) pos++;
                }
                continue;
            }

            /* Skip other content */
            while (*pos && *pos != '\n') pos++;
            if (*pos) pos++;
        }
    }

    free(ddl_buf);

    if (s->cancelled) return ODV_ERROR_CANCELLED;
    return rc;
}

/*---------------------------------------------------------------------------
    parse_exp_dump

    Main entry point for EXP dump parsing.
 ---------------------------------------------------------------------------*/
int parse_exp_dump(ODV_SESSION *s, int list_only)
{
    FILE *fp;
    int rc;

    if (!s) return ODV_ERROR_INVALID_ARG;

    fp = fopen(s->dump_path, "rb");
    if (!fp) {
        odv_strcpy(s->last_error, "Cannot open dump file", ODV_MSG_LEN);
        return ODV_ERROR_FOPEN;
    }

    /* Parse header */
    rc = parse_exp_header(s, fp);
    if (rc != ODV_OK) {
        fclose(fp);
        return rc;
    }

    /* Parse DDL and data */
    rc = parse_exp_ddl_and_data(s, fp, list_only);

    fclose(fp);
    return rc;
}

/*---------------------------------------------------------------------------
    trim_right - trim trailing whitespace
 ---------------------------------------------------------------------------*/
static void trim_right(char *str)
{
    int len;
    if (!str) return;
    len = (int)strlen(str);
    while (len > 0 && ((unsigned char)str[len - 1] <= ' ')) {
        len--;
    }
    str[len] = '\0';
}
