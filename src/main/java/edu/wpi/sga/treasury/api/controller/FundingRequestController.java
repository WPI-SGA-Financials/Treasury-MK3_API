package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.api.contract.response.PagedResponse;
import edu.wpi.sga.treasury.api.contract.response.Response;
import edu.wpi.sga.treasury.application.accessor.FundingRequestAccessor;
import edu.wpi.sga.treasury.application.dto.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDto;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequiredArgsConstructor
public class FundingRequestController {
    private final FundingRequestAccessor fundingRequestAccessor;

    @PostMapping(value = "/financials/frs", produces = MediaType.APPLICATION_JSON_VALUE)
    public PagedResponse<FundingRequestDto> getFundingRequests(@RequestBody PagedRequest request) {
        Page<FundingRequestDto> data = fundingRequestAccessor.getFundingRequests(request);

        return PagedResponse.<FundingRequestDto>builder()
                .data(data.getContent())
                .maxResults(Math.toIntExact(data.getTotalElements()))
                .pageNumber(request.getPage())
                .resultsPerPage(request.getResultsPerPage())
                .message("Successfully got " + data.getNumberOfElements() + " Funding Requests")
                .build();
    }

    @GetMapping(value = "/organization/{name}/frs", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<List<FundingRequestDto>> getOrganizationFundingRequests(@PathVariable String name) {
        List<FundingRequestDto> fundingRequests = fundingRequestAccessor.getFundingRequestsForOrganization(name);

        return Response.<List<FundingRequestDto>>builder()
                .data(fundingRequests)
                .message("Successfully returned the organization's funding requests")
                .build();
    }

    @GetMapping(value = "/financials/fr/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<FundingRequestDetailedDto> getFundingRequestById(@PathVariable Integer id) {
        FundingRequestDetailedDto dto = fundingRequestAccessor.getFundingRequestById(id);

        return Response.<FundingRequestDetailedDto>builder()
                .data(dto)
                .message("Successfully returned the funding request")
                .build();
    }
}
