package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.mapper.custom.BudgetMapperCustom;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.budget.Budget;
import edu.wpi.sga.treasury.domain.repository.BudgetRepository;
import edu.wpi.sga.treasury.domain.specification.BudgetSpecification;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;

@RequiredArgsConstructor
@Service
public class BudgetAccessorImpl implements BudgetAccessor {

    // Repositories
    private final BudgetRepository budgetRepository;

    // Mappers
    private final BudgetMapperCustom budgetMapperCustom;

    // Util
    private final GeneralHelperFunctions generalHelperFunctions;

    @Override
    public ListResponse<BudgetDto> getBudgetsForOrganization(String organization) {
        List<Budget> organizationBudgets = budgetRepository.findAllByOrganizationNameIsOrderByFiscalYearDesc(organization);

        return new ListResponse<>(organizationBudgets, budgetMapperCustom::toBudgetDtos);
    }

    @Override
    public PagedResponse<BudgetDto> getBudgets(PagedRequest request) {
        Pageable pageable = generalHelperFunctions.generatePagedRequest(request);

        request = generalHelperFunctions.cleanRequest(request);

        Specification<Budget> specification = BudgetSpecification.builder().request(request).build();

        Page<Budget> budgets = budgetRepository.findAll(specification, pageable);

        return new PagedResponse<>(budgets, budgetMapperCustom::toBudgetDtos);
    }

    @Override
    public Response<BudgetDetailedDto> getBudgetById(Integer id) {
        Optional<Budget> optionalBudget = budgetRepository.findById(id);

        return optionalBudget.map(o -> new Response<>(o, budgetMapperCustom::toBudgetDetailedDto))
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND));
    }
}
