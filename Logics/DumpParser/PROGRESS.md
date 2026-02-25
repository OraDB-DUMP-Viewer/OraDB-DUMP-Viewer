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

## Phase 4: Data Type Conversion - PENDING

## Phase 5: Record Parsing - PENDING

## Phase 6: EXP Parsing - PENDING

## Phase 7: Output (CSV/SQL) - PENDING

## Phase 8: VB.NET Integration - PENDING
