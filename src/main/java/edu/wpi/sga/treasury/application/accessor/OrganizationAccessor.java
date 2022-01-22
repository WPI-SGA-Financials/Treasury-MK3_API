package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.GeneralPagedRequest;
import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import edu.wpi.sga.treasury.application.util.PagedTuple;

import java.util.List;

public interface OrganizationAccessor {
    PagedTuple<List<OrganizationDto>, Long> getAllOrganizations(GeneralPagedRequest request);
}
