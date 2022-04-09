package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "student_life_fee")
public class StudentLifeFee {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @Column(name = "fiscal_year", nullable = false, length = 6)
    private String fiscalYear;

    @Column(name = "student_life_fee_amount", nullable = false, precision = 10, scale = 2)
    private BigDecimal studentLifeFeeAmount;

    @Column(name = "fall_student_amount")
    private Integer fallStudentAmount;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}