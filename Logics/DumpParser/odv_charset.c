/*****************************************************************************
    OraDB DUMP Viewer

    odv_charset.c
    Character set conversion using Win32 API

    Copyright (C) 2026 YANAI Taketo
 *****************************************************************************/

#include "odv_types.h"

/* Stub - will be implemented in Phase 3 */
int convert_charset(const char *src, int src_len, int src_cs,
                    char *dst, int dst_size, int dst_cs, int *out_len)
{
    int copy_len;
    if (!src || !dst) return ODV_ERROR_INVALID_ARG;

    /* Passthrough: just copy */
    copy_len = (src_len < dst_size - 1) ? src_len : dst_size - 1;
    memcpy(dst, src, copy_len);
    dst[copy_len] = '\0';
    if (out_len) *out_len = copy_len;
    return ODV_OK;
}
