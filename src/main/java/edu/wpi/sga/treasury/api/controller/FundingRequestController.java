package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.accessor.FundingRequestAccessor;
import edu.wpi.sga.treasury.application.dto.funding_request.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.funding_request.FundingRequestDto;
import lombok.RequiredArgsConstructor;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
public class FundingRequestController {
    private final FundingRequestAccessor fundingRequestAccessor;

    @PostMapping(value = "/financials/frs", produces = MediaType.APPLICATION_JSON_VALUE)
    public PagedResponse<FundingRequestDto> getFundingRequests(@RequestBody PagedRequest request) {
        return fundingRequestAccessor.getFundingRequests(request);
    }

    @GetMapping(value = "/organization/{name}/frs", produces = MediaType.APPLICATION_JSON_VALUE)
    public ListResponse<FundingRequestDto> getOrganizationFundingRequests(@PathVariable String name) {
        return fundingRequestAccessor.getFundingRequestsForOrganization(name);
    }

    @GetMapping(value = "/financials/fr/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<FundingRequestDetailedDto> getFundingRequestById(@PathVariable Integer id) {
        return fundingRequestAccessor.getFundingRequestById(id);
    }
}
