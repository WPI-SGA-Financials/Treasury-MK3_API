package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
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
        return organizationAccessor.getOrganizations(request);
    }

    @GetMapping(value = "/organization/{name}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<OrganizationDto> getOrganization(@PathVariable String name) {
        return organizationAccessor.getOrganization(name);
    }
}
