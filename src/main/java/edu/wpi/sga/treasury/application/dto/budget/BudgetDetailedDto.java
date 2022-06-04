package edu.wpi.sga.treasury.application.dto.budget;

import lombok.*;

import java.math.BigDecimal;
import java.util.List;

@Getter
@Setter
@Builder
public class BudgetDetailedDto {
    private Integer id;
    private String nameOfClub;
    private String fiscalYear;
    private Integer numOfItems;
    private BigDecimal amountRequested;
    private BigDecimal amountProposed;
    private BigDecimal amountApproved;

    private List<BudgetSectionDto> sections;
    private boolean appealed;
    private BigDecimal appealAmount;
    private String appealDecision;
    private BigDecimal approvedAppeal;
}
