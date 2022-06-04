package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.mapper.OrganizationMapper;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.repository.OrganizationRepository;
import edu.wpi.sga.treasury.domain.specification.OrganizationSpecification;
import lombok.RequiredArgsConstructor;
import org.mapstruct.factory.Mappers;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.Optional;

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
    public PagedResponse<OrganizationDto> getOrganizations(PagedRequest request) {
        Pageable pageable = generalHelperFunctions.generatePagedRequest(request);

        request = generalHelperFunctions.cleanRequest(request);

        Specification<Organization> orgSpec = OrganizationSpecification.builder().request(request).build();

        Page<Organization> organizations = organizationRepository.findAll(orgSpec, pageable);

        return new PagedResponse<>(organizations, organizationMapper::organizationsToOrganizationsDtos);
    }

    @Override
    public Response<OrganizationDto> getOrganization(String organization) {
        Optional<Organization> optionalOrg = organizationRepository.findByName(organization);

        return optionalOrg.map(o -> new Response<>(o, organizationMapper::organizationToOrganizationDto))
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND));
    }
}
