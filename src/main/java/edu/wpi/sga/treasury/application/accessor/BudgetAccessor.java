package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.BudgetDto;
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
     * @param id
     * @return
     */
    BudgetDetailedDto getBudgetById(Integer id);

    /**
     *
     * @param request
     * @return
     */
    Page<BudgetDto> getBudgets(PagedRequest request);
}
