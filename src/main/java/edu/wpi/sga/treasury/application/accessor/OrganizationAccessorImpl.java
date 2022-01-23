package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import edu.wpi.sga.treasury.application.mapper.OrganizationMapper;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.repository.OrganizationRepository;
import lombok.RequiredArgsConstructor;
import org.hibernate.cfg.NotYetImplementedException;
import org.mapstruct.factory.Mappers;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class OrganizationAccessorImpl implements OrganizationAccessor {

    // Repositories
    private final OrganizationRepository organizationRepository;

    // Mappers
    private final OrganizationMapper organizationMapper = Mappers.getMapper(OrganizationMapper.class);

    // Helpers
    private final GeneralHelperFunctions generalHelperFunctions;

    @Override
    public Page<OrganizationDto> getOrganizations(PagedRequest request) {
        Pageable pageable = generalHelperFunctions.generatePagedRequest(request);

        Page<Organization> organizations;

        request = generalHelperFunctions.cleanRequest(request);

        if(generalHelperFunctions.determineFilterable(request)) {
            organizations = organizationRepository.findOrganizationsByFilters(request);
        } else {
            organizations = organizationRepository.findAllByInactiveIsFalse(pageable);
        }

        List<OrganizationDto> dtos = organizationMapper.organizationsToOrganizationsDtos(organizations.getContent());

        return new PageImpl<>(dtos, pageable, organizations.getTotalElements());
    }

    @Override
    public Object getOrganization(String organization) {
        throw new NotYetImplementedException();
    }
}
