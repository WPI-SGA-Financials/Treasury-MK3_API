package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "budget_line_item")
public class BudgetLineItem {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(optional = false)
    @JoinColumn(name = "budget_section_id", nullable = false)
    private BudgetSection budgetSection;

    @Column(name = "line_item_name", nullable = false)
    private String lineItemName;

    @Column(name = "amount_requested", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountRequest;

    @Column(name = "amount_proposed", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountProposed;

    @Column(name = "appealed", nullable = false)
    private Boolean appealed = false;

    @Column(name = "amount_appealed", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountAppealed;

    @Column(name = "appeal_decision", nullable = false, length = 20)
    private String appealDecision;

    @Column(name = "approved_appeal", nullable = false, precision = 10, scale = 2)
    private BigDecimal approvedAppeal;

    @Column(name = "notes", nullable = false)
    private String notes;

    @Column(name = "amount_spent", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountSpent;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}