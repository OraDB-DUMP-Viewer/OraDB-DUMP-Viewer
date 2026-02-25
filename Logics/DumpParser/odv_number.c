/*****************************************************************************
    OraDB DUMP Viewer

    odv_number.c
    Oracle NUMBER binary format decoding

    Copyright (C) 2026 YANAI Taketo
 *****************************************************************************/

#include "odv_types.h"

/* Stub - will be implemented in Phase 4 */
int decode_oracle_number(const unsigned char *buf, int len, char *out, int out_size)
{
    if (!buf || !out || out_size < 2) return ODV_ERROR_INVALID_ARG;
    out[0] = '0';
    out[1] = '\0';
    return ODV_OK;
}
