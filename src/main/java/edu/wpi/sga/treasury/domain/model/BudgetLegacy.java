package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "BudgetLegacy")
public class BudgetLegacy {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "ID", nullable = false)
    private Integer id;

    @OneToOne(optional = false)
    @JoinColumn(name = "b_id", nullable = false)
    private Budget budget;

    @Column(name = "`Amount Requested`", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountRequested;

    @Column(name = "`Amount Proposed`", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountProposed;

    @Column(name = "Appealed", nullable = false)
    private Boolean appealed = false;

    @Column(name = "`Appeal Amount`", nullable = false, precision = 10, scale = 2)
    private BigDecimal appealAmount;

    @Column(name = "`Appeal Decision`", nullable = false, length = 20)
    private String appealDecision;

    @Column(name = "`Approved Appeal`", nullable = false, precision = 10, scale = 2)
    private BigDecimal approvedAppeal;

    @Column(name = "`Amount Spent`", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountSpent;

    @Column(name = "Timestamp", nullable = false)
    private LocalDateTime timestamp;

}