package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.BudgetDto;

import java.util.List;

public interface BudgetAccessor {
    List<BudgetDto> getBudgetsForOrganization(String organization);
    BudgetDetailedDto getBudgetById(Integer id);
}
