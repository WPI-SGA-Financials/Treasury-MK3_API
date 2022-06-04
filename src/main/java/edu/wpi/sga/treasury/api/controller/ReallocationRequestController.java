package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.accessor.ReallocationRequestAccessor;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDto;
import lombok.RequiredArgsConstructor;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
public class ReallocationRequestController {
    private final ReallocationRequestAccessor reallocationRequestAccessor;

    @PostMapping(value = "/financials/reallocs", produces = MediaType.APPLICATION_JSON_VALUE)
    public PagedResponse<ReallocationRequestDto> getReallocationRequests(@RequestBody PagedRequest request) {
        return reallocationRequestAccessor.getReallocationRequests(request);
    }

    @GetMapping(value = "/organization/{name}/reallocs", produces = MediaType.APPLICATION_JSON_VALUE)
    public ListResponse<ReallocationRequestDto> getOrganizationReallocationRequests(@PathVariable String name) {
        return reallocationRequestAccessor.getReallocationRequestsForOrganization(name);
    }

    @GetMapping(value = "/financials/realloc/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<ReallocationRequestDetailedDto> getReallocationRequestById(@PathVariable Integer id) {
        return reallocationRequestAccessor.getReallocationRequestById(id);
    }
}
