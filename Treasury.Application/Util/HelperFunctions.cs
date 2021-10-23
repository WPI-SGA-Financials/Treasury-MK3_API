﻿using Treasury.Application.Contracts.V1.Requests;

namespace Treasury.Application.Util
{
    public static class HelperFunctions
    {
        public static int GetPage(IPagedRequest generalPagedRequest)
        {
            return generalPagedRequest.Page * generalPagedRequest.Rpp - generalPagedRequest.Rpp;
        }
    }
}