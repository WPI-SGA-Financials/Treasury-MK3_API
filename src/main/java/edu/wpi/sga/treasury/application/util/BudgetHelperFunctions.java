package edu.wpi.sga.treasury.application.util;

import edu.wpi.sga.treasury.application.dto.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.BudgetDto;
import edu.wpi.sga.treasury.application.dto.BudgetDto.BudgetDtoBuilder;
import edu.wpi.sga.treasury.domain.model.Budget;
import edu.wpi.sga.treasury.domain.model.BudgetLegacy;
import edu.wpi.sga.treasury.domain.model.BudgetSection;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Component;

import java.math.BigDecimal;
import java.util.List;

@Component
@RequiredArgsConstructor
public class BudgetHelperFunctions {
    // Utils
    private final BudgetSectionHelperFunctions bsHelperFunctions;

    public BudgetDto translateBudgetToBudgetDto(Budget budget) {
        BudgetDtoBuilder dto = BudgetDto.builder().id(budget.getId()).fiscalYear(budget.getFiscalYear());

        if (budget.getBudgetLegacy() != null) {
            BudgetLegacy legacy = budget.getBudgetLegacy();
            dto = dto.numOfItems(-1)
                    .amountRequested(legacy.getAmountRequested())
                    .amountProposed(legacy.getAmountProposed())
                    .amountApproved(getAmountApprovedFromLegacyBudget(legacy));
        } else {
            List<BudgetSection> sections = budget.getBudgetSections();
            dto = dto.numOfItems(bsHelperFunctions.getLineItemCount(sections))
                    .amountRequested(bsHelperFunctions.getAmountRequested(sections))
                    .amountProposed(bsHelperFunctions.getAmountProposed(sections))
                    .amountApproved(bsHelperFunctions.getAmountApproved(sections));
        }

        return dto.build();
    }

    public BudgetDetailedDto translateBudgetToBudgetDetailedDto(Budget budget) {
        BudgetDto budgetDto = translateBudgetToBudgetDto(budget);

        BudgetDetailedDto.BudgetDetailedDtoBuilder dto = BudgetDetailedDto.builder()
                .id(budgetDto.getId())
                .numOfItems(budgetDto.getNumOfItems())
                .amountRequested(budgetDto.getAmountRequested())
                .amountProposed(budgetDto.getAmountProposed())
                .amountApproved(budgetDto.getAmountApproved());

        if (budget.getBudgetLegacy() != null) {
            BudgetLegacy legacy = budget.getBudgetLegacy();
            dto.appealed(legacy.getAppealed())
                    .appealAmount(legacy.getAppealAmount())
                    .appealDecision(legacy.getAppealDecision())
                    .approvedAppeal(legacy.getApprovedAppeal());
        } else {
            List<BudgetSection> sections = budget.getBudgetSections();
            dto.sections(bsHelperFunctions.translateBudgetSectionsToBudgetSectionDtos(sections))
                    .approvedAppeal(bsHelperFunctions.getApprovedAppealAmount(sections));
        }

        return dto.build();
    }


    /* Private Helper Functions*/
    private BigDecimal getAmountApprovedFromLegacyBudget(BudgetLegacy legacy) {
        return legacy.getAmountProposed().add(legacy.getApprovedAppeal());
    }
}
