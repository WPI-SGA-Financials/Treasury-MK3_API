package edu.wpi.sga.treasury.application.mapper.custom;

import edu.wpi.sga.treasury.application.dto.budget.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDto;
import edu.wpi.sga.treasury.application.util.BudgetSectionHelperFunctions;
import edu.wpi.sga.treasury.domain.model.budget.Budget;
import edu.wpi.sga.treasury.domain.model.budget.BudgetLegacy;
import edu.wpi.sga.treasury.domain.model.budget.BudgetSection;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.math.BigDecimal;
import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class BudgetMapperCustomImpl implements BudgetMapperCustom {
    private final BudgetSectionHelperFunctions bsHelperFunctions;

    @Override
    public List<BudgetDto> toBudgetDtos(List<Budget> budgets) {
        return budgets.stream().map(this::toBudgetDto).collect(Collectors.toList());
    }

    @Override
    public BudgetDto toBudgetDto(Budget budget) {
        BudgetDto.BudgetDtoBuilder dto = BudgetDto.builder().nameOfClub(budget.getOrganization().getName()).id(budget.getId()).fiscalYear(budget.getFiscalYear());

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

    @Override
    public BudgetDetailedDto toBudgetDetailedDto(Budget budget) {
        BudgetDto budgetDto = toBudgetDto(budget);

        BudgetDetailedDto dto = BudgetDetailedDto.builder()
                .id(budgetDto.getId())
                .nameOfClub(budgetDto.getNameOfClub())
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

    private BigDecimal getAmountApprovedFromLegacyBudget(BudgetLegacy legacy) {
        return legacy.getAmountProposed().add(legacy.getApprovedAppeal());
    }

}
