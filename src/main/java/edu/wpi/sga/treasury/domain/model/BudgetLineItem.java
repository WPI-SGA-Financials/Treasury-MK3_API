package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "BudgetLineItem")
public class BudgetLineItem {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "ID", nullable = false)
    private Integer id;

    @ManyToOne(optional = false)
    @JoinColumn(name = "BS_ID", nullable = false)
    private BudgetSection budgetSection;

    @Column(name = "Line_Item_Name", nullable = false)
    private String lineItemName;

    @Column(name = "Amount_Request", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountRequest;

    @Column(name = "Amount_Proposed", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountProposed;

    @Column(name = "Appealed", nullable = false)
    private Boolean appealed = false;

    @Column(name = "Appeal_Amount", nullable = false, precision = 10, scale = 2)
    private BigDecimal appealAmount;

    @Column(name = "Appeal_Decision", nullable = false, length = 20)
    private String appealDecision;

    @Column(name = "Approved_Appeal", nullable = false, precision = 10, scale = 2)
    private BigDecimal approvedAppeal;

    @Column(name = "Notes", nullable = false)
    private String notes;

    @Column(name = "Amount_Spent", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountSpent;

    @Column(name = "Timestamp", nullable = false)
    private LocalDateTime timestamp;

}