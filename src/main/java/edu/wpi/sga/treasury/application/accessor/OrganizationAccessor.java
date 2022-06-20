package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;

public interface OrganizationAccessor {
    /**
     * @param request
     * @return
     */
    PagedResponse<OrganizationDto> getOrganizations(PagedRequest request);

    /**
     * @param organization
     * @return
     */
    Response<OrganizationDto> getOrganization(String organization);
}
