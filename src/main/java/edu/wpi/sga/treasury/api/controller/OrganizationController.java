package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.api.contract.response.PagedResponse;
import edu.wpi.sga.treasury.application.accessor.OrganizationAccessor;
import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("api/v1")
@RequiredArgsConstructor
public class OrganizationController {
    // Accessors
    private final OrganizationAccessor organizationAccessor;

    @PostMapping(value = "/organizations", produces = MediaType.APPLICATION_JSON_VALUE)
    public PagedResponse<List<OrganizationDto>> getAllOrganizations(@RequestBody PagedRequest request) {
        Page<OrganizationDto> data = organizationAccessor.getAllOrganizations(request);

        return PagedResponse.<List<OrganizationDto>>builder()
                .data(data.getContent())
                .maxResults(Math.toIntExact(data.getTotalElements()))
                .pageNumber(request.getPage())
                .resultsPerPage(request.getResultsPerPage())
                .message("Successfully got " + data.getNumberOfElements() + " Organizations")
                .build();
    }
}
