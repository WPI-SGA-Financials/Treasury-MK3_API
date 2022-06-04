package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDto;
import org.springframework.data.domain.Page;

import java.util.List;

public interface BudgetAccessor {
    /**
     *
     * @param organization
     * @return
     */
    List<BudgetDto> getBudgetsForOrganization(String organization);

    /**
     *
     * @param request
     * @return
     */
    Page<BudgetDto> getBudgets(PagedRequest request);

    /**
     *
     * @param id
     * @return
     */
    BudgetDetailedDto getBudgetById(Integer id);
}
