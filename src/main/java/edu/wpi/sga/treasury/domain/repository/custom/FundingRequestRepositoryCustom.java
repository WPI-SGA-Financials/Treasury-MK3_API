package edu.wpi.sga.treasury.domain.repository.custom;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.domain.model.FundingRequest;
import org.springframework.data.domain.Page;

public interface FundingRequestRepositoryCustom {
    Page<FundingRequest> findFundingRequestsByFilters(PagedRequest request);
}
