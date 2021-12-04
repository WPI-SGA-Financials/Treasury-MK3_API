using System.Collections.Generic;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.DTOs;

namespace Treasury.Application.Accessor.Interface
{
    public interface IOrganizationAccessor
    {
        List<OrganizationDto> GetOrganizations(GeneralPagedRequest generalPagedRequest, out int maxResults);
        OrganizationDetailDto GetOrganization(string name);
    }
}