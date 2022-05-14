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
@Table(name = "fr_report_form")
public class FRReportForm {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @OneToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "funding_request_id", nullable = false)
    private FundingRequest fundingRequest;

    @Column(name = "amount_spent", precision = 10, scale = 2)
    private BigDecimal amountSpent;

    @Column(name = "status", length = 25)
    private String status;

    @Column(name = "amount_approved", precision = 10, scale = 2)
    private BigDecimal amountApproved;

    @Column(name = "approved_date")
    private LocalDate approvedDate;

    @Column(name = "notes")
    private String notes;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}