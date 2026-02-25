/*****************************************************************************
    OraDB DUMP Viewer

    odv_xml.c
    Lightweight XML parser for EXPDP DDL metadata

    Copyright (C) 2026 YANAI Taketo
 *****************************************************************************/

#include "odv_types.h"

/* Stub - will be implemented in Phase 3 */
int parse_xml_ddl(const char *xml, int xml_len, xml_tag_callback cb, void *ctx)
{
    if (!xml || !cb) return ODV_ERROR_INVALID_ARG;
    return ODV_OK;
}
