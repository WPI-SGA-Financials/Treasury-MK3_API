package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.api.contract.response.PagedResponse;
import edu.wpi.sga.treasury.api.contract.response.Response;
import edu.wpi.sga.treasury.application.accessor.ReallocationRequestAccessor;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDto;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequiredArgsConstructor
public class ReallocationRequestController {
    private final ReallocationRequestAccessor reallocationRequestAccessor;

    @PostMapping(value = "/financials/reallocs", produces = MediaType.APPLICATION_JSON_VALUE)
    public PagedResponse<ReallocationRequestDto> getReallocationRequests(@RequestBody PagedRequest request) {
        Page<ReallocationRequestDto> data = reallocationRequestAccessor.getReallocationRequests(request);

        return PagedResponse.<ReallocationRequestDto>builder()
                .data(data.getContent())
                .maxResults(Math.toIntExact(data.getTotalElements()))
                .pageNumber(request.getPage())
                .resultsPerPage(request.getResultsPerPage())
                .message("Successfully got " + data.getNumberOfElements() + " Reallocation Requests")
                .build();
    }

    @GetMapping(value = "/organization/{name}/reallocs", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<List<ReallocationRequestDto>> getOrganizationReallocationRequests(@PathVariable String name) {
        List<ReallocationRequestDto> fundingRequests = reallocationRequestAccessor.getReallocationRequestsForOrganization(name);

        return Response.<List<ReallocationRequestDto>>builder()
                .data(fundingRequests)
                .message("Successfully returned the organization's reallocation requests")
                .build();
    }

    @GetMapping(value = "/financials/realloc/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<ReallocationRequestDetailedDto> getReallocationRequestById(@PathVariable Integer id) {
        ReallocationRequestDetailedDto dto = reallocationRequestAccessor.getReallocationRequestById(id);

        return Response.<ReallocationRequestDetailedDto>builder()
                .data(dto)
                .message("Successfully returned the reallocation request")
                .build();
    }
}
