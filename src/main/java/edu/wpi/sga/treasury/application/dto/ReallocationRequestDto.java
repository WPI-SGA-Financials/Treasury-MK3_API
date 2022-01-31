package edu.wpi.sga.treasury.application.dto;

import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.LocalDateTime;

@Getter
@Setter
public class ReallocationRequestDto {
    private Integer id;
    private String organizationName;
    private LocalDate hearingDate;
    private String fiscalYear;
    private String dotNumber;
    private String description;
    private BigDecimal allocationAmount;
    private String decision;
    private BigDecimal amountApproved;
}
