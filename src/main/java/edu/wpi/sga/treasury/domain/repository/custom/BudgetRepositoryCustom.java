package edu.wpi.sga.treasury.domain.repository.custom;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.domain.model.Budget;
import org.springframework.data.domain.Page;

public interface BudgetRepositoryCustom {
    Page<Budget> findBudgetsByFilters(PagedRequest request);
}
