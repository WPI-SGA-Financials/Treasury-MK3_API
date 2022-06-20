package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;

public interface BudgetAccessor {
    /**
     * @param organization
     * @return
     */
    ListResponse<BudgetDto> getBudgetsForOrganization(String organization);

    /**
     * @param request
     * @return
     */
    PagedResponse<BudgetDto> getBudgets(PagedRequest request);

    /**
     * @param id
     * @return
     */
    Response<BudgetDetailedDto> getBudgetById(Integer id);
}
