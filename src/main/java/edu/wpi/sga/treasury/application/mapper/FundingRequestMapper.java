package edu.wpi.sga.treasury.application.mapper;

import edu.wpi.sga.treasury.application.dto.funding_request.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.funding_request.FundingRequestDto;
import edu.wpi.sga.treasury.domain.model.funding_request.FundingRequest;
import org.mapstruct.*;

import java.util.List;

@Mapper(unmappedTargetPolicy = ReportingPolicy.IGNORE, componentModel = "spring")
public interface FundingRequestMapper {
    @Mapping(source = "nameOfClub", target = "organization.name")
    @Mapping(source = "hearingDate", target = "hearingDate")
    FundingRequest fundingRequestDtoToFundingRequest(FundingRequestDto fundingRequestDto);

    @InheritInverseConfiguration(name = "fundingRequestDtoToFundingRequest")
    FundingRequestDto fundingRequestToFundingRequestDto(FundingRequest fundingRequest);

    @InheritConfiguration
    List<FundingRequestDto> toFundingRequestDtos(List<FundingRequest> fundingRequests);

    @InheritConfiguration(name = "fundingRequestDtoToFundingRequest")
    @BeanMapping(nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE)
    void updateFundingRequestFromFundingRequestDto(FundingRequestDto fundingRequestDto, @MappingTarget FundingRequest fundingRequest);

    @Mapping(source = "nameOfClub", target = "organization.name")
    @Mapping(source = "hearingDate", target = "hearingDate")
    FundingRequest fundingRequestDetailedDtoToFundingRequest(FundingRequestDetailedDto fundingRequestDetailedDto);

    @Mapping(source = "organization.name", target = "nameOfClub")
    @Mapping(target = "hearingDate", source = "hearingDate")
    FundingRequestDetailedDto fundingRequestToFundingRequestDetailedDto(FundingRequest fundingRequest);

    @Mapping(source = "nameOfClub", target = "organization.name")
    @BeanMapping(nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE)
    void updateFundingRequestFromFundingRequestDetailedDto(FundingRequestDetailedDto fundingRequestDetailedDto, @MappingTarget FundingRequest fundingRequest);
}
