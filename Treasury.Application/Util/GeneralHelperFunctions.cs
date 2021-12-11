using Treasury.Application.Contracts.V1.Requests;

namespace Treasury.Application.Util
{
    public static class GeneralHelperFunctions
    {
        public static int GetPage(IPagedRequest generalPagedRequest)
        {
            return generalPagedRequest.Page * generalPagedRequest.Rpp - generalPagedRequest.Rpp;
        }
    }
}