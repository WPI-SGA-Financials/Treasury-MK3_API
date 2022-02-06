package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "FRAppeals")
public class FRAppeal {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "ID", nullable = false)
    private Integer id;

    @OneToOne(optional = false)
    @JoinColumn(name = "FR_ID", nullable = false)
    private FundingRequest fundingRequest;

    @Column(name = "`New Dot Number`", nullable = false, length = 6)
    private String newDotNumber;

    @Column(name = "`Appeal Date`", nullable = false)
    private LocalDate appealDate;

    @Column(name = "Description")
    private String description;

    @Column(name = "`Appeal Amount`", nullable = false, precision = 10, scale = 2)
    private BigDecimal appealAmount;

    @Column(name = "Decision", nullable = false, length = 20)
    private String decision;

    @Column(name = "`Approved Appeal`", nullable = false, precision = 10, scale = 2)
    private BigDecimal approvedAppeal;

    @Column(name = "Notes")
    private String notes;

    @Column(name = "`Minutes Link`")
    private String minutesLink;

    @Column(name = "Timestamp", nullable = false)
    private LocalDateTime timestamp;

}