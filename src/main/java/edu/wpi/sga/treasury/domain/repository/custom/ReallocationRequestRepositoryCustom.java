package edu.wpi.sga.treasury.domain.repository.custom;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.domain.model.ReallocationRequest;
import org.springframework.data.domain.Page;

public interface ReallocationRequestRepositoryCustom {
    Page<ReallocationRequest> findReallocsByFilters(PagedRequest request);
}
