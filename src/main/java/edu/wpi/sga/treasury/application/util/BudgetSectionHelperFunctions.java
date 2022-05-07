package edu.wpi.sga.treasury.application.util;

import edu.wpi.sga.treasury.application.dto.BudgetSectionDto;
import edu.wpi.sga.treasury.domain.model.BudgetLineItem;
import edu.wpi.sga.treasury.domain.model.BudgetSection;
import org.springframework.stereotype.Component;

import java.math.BigDecimal;
import java.util.List;
import java.util.stream.Collectors;

@Component
public class BudgetSectionHelperFunctions {

    public List<BudgetSectionDto> translateBudgetSectionsToBudgetSectionDtos(List<BudgetSection> budgetSections) {
        return budgetSections.stream().map(this::translateBudgetSectionToBudgetSectionDto).collect(Collectors.toList());
    }

    public Integer getLineItemCount(List<BudgetSection> sections) {
        return sections
                .stream()
                .map(budgetSection -> budgetSection.getBudgetLineItems().size())
                .reduce(0, Integer::sum);
    }

    public BigDecimal getAmountRequested(List<BudgetSection> sections) {
        return sections
                .stream()
                .map(this::getAmountRequestedFromSection)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    public BigDecimal getAmountProposed(List<BudgetSection> sections) {
        return sections
                .stream()
                .map(this::getAmountProposedFromSection)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    public boolean isAppealed(List<BudgetSection> sections) {
        return sections
                .stream()
                .anyMatch(this::isAppealedFromSection);
    }

    public BigDecimal getAppealAmount(List<BudgetSection> sections) {
        return sections
                .stream()
                .map(this::getAppealAmountFromSection)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    public BigDecimal getApprovedAppealAmount(List<BudgetSection> sections) {
        return sections
                .stream()
                .map(this::getApprovedAppealAmountFromSection)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    public String getAppealDecision(BigDecimal requested, BigDecimal approvedAppeal) {
        if(requested.compareTo(BigDecimal.ZERO) == 0) {
            return "N/A";
        }
        if (requested.compareTo(approvedAppeal) == 0) {
            return "Passed in Full";
        }
        if(requested.compareTo(approvedAppeal) > 0 && approvedAppeal.compareTo(BigDecimal.ZERO) > 0) {
            return "Partially Passed";
        }
        return "Denied";
    }

    public BigDecimal getAmountApproved(List<BudgetSection> sections) {
        return getAmountProposed(sections).add(getApprovedAppealAmount(sections));
    }

    /* Private Helper Functions */
    private BudgetSectionDto translateBudgetSectionToBudgetSectionDto(BudgetSection budgetSection) {
        BudgetSectionDto dto = BudgetSectionDto.builder()
                .id(budgetSection.getId())
                .sectionName(budgetSection.getSectionName())
                .build();

        dto.setNumOfItems(budgetSection.getBudgetLineItems().size());
        dto.setAmountRequested(getAmountRequestedFromSection(budgetSection));
        dto.setAmountProposed(getAmountProposedFromSection(budgetSection));
        dto.setAppealed(isAppealedFromSection(budgetSection));
        dto.setAppealAmount(getAppealAmountFromSection(budgetSection));
        dto.setApprovedAppeal(getApprovedAppealAmountFromSection(budgetSection));
        dto.setAppealDecision(getAppealDecision(dto.getAppealAmount(), dto.getApprovedAppeal()));
        dto.setAmountApproved(getAmountApprovedFromSection(budgetSection));

        return dto;
    }

    private BigDecimal getAmountRequestedFromSection(BudgetSection budgetSection) {
        return budgetSection.getBudgetLineItems().stream()
                .map(BudgetLineItem::getAmountRequest)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    private BigDecimal getAmountProposedFromSection(BudgetSection budgetSection) {
        return budgetSection.getBudgetLineItems().stream()
                .map(BudgetLineItem::getAmountProposed)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    private boolean isAppealedFromSection(BudgetSection budgetSection) {
        return budgetSection.getBudgetLineItems().stream()
                .anyMatch(BudgetLineItem::getAppealed);
    }

    private BigDecimal getAppealAmountFromSection(BudgetSection budgetSection) {
        return budgetSection.getBudgetLineItems().stream()
                .map(BudgetLineItem::getAmountAppealed)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    private BigDecimal getApprovedAppealAmountFromSection(BudgetSection budgetSection) {
        return budgetSection.getBudgetLineItems().stream()
                .map(BudgetLineItem::getApprovedAppeal)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    private BigDecimal getAmountApprovedFromSection(BudgetSection section) {
        return getAmountProposedFromSection(section).add(getApprovedAppealAmountFromSection(section));
    }
}
