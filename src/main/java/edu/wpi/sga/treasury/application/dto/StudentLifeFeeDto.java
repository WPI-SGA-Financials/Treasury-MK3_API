package edu.wpi.sga.treasury.application.dto;

import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;

@Getter
@Setter
public class StudentLifeFeeDto {
    private Integer id;
    private String fiscalYear;
    private BigDecimal slfAmount;
    private Integer fallStudentAmount;
}
