package edu.wpi.sga.treasury.application.mapper;

import edu.wpi.sga.treasury.application.dto.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDto;
import edu.wpi.sga.treasury.domain.model.FundingRequest;
import org.mapstruct.*;

@Mapper(unmappedTargetPolicy = ReportingPolicy.IGNORE, componentModel = "spring")
public interface FundingRequestMapper {
    @Mapping(source = "nameOfClub", target = "organization.name")
    @Mapping(source = "hearingDate", target = "fundingDate")
    FundingRequest fundingRequestDtoToFundingRequest(FundingRequestDto fundingRequestDto);

    @InheritInverseConfiguration(name = "fundingRequestDtoToFundingRequest")
    FundingRequestDto fundingRequestToFundingRequestDto(FundingRequest fundingRequest);

    @InheritConfiguration(name = "fundingRequestDtoToFundingRequest")
    @BeanMapping(nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE)
    void updateFundingRequestFromFundingRequestDto(FundingRequestDto fundingRequestDto, @MappingTarget FundingRequest fundingRequest);

    @Mapping(source = "nameOfClub", target = "organization.name")
    @Mapping(source = "hearingDate", target = "fundingDate")
    FundingRequest fundingRequestDetailedDtoToFundingRequest(FundingRequestDetailedDto fundingRequestDetailedDto);

    @Mapping(source = "organization.name", target = "nameOfClub")
    @Mapping(target = "hearingDate", source = "fundingDate")
    FundingRequestDetailedDto fundingRequestToFundingRequestDetailedDto(FundingRequest fundingRequest);

    @Mapping(source = "nameOfClub", target = "organization.name")
    @BeanMapping(nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE)
    void updateFundingRequestFromFundingRequestDetailedDto(FundingRequestDetailedDto fundingRequestDetailedDto, @MappingTarget FundingRequest fundingRequest);
}
