/*****************************************************************************
    OraDB DUMP Viewer

    odv_sql.c
    SQL INSERT statement output

    Copyright (C) 2026 YANAI Taketo
 *****************************************************************************/

#include "odv_types.h"

/* Stub - will be implemented in Phase 7 */
int write_sql_file(ODV_SESSION *s, const char *table_name, const char *output_path, int dbms_type)
{
    if (!s) return ODV_ERROR_INVALID_ARG;
    snprintf(s->last_error, ODV_MSG_LEN, "SQL export not yet implemented");
    return ODV_ERROR_UNSUPPORTED;
}
