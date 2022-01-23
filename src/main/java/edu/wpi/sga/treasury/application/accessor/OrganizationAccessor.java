package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import org.springframework.data.domain.Page;

public interface OrganizationAccessor {
    Page<OrganizationDto> getAllOrganizations(PagedRequest request);
}
