/*****************************************************************************
    OraDB DUMP Viewer

    odv_expdp.c
    DataPump (EXPDP) format dump file parsing

    Copyright (C) 2026 YANAI Taketo
 *****************************************************************************/

#include "odv_types.h"

/* Stub - will be implemented in Phase 3 */
int parse_expdp_dump(ODV_SESSION *s, int list_only)
{
    if (!s) return ODV_ERROR_INVALID_ARG;
    snprintf(s->last_error, ODV_MSG_LEN, "EXPDP parsing not yet implemented");
    return ODV_ERROR_UNSUPPORTED;
}
