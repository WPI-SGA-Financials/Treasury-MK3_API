package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDto;
import org.springframework.data.domain.Page;

import java.util.List;

public interface ReallocationRequestAccessor {
    /**
     * Get a list of Reallocation Requests for an Organization
     *
     * @param organization Organization
     * @return List of Reallocation Requests
     */
    List<ReallocationRequestDto> getReallocationRequestsForOrganization(String organization);

    /**
     * Get a filtered and paged list of Reallocation Requests
     *
     * @param request Paged Request
     * @return Page of Reallocation Requests
     */
    Page<ReallocationRequestDto> getReallocationRequests(PagedRequest request);

    /**
     * Get Reallocation Request by ID
     *
     * @param id Reallocation ID
     * @return Reallocation
     */
    ReallocationRequestDetailedDto getReallocationRequestById(Integer id);
}
