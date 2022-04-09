package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.BudgetDto;
import edu.wpi.sga.treasury.application.util.BudgetHelperFunctions;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.Budget;
import edu.wpi.sga.treasury.domain.repository.BudgetRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.Pageable;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@RequiredArgsConstructor
@Service
public class BudgetAccessorImpl implements BudgetAccessor {

    // Repositories
    private final BudgetRepository budgetRepository;

    // Util
    private final BudgetHelperFunctions budgetHelperFunctions;
    private final GeneralHelperFunctions generalHelperFunctions;

    @Override
    public List<BudgetDto> getBudgetsForOrganization(String organization) {
        Optional<List<Budget>> organizationBudgets = budgetRepository.findAllByOrganizationNameIsOrderByFiscalYearDesc(organization);

        if(organizationBudgets.isPresent() && !organizationBudgets.get().isEmpty()) {
            return organizationBudgets.get().stream().map(budgetHelperFunctions::translateBudgetToBudgetDto).collect(Collectors.toList());
        }

        throw new ResponseStatusException(HttpStatus.NOT_FOUND);
    }

    @Override
    public BudgetDetailedDto getBudgetById(Integer id) {
        Optional<Budget> budget = budgetRepository.findById(id);

        if(budget.isPresent()) {
            return budgetHelperFunctions.translateBudgetToBudgetDetailedDto(budget.get());
        }

        throw new ResponseStatusException(HttpStatus.NOT_FOUND);
    }

    @Override
    public Page<BudgetDto> getBudgets(PagedRequest request) {
        Pageable pageable = generalHelperFunctions.generatePagedRequest(request);

        Page<Budget> budgets;

        request = generalHelperFunctions.cleanRequest(request);

        if(generalHelperFunctions.determineFilterable(request)) {
            budgets = budgetRepository.findBudgetsByFilters(request);
        } else {
            budgets = budgetRepository.findAllByOrganizationInactiveIsFalseOrderByOrganizationAscFiscalYearDesc(pageable);
        }

        List<BudgetDto> dtos = budgets.getContent().stream().map(budgetHelperFunctions::translateBudgetToBudgetDto).collect(Collectors.toList());

        return new PageImpl<>(dtos, pageable, budgets.getTotalElements());
    }
}
