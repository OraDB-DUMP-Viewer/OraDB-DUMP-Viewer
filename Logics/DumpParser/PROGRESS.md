# OraDB DUMP Parser - Implementation Progress

## Phase 1: DLL Skeleton - COMPLETED
- [x] odv_types.h - Internal type definitions (ODV_SESSION, ODV_TABLE, ODV_RECORD etc.)
- [x] odv_api.h - DLL public API header (13 exported functions)
- [x] odv_api.c - Session lifecycle + operation dispatch
- [x] odv_record.c - Record buffer management (init/free/reset/deliver)
- [x] odv_detect.c - Dump format detection (EXP/EXPDP/compressed)
- [x] unistd.h - POSIX compatibility stub
- [x] OraDB_DumpParser.vcxproj - VS 2022 C++ project
- [x] build_dll.bat / _build.cmd - Build scripts
- [x] Stub files: odv_expdp.c, odv_exp.c, odv_number.c, odv_datetime.c, odv_charset.c, odv_xml.c, odv_csv.c, odv_sql.c
- [x] BUILD VERIFIED: 13 exports, 148KB DLL

## Phase 2: Dump Format Detection - COMPLETED (included in Phase 1)
- [x] EXP detection: "EXPORT:" signature at offset 0x03-0x1f
- [x] EXPDP detection: "xml version" marker scan
- [x] Compressed EXPDP detection: "KGC" + "HDR" markers
- [x] Charset extraction from header

## Phase 3: EXPDP Parsing + Table Listing - COMPLETED
- [x] odv_xml.c - Lightweight XML parser (byte-by-byte, tag/value callback)
  - Handles `<?...?>`, `<!--...-->`, open/close/self-closing tags
  - Trims whitespace from text content before delivery
- [x] odv_charset.c - Character set conversion (Win32 MultiByteToWideChar/WideCharToMultiByte)
  - Supports UTF-8, SJIS (CP932), EUC-JP (CP20932), US7ASCII, UTF-16LE/BE
  - Optimized pass-through for same-charset conversion
- [x] odv_expdp.c - Full EXPDP DataPump parser (~450 lines)
  - DDL XML extraction: scans for `<?xml` markers, accumulates until `</ROWSET>`
  - `ddl_xml_callback`: populates ODV_TABLE from XML tags (NAME, OWNER_NAME, COL_LIST_ITEM, COL_NAME, TYPE_NUM, LENGTH, PRECISION_NUM, SCALE, CHARSET, FLAGS, PROPERTY)
  - `type_num_to_col_type`: Maps Oracle TYPE_NUM → internal COL_* constants
  - `is_dictionary_table`: Filters system metadata tables (SCN, SEED, OPERATION, etc.)
  - `notify_table`: Fires table_callback and adds to table_list
  - `parse_expdp_records`: Binary record state machine
    - Record headers: 0x01/0x04 normal, 0x08/0x09 LOB, 0x18/0x19 >255-col, 0xff end
    - Column length: 0x00=empty, 0x01-0xfd=direct, 0xfe=2-byte LE, 0xff=NULL
    - Type-specific decoding delegated to odv_number/odv_datetime modules
    - Charset conversion for string types (CHAR, VARCHAR, NCHAR, NVARCHAR)
    - LOB chunk reassembly with dynamic buffer
  - `parse_expdp_dump`: Main loop (4KB block reads, DDL/data alternation)
  - Skips SYS_NC*****$ hidden columns
- [x] BUILD VERIFIED: All 11 .c files compile, DLL generated

## Phase 4: Data Type Conversion - COMPLETED
- [x] odv_number.c - Oracle NUMBER binary decoding
  - Positive numbers: exponent >= 0xC0, digit = (byte - 1), base-100 pairs
  - Negative numbers: exponent <= 0x3F, digit = (101 - byte), terminator 0x66
  - Small decimals: leading zero pairs via exponent offset
  - Zero: single byte 0x80
  - Trailing zero trimming for fractional part
- [x] odv_datetime.c - Oracle DATE/TIMESTAMP/BINARY_FLOAT/BINARY_DOUBLE decoding
  - DATE (7 bytes): century/year offset 100, month/day direct, H/M/S offset 1
  - TIMESTAMP (7-11 bytes): DATE + big-endian 32-bit nanoseconds, trailing zero trim
  - BINARY_FLOAT (4 bytes): Oracle-modified IEEE 754, sign bit transform + byte reversal
  - BINARY_DOUBLE (8 bytes): Same transform for 8-byte double precision
  - Format options: SLASH ("YYYY/MM/DD HH:MI:SS"), COMPACT, FULL
- [x] BUILD VERIFIED: Zero warnings, zero errors

## Phase 5: Record Parsing - COMPLETED (included in Phase 3)
- [x] odv_record.c - Buffer management + row delivery (implemented in Phase 1)
- [x] parse_expdp_records in odv_expdp.c - EXPDP binary record state machine (Phase 3)
- [x] Column length encoding: 0x00=empty, 0x01-0xfd=direct, 0xfe=2-byte LE, 0xff=NULL
- [x] Record headers: 0x01/0x04, 0x08/0x09 LOB, 0x18/0x19 >255-col, 0xff=end
- [x] LOB chunk reassembly with dynamic buffer
- [x] 255-column boundary filler handling

## Phase 6: EXP Parsing - COMPLETED
- [x] odv_exp.c - Full legacy EXP format parser (~600 lines)
  - `parse_exp_header`: 256-byte header parsing (version, mode RTABLES/RUSERS/RENTIRE, charset detection)
  - `parse_create_table`: DDL text parser for CREATE TABLE statements
    - Extracts schema.table, column names, types from quoted identifiers
    - Skips CONSTRAINT/PRIMARY/UNIQUE/FOREIGN/CHECK clauses
  - `parse_column_type`: Maps DDL type strings (VARCHAR2, NUMBER, DATE, etc.) to COL_* constants
    - Extracts length/precision/scale from parentheses
    - Supports all Oracle types including INTERVAL, BINARY_FLOAT/DOUBLE, XMLTYPE
  - `parse_exp_records`: Binary record reader
    - 2-byte LE length prefix per column
    - Special markers: 0x0000=record end, 0xFFFF=table end, 0xFFFE=NULL, 0xFF00=4-byte length
  - `decode_exp_column`: Type-specific column decoding
    - NUMBER/DATE/TIMESTAMP via odv_number/odv_datetime
    - RAW/ROWID as hex string
    - String types with charset conversion
    - CHAR right-space trimming
    - LOB placeholders (%BLOB%, %CLOB% etc.)
  - CONNECT schema detection for multi-schema exports
  - Direct export mode detection ("D\\n" marker)
- [x] BUILD VERIFIED: Zero warnings, zero errors

## Phase 7: Output (CSV/SQL) - PENDING

## Phase 8: VB.NET Integration - PENDING
