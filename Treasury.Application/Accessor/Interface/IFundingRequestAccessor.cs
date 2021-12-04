using System.Collections.Generic;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.DTOs;

namespace Treasury.Application.Accessor.Interface
{
    public interface IFundingRequestAccessor
    {
        List<FundingRequestDto> GetFundingRequestsByOrganization(string organization);
        List<FundingRequestDto> GetFundingRequests(FinancialPagedRequest financialPagedRequest, out int maxResults);
        FundingRequestDetailedDto GetFundingRequestById(int id);
    }
}