package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;

public interface FundingRequestAccessor {
    /**
     * @param organization
     * @return
     */
    ListResponse<FundingRequestDto> getFundingRequestsForOrganization(String organization);

    /**
     * @param request
     * @return
     */
    PagedResponse<FundingRequestDto> getFundingRequests(PagedRequest request);

    /**
     * @param id
     * @return
     */
    Response<FundingRequestDetailedDto> getFundingRequestById(Integer id);
}
