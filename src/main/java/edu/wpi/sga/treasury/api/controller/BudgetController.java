package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.application.accessor.BudgetAccessor;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDto;
import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import lombok.RequiredArgsConstructor;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
public class BudgetController {
    private final BudgetAccessor budgetAccessor;

    @PostMapping(value = "/financials/budgets", produces = MediaType.APPLICATION_JSON_VALUE)
    public PagedResponse<BudgetDto> getBudgets(@RequestBody PagedRequest request) {
        return budgetAccessor.getBudgets(request);
    }

    @GetMapping(value = "/organization/{name}/budgets", produces = MediaType.APPLICATION_JSON_VALUE)
    public ListResponse<BudgetDto> getOrganizationBudgets(@PathVariable String name) {
        return budgetAccessor.getBudgetsForOrganization(name);
    }

    @GetMapping(value = "/financials/budget/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<BudgetDetailedDto> getBudgetById(@PathVariable Integer id) {
        return budgetAccessor.getBudgetById(id);
    }
}
