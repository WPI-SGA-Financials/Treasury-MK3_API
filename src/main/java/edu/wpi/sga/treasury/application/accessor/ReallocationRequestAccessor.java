package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;

import java.util.List;

public interface ReallocationRequestAccessor {
    /**
     *
     * @param organization
     * @return
     */
    List<?> getReallocationRequestsForOrganization(String organization);

    /**
     *
     * @param request
     * @return
     */
    List<?> getReallocationRequest(PagedRequest request);

    /**
     *
     * @param id
     * @return
     */
    Object getReallocationRequestById(Integer id);
}
