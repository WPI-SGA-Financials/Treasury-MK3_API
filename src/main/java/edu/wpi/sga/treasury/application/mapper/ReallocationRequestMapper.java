package edu.wpi.sga.treasury.application.mapper;

import edu.wpi.sga.treasury.application.dto.ReallocationRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDto;
import edu.wpi.sga.treasury.domain.model.ReallocationRequest;
import org.mapstruct.*;

@Mapper(unmappedTargetPolicy = ReportingPolicy.IGNORE, componentModel = "spring")
public interface ReallocationRequestMapper {
    @Mapping(source = "organizationName", target = "organization.name")
    ReallocationRequest reallocationRequestDtoToReallocationRequest(ReallocationRequestDto reallocationRequestDto);

    @Mapping(source = "organization.name", target = "organizationName")
    ReallocationRequestDto reallocationRequestToReallocationRequestDto(ReallocationRequest reallocationRequest);

    @Mapping(source = "organizationName", target = "organization.name")
    @BeanMapping(nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE)
    void updateReallocationRequestFromReallocationRequestDto(ReallocationRequestDto reallocationRequestDto, @MappingTarget ReallocationRequest reallocationRequest);

    @Mapping(source = "organizationName", target = "organization.name")
    ReallocationRequest reallocationRequestDetailedDtoToReallocationRequest(ReallocationRequestDetailedDto reallocationRequestDetailedDto);

    @Mapping(source = "organization.name", target = "organizationName")
    ReallocationRequestDetailedDto reallocationRequestToReallocationRequestDetailedDto(ReallocationRequest reallocationRequest);

    @Mapping(source = "organizationName", target = "organization.name")
    @BeanMapping(nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE)
    void updateReallocationRequestFromReallocationRequestDetailedDto(ReallocationRequestDetailedDto reallocationRequestDetailedDto, @MappingTarget ReallocationRequest reallocationRequest);
}