/*****************************************************************************
    OraDB DUMP Viewer

    odv_record.c
    Record buffer management and row delivery

    Copyright (C) 2026 YANAI Taketo
 *****************************************************************************/

#include "odv_types.h"

/*---------------------------------------------------------------------------
    Record buffer management
 ---------------------------------------------------------------------------*/

int init_record(ODV_RECORD *rec, int max_cols)
{
    if (!rec) return ODV_ERROR_INVALID_ARG;
    if (max_cols <= 0) max_cols = 256;

    rec->values = (ODV_VALUE *)calloc(max_cols, sizeof(ODV_VALUE));
    if (!rec->values) return ODV_ERROR_MALLOC;

    rec->max_columns = max_cols;
    rec->col_count = 0;
    return ODV_OK;
}

void free_record(ODV_RECORD *rec)
{
    int i;
    if (!rec || !rec->values) return;

    for (i = 0; i < rec->max_columns; i++) {
        if (rec->values[i].data) {
            free(rec->values[i].data);
            rec->values[i].data = NULL;
        }
    }
    free(rec->values);
    rec->values = NULL;
    rec->max_columns = 0;
    rec->col_count = 0;
}

void reset_record(ODV_RECORD *rec)
{
    int i;
    if (!rec || !rec->values) return;

    for (i = 0; i < rec->col_count; i++) {
        rec->values[i].is_null = 1;
        rec->values[i].data_len = 0;
        /* Keep buffer allocated for reuse */
    }
    rec->col_count = 0;
}

/* Grow record if needed (e.g., table has more than 256 columns) */
static int grow_record(ODV_RECORD *rec, int needed)
{
    ODV_VALUE *new_vals;
    int new_max;

    if (needed <= rec->max_columns) return ODV_OK;

    new_max = needed + 64; /* grow with some slack */
    new_vals = (ODV_VALUE *)realloc(rec->values, new_max * sizeof(ODV_VALUE));
    if (!new_vals) return ODV_ERROR_MALLOC;

    /* Zero-init new entries */
    memset(new_vals + rec->max_columns, 0,
           (new_max - rec->max_columns) * sizeof(ODV_VALUE));

    rec->values = new_vals;
    rec->max_columns = new_max;
    return ODV_OK;
}

/*---------------------------------------------------------------------------
    Value helpers
 ---------------------------------------------------------------------------*/

int ensure_value_buf(ODV_VALUE *v, int needed)
{
    if (!v) return ODV_ERROR_INVALID_ARG;

    if (v->buf_size >= needed) return ODV_OK;

    /* Round up to next power-of-2-ish size for reuse efficiency */
    int new_size = (needed < 256) ? 256 : (needed + 255) & ~255;

    if (v->data) {
        unsigned char *p = (unsigned char *)realloc(v->data, new_size);
        if (!p) return ODV_ERROR_MALLOC;
        v->data = p;
    } else {
        v->data = (unsigned char *)malloc(new_size);
        if (!v->data) return ODV_ERROR_MALLOC;
    }
    v->buf_size = new_size;
    return ODV_OK;
}

int set_value_null(ODV_VALUE *v)
{
    if (!v) return ODV_ERROR_INVALID_ARG;
    v->is_null = 1;
    v->data_len = 0;
    v->type = COL_NULL;
    return ODV_OK;
}

int set_value_string(ODV_VALUE *v, const char *str, int len)
{
    int rc;
    if (!v) return ODV_ERROR_INVALID_ARG;

    if (!str || len <= 0) {
        v->is_null = 1;
        v->data_len = 0;
        return ODV_OK;
    }

    rc = ensure_value_buf(v, len + 1);
    if (rc != ODV_OK) return rc;

    memcpy(v->data, str, len);
    v->data[len] = '\0';
    v->data_len = len;
    v->is_null = 0;
    return ODV_OK;
}

/*---------------------------------------------------------------------------
    Row delivery to VB.NET callback
 ---------------------------------------------------------------------------*/

int deliver_row(ODV_SESSION *s)
{
    const char *col_names[ODV_MAX_COLUMNS];
    const char *col_values[ODV_MAX_COLUMNS];
    int i;
    static const char empty_str[] = "";

    if (!s || !s->row_cb) return ODV_OK;

    for (i = 0; i < s->table.col_count && i < ODV_MAX_COLUMNS; i++) {
        col_names[i] = s->table.columns[i].name;

        if (s->record.values[i].is_null || !s->record.values[i].data) {
            col_values[i] = empty_str;
        } else {
            col_values[i] = (const char *)s->record.values[i].data;
        }
    }

    s->row_cb(
        s->table.schema,
        s->table.name,
        s->table.col_count,
        col_names,
        col_values,
        s->row_ud
    );

    s->total_rows++;

    /* Progress callback every 500 rows */
    if (s->progress_cb && (s->total_rows % 500) == 0) {
        s->progress_cb(s->total_rows, s->table.name, s->progress_ud);
    }

    return ODV_OK;
}
