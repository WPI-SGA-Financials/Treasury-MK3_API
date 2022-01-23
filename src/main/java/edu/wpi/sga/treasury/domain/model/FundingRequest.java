package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

@Getter
@Setter
@Entity
@Table(name = "`Funding Requests`")
public class FundingRequest {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "ID", nullable = false)
    private Integer id;

    @ManyToOne(optional = false)
    @JoinColumn(name = "`Name of Club`", nullable = false)
    private Organization organization;

    @Column(name = "Description")
    private String description;

    @Column(name = "`Funding Date`", nullable = false)
    private LocalDate fundingDate;

    @Column(name = "`Fiscal Year`", length = 6)
    private String fiscalYear;

    @Column(name = "`Date of Event`")
    private LocalDate dateOfEvent;

    @Column(name = "`Dot Number`", nullable = false, length = 6)
    private String dotNumber;

    @Column(name = "`Amount Requested`", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountRequested;

    @Column(name = "Decision", nullable = false, length = 20)
    private String decision;

    @Column(name = "`Amount Approved`", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountApproved;

    @Column(name = "Notes", length = 512)
    private String notes;

    @Column(name = "Timestamp", nullable = false)
    private Instant timestamp;

}