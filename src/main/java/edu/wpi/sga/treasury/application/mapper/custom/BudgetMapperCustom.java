package edu.wpi.sga.treasury.application.mapper.custom;

import edu.wpi.sga.treasury.application.dto.budget.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDto;
import edu.wpi.sga.treasury.domain.model.budget.Budget;

import java.util.List;

public interface BudgetMapperCustom {

    List<BudgetDto> toBudgetDtos(List<Budget> budgets);

    BudgetDto toBudgetDto(Budget budget);

    BudgetDetailedDto toBudgetDetailedDto(Budget budget);


}
