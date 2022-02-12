package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDto;
import edu.wpi.sga.treasury.application.mapper.FundingRequestMapper;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.FundingRequest;
import edu.wpi.sga.treasury.domain.repository.FundingRequestRepository;
import lombok.RequiredArgsConstructor;
import org.mapstruct.factory.Mappers;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.Pageable;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class FundingRequestAccessorImpl implements FundingRequestAccessor {
    // Repositories
    private final FundingRequestRepository fundingRequestRepository;

    // Mappers
    private final FundingRequestMapper fundingRequestMapper = Mappers.getMapper(FundingRequestMapper.class);

    // Util
    private final GeneralHelperFunctions generalHelperFunctions;

    @Override
    public List<FundingRequestDto> getFundingRequestsForOrganization(String organization) {
        Optional<List<FundingRequest>> orgFrs = fundingRequestRepository.findAllByOrganizationNameOrderByFundingDateDesc(organization);

        if(orgFrs.isPresent() && !orgFrs.get().isEmpty()) {
            return orgFrs.get().stream().map(fundingRequestMapper::fundingRequestToFundingRequestDto).collect(Collectors.toList());
        }

        throw new ResponseStatusException(HttpStatus.NOT_FOUND);
    }

    @Override
    public Page<FundingRequestDto> getFundingRequests(PagedRequest request) {
        Pageable pageable = generalHelperFunctions.generatePagedRequest(request);

        Page<FundingRequest> fundingRequests;

        request = generalHelperFunctions.cleanRequest(request);

        if (generalHelperFunctions.determineFilterable(request)) {
            fundingRequests = fundingRequestRepository.findFundingRequestsByFilters(request);
        } else {
            fundingRequests = fundingRequestRepository.findAllByOrganizationInactiveIsFalseOrderByFundingDateDescDotNumberDesc(pageable);
        }

        List<FundingRequestDto> dtos = fundingRequests.getContent().stream().map(fundingRequestMapper::fundingRequestToFundingRequestDto).collect(Collectors.toList());

        return new PageImpl<>(dtos, pageable, fundingRequests.getTotalElements());
    }

    @Override
    public FundingRequestDetailedDto getFundingRequestById(Integer id) {
        Optional<FundingRequest> request = fundingRequestRepository.findById(id);

        if (request.isPresent()) {
            return fundingRequestMapper.fundingRequestToFundingRequestDetailedDto(request.get());
        }

        throw new ResponseStatusException(HttpStatus.NOT_FOUND);
    }
}
