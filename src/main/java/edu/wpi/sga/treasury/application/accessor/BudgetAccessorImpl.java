package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.BudgetDto;
import edu.wpi.sga.treasury.application.util.BudgetHelperFunctions;
import edu.wpi.sga.treasury.domain.model.Budget;
import edu.wpi.sga.treasury.domain.repository.BudgetRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@RequiredArgsConstructor
@Service
public class BudgetAccessorImpl implements BudgetAccessor {

    // Repositories
    private final BudgetRepository budgetRepository;

    // Util
    private final BudgetHelperFunctions budgetHelperFunctions;

    @Override
    public List<BudgetDto> getBudgetsForOrganization(String organization) {
        List<Budget> organizationBudgets = budgetRepository.findAllByOrganizationNameIs(organization);

        return organizationBudgets.stream().map(budgetHelperFunctions::translateBudgetToBudgetDto).collect(Collectors.toList());
    }

    @Override
    public BudgetDetailedDto getBudgetById(Integer id) {
        Budget budget = budgetRepository.getById(id);

        return budgetHelperFunctions.translateBudgetToBudgetDetailedDto(budget);
    }
}
