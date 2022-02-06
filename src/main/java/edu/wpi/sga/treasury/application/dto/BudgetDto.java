package edu.wpi.sga.treasury.application.dto;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;

@Getter
@Setter
@Builder
public class BudgetDto {
    private String nameOfClub;
    private Integer id;
    private String fiscalYear;
    private Integer numOfItems;
    private BigDecimal amountRequested;
    private BigDecimal amountProposed;
    private BigDecimal amountApproved;
}
