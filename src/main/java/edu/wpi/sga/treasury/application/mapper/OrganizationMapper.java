package edu.wpi.sga.treasury.application.mapper;

import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import edu.wpi.sga.treasury.domain.model.Organization;
import org.mapstruct.*;

import java.util.List;

@Mapper(unmappedTargetPolicy = ReportingPolicy.IGNORE, componentModel = "spring")
public interface OrganizationMapper {
    @InheritInverseConfiguration
    Organization organizationDtoToOrganization(OrganizationDto organizationDto);

    @Mapping(source = "name", target = "nameOfClub")
    OrganizationDto organizationToOrganizationDto(Organization organization);

    List<OrganizationDto> organizationsToOrganizationsDtos(List<Organization> organizationList);

    @BeanMapping(nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE)
    void updateOrganizationFromOrganizationDto(OrganizationDto organizationDto, @MappingTarget Organization organization);
}
