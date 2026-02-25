/*****************************************************************************
    OraDB DUMP Viewer

    odv_csv.c
    CSV file output

    Copyright (C) 2026 YANAI Taketo
 *****************************************************************************/

#include "odv_types.h"

/* Stub - will be implemented in Phase 7 */
int write_csv_file(ODV_SESSION *s, const char *table_name, const char *output_path)
{
    if (!s) return ODV_ERROR_INVALID_ARG;
    snprintf(s->last_error, ODV_MSG_LEN, "CSV export not yet implemented");
    return ODV_ERROR_UNSUPPORTED;
}
