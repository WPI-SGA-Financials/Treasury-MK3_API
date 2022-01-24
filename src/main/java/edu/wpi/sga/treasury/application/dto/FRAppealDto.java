package edu.wpi.sga.treasury.application.dto;

import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;
import java.time.LocalDate;

@Getter
@Setter
public class FRAppealDto {
    private Integer id;
    private String newDotNumber;
    private LocalDate appealDate;
    private String description;
    private BigDecimal appealAmount;
    private String decision;
    private BigDecimal approvedAppeal;
    private String minutesLink;
}
