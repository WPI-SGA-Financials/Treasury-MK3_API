using System.Collections.Generic;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.DTOs;

namespace Treasury.Application.Accessor.Interface
{
    public interface IReallocationRequestAccessor
    {
        List<ReallocationRequestDto> GetReallocationRequestsByOrganization(string organization);
        List<ReallocationRequestDto> GetReallocationRequests(FinancialPagedRequest financialPagedRequest, out int maxResults);
        ReallocationRequestDetailedDto GetReallocationRequestById(int id);
    }
}