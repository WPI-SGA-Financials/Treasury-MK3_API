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
@Table(name = "operating_expense")
public class OperatingExpense {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "mandatory_transfer_line_item_id", nullable = false)
    private MandatoryTransferLineItem mandatoryTransferLineItem;

    @Column(name = "amount_spent", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountSpent;

    @Column(name = "person", nullable = false, length = 100)
    private String person;

    @Column(name = "description", nullable = false)
    private String description;

    @Column(name = "payment_type", nullable = false, length = 20)
    private String paymentType;

    @Column(name = "workday_approved")
    private Boolean workdayApproved;

    @Column(name = "workday_approval_date")
    private LocalDate workdayApprovalDate;

    @Column(name = "notes")
    private String notes;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}