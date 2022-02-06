package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.api.contract.response.PagedResponse;
import edu.wpi.sga.treasury.api.contract.response.Response;
import edu.wpi.sga.treasury.application.accessor.BudgetAccessor;
import edu.wpi.sga.treasury.application.dto.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.BudgetDto;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequiredArgsConstructor
public class BudgetController {
    private final BudgetAccessor budgetAccessor;

    @PostMapping(value = "/financials/budgets", produces = MediaType.APPLICATION_JSON_VALUE)
    public PagedResponse<BudgetDto> getBudgets(@RequestBody PagedRequest request) {
        Page<BudgetDto> data = budgetAccessor.getBudgets(request);

        return PagedResponse.<BudgetDto>builder()
                .data(data.getContent())
                .maxResults(Math.toIntExact(data.getTotalElements()))
                .pageNumber(request.getPage())
                .resultsPerPage(request.getResultsPerPage())
                .message("Successfully got " + data.getNumberOfElements() + " Budgets")
                .build();
    }

    @GetMapping(value = "/organization/{name}/budgets", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<List<BudgetDto>> getOrganizationBudgets(@PathVariable String name) {
        List<BudgetDto> budgets = budgetAccessor.getBudgetsForOrganization(name);

        return Response.<List<BudgetDto>>builder()
                .data(budgets)
                .message("Successfully returned the organizations budgets")
                .build();
    }

    @GetMapping(value = "/financials/budget/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<BudgetDetailedDto> getBudgetById(@PathVariable Integer id) {
        BudgetDetailedDto dto = budgetAccessor.getBudgetById(id);

        return Response.<BudgetDetailedDto>builder()
                .data(dto)
                .message("Successfully returned the budget")
                .build();
    }
}
