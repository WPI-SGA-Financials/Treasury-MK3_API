package edu.wpi.sga.treasury.application.dto;

import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;
import java.time.LocalDate;

@Getter
@Setter
public class ReallocationRequestDetailedDto {
    private Integer id;
    private String organizationName;
    private String description;
    private LocalDate hearingDate;
    private String fiscalYear;
    private String dotNumber;
    private String allocatedFrom;
    private String allocatedTo;
    private BigDecimal allocationAmount;
    private String decision;
    private BigDecimal amountApproved;
}
