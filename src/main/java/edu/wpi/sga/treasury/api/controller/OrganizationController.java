package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.api.contract.response.PagedResponse;
import edu.wpi.sga.treasury.api.contract.response.Response;
import edu.wpi.sga.treasury.application.accessor.OrganizationAccessor;
import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
public class OrganizationController {
    private final OrganizationAccessor organizationAccessor;

    @PostMapping(value = "/organizations", produces = MediaType.APPLICATION_JSON_VALUE)
    public PagedResponse<OrganizationDto> getOrganizations(@RequestBody PagedRequest request) {
        Page<OrganizationDto> data = organizationAccessor.getOrganizations(request);

        return PagedResponse.<OrganizationDto>builder()
                .data(data.getContent())
                .maxResults(Math.toIntExact(data.getTotalElements()))
                .pageNumber(request.getPage())
                .resultsPerPage(request.getResultsPerPage())
                .message("Successfully got " + data.getNumberOfElements() + " Organizations")
                .build();
    }

    @GetMapping(value = "/organization/{name}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<OrganizationDto> getOrganization(@PathVariable String name) {
        OrganizationDto dto = organizationAccessor.getOrganization(name);

        return Response.<OrganizationDto>builder()
                .data(dto)
                .message("Successfully returned the organization")
                .build();
    }
}
