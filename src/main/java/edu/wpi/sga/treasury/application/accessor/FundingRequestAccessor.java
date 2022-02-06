package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDto;
import org.springframework.data.domain.Page;

import java.util.List;

public interface FundingRequestAccessor {
    /**
     *
     * @param organization
     * @return
     */
    List<FundingRequestDto> getFundingRequestsForOrganization(String organization);

    /**
     *
     * @param request
     * @return
     */
    Page<FundingRequestDto> getFundingRequests(PagedRequest request);

    /**
     *
     * @param id
     * @return
     */
    FundingRequestDetailedDto getFundingRequestById(Integer id);
}
