package edu.wpi.sga.treasury.domain.repository.custom;

import edu.wpi.sga.treasury.api.contract.request.GeneralPagedRequest;
import edu.wpi.sga.treasury.domain.model.Organization;
import org.springframework.data.domain.Page;

public interface OrganizationRepositoryCustom {
    Page<Organization> findOrganizationsByFilters(GeneralPagedRequest request);
}
