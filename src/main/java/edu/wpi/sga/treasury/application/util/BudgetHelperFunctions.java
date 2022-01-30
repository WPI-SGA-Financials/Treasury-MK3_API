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
        BudgetDtoBuilder dto = BudgetDto.builder().nameOfClub(budget.getOrganization().getName()).id(budget.getId()).fiscalYear(budget.getFiscalYear());

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

        BudgetDetailedDto dto = BudgetDetailedDto.builder()
                .id(budgetDto.getId())
                .fiscalYear(budget.getFiscalYear())
                .numOfItems(budgetDto.getNumOfItems())
                .amountRequested(budgetDto.getAmountRequested())
                .amountProposed(budgetDto.getAmountProposed())
                .amountApproved(budgetDto.getAmountApproved()).build();

        if (budget.getBudgetLegacy() != null) {
            BudgetLegacy legacy = budget.getBudgetLegacy();
            dto.setAppealed(legacy.getAppealed());
            dto.setAppealAmount(legacy.getAppealAmount());
            dto.setAppealDecision(legacy.getAppealDecision());
            dto.setApprovedAppeal(legacy.getApprovedAppeal());
        } else {
            List<BudgetSection> sections = budget.getBudgetSections();
            dto.setSections(bsHelperFunctions.translateBudgetSectionsToBudgetSectionDtos(sections));
            dto.setAppealed(bsHelperFunctions.isAppealed(sections));
            dto.setAppealAmount(bsHelperFunctions.getAppealAmount(sections));
            dto.setApprovedAppeal(bsHelperFunctions.getApprovedAppealAmount(sections));

            dto.setAppealDecision(bsHelperFunctions.getAppealDecision(dto.getAppealAmount(), dto.getApprovedAppeal()));
        }

        return dto;
    }

    /* Private Helper Functions*/
    private BigDecimal getAmountApprovedFromLegacyBudget(BudgetLegacy legacy) {
        return legacy.getAmountProposed().add(legacy.getApprovedAppeal());
    }
}
