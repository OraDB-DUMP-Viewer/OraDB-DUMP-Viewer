/*****************************************************************************
    OraDB DUMP Viewer

    odv_datetime.c
    Oracle DATE / TIMESTAMP binary format decoding

    Copyright (C) 2026 YANAI Taketo
 *****************************************************************************/

#include "odv_types.h"

/* Stub - will be implemented in Phase 4 */
int decode_oracle_date(const unsigned char *buf, int len, char *out, int out_size, int fmt)
{
    if (!buf || !out || out_size < 2) return ODV_ERROR_INVALID_ARG;
    out[0] = '\0';
    return ODV_OK;
}

int decode_oracle_timestamp(const unsigned char *buf, int len, char *out, int out_size, int fmt)
{
    if (!buf || !out || out_size < 2) return ODV_ERROR_INVALID_ARG;
    out[0] = '\0';
    return ODV_OK;
}

int decode_binary_float(const unsigned char *buf, char *out, int out_size)
{
    if (!buf || !out || out_size < 2) return ODV_ERROR_INVALID_ARG;
    out[0] = '0';
    out[1] = '\0';
    return ODV_OK;
}

int decode_binary_double(const unsigned char *buf, char *out, int out_size)
{
    if (!buf || !out || out_size < 2) return ODV_ERROR_INVALID_ARG;
    out[0] = '0';
    out[1] = '\0';
    return ODV_OK;
}
