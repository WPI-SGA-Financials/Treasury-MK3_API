package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;

import java.util.List;

public interface FundingRequestAccessor {
    /**
     *
     * @param organization
     * @return
     */
    List<?> getFundingRequestsForOrganization(String organization);

    /**
     *
     * @param request
     * @return
     */
    List<?> getFundingRequest(PagedRequest request);

    /**
     *
     * @param id
     * @return
     */
    Object getFundingRequestById(Integer id);
}
