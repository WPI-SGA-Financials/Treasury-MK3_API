package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;

public interface ReallocationRequestAccessor {
    /**
     * Get a list of Reallocation Requests for an Organization
     *
     * @param organization Organization
     * @return List of Reallocation Requests
     */
    ListResponse<ReallocationRequestDto> getReallocationRequestsForOrganization(String organization);

    /**
     * Get a filtered and paged list of Reallocation Requests
     *
     * @param request Paged Request
     * @return Page of Reallocation Requests
     */
    PagedResponse<ReallocationRequestDto> getReallocationRequests(PagedRequest request);

    /**
     * Get Reallocation Request by ID
     *
     * @param id Reallocation ID
     * @return Reallocation
     */
    Response<ReallocationRequestDetailedDto> getReallocationRequestById(Integer id);
}
