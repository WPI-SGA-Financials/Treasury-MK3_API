package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "`Student Life Fee`")
public class StudentLifeFee {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "ID", nullable = false)
    private Integer id;

    @Column(name = "`Fiscal Year`", nullable = false, length = 6)
    private String fiscalYear;

    @Column(name = "`SLF Amount`", nullable = false, precision = 10, scale = 2)
    private BigDecimal slfAmount;

    @Column(name = "`Fall Student Amount`")
    private Integer fallStudentAmount;

    @Column(name = "Timestamp", nullable = false)
    private LocalDateTime timestamp;

}