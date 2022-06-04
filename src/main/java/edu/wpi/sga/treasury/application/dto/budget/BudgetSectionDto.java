package edu.wpi.sga.treasury.application.dto.budget;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;

@Getter
@Setter
@Builder
public class BudgetSectionDto {
    private Integer id;
    private String sectionName;
    private Integer numOfItems;
    private BigDecimal amountRequested;
    private BigDecimal amountProposed;
    private BigDecimal amountApproved;

    private boolean appealed;
    private BigDecimal appealAmount;
    private String appealDecision;
    private BigDecimal approvedAppeal;
}
