package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.api.contract.request.GeneralPagedRequest;
import edu.wpi.sga.treasury.api.contract.response.PagedResponse;
import edu.wpi.sga.treasury.api.contract.response.Response;
import edu.wpi.sga.treasury.application.accessor.OrganizationAccessor;
import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import edu.wpi.sga.treasury.application.util.PagedTuple;
import lombok.RequiredArgsConstructor;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("api/v1")
@RequiredArgsConstructor
public class OrganizationController {
    // Accessors
    private final OrganizationAccessor organizationAccessor;

    @PostMapping(value = "/organizations", produces = MediaType.APPLICATION_JSON_VALUE)
    public PagedResponse<List<OrganizationDto>> getAllOrganizations(@RequestBody GeneralPagedRequest request) {
        PagedTuple<List<OrganizationDto>, Long> data = organizationAccessor.getAllOrganizations(request);

        return PagedResponse.<List<OrganizationDto>>builder()
                .data(data.getData())
                .maxResults(data.getMaxResults().intValue())
                .pageNumber(request.getPage())
                .resultsPerPage(request.getResultsPerPage())
                .message("Successfully got " + data.getData().size() + " Organizations")
                .build();
    }
}
