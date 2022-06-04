package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.funding_request.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.funding_request.FundingRequestDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.mapper.FundingRequestMapper;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.funding_request.FundingRequest;
import edu.wpi.sga.treasury.domain.repository.FundingRequestRepository;
import edu.wpi.sga.treasury.domain.specification.FundingRequestSpecification;
import lombok.RequiredArgsConstructor;
import org.mapstruct.factory.Mappers;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;

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
    public ListResponse<FundingRequestDto> getFundingRequestsForOrganization(String organization) {
        List<FundingRequest> orgFrs = fundingRequestRepository.findAllByOrganizationNameOrderByHearingDateDesc(organization);

        return new ListResponse<>(orgFrs, fundingRequestMapper::toFundingRequestDtos);
    }

    @Override
    public PagedResponse<FundingRequestDto> getFundingRequests(PagedRequest request) {
        Pageable pageable = generalHelperFunctions.generatePagedRequest(request);

        request = generalHelperFunctions.cleanRequest(request);

        Specification<FundingRequest> fundingRequestSpecification = FundingRequestSpecification.builder().request(request).build();

        Page<FundingRequest> fundingRequests = fundingRequestRepository.findAll(fundingRequestSpecification, pageable);

        return new PagedResponse<>(fundingRequests, fundingRequestMapper::toFundingRequestDtos);
    }

    @Override
    public Response<FundingRequestDetailedDto> getFundingRequestById(Integer id) {
        Optional<FundingRequest> optionalFundingRequest = fundingRequestRepository.findById(id);

        return optionalFundingRequest.map(o -> new Response<>(o, fundingRequestMapper::fundingRequestToFundingRequestDetailedDto))
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND));

    }
}
