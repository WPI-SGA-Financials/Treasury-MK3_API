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
@Table(name = "Reallocations")
public class ReallocationRequest {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "ID", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "`Name of Club`", nullable = false)
    private Organization organization;

    @Column(name = "Description", nullable = false)
    private String description;

    @Column(name = "`Hearing Date`", nullable = false)
    private LocalDate hearingDate;

    @Column(name = "`Fiscal Year`", nullable = false)
    private String fiscalYear;

    @Column(name = "`Dot Number`", nullable = false)
    private String dotNumber;

    @Column(name = "`Allocated From`")
    private String allocatedFrom;

    @Column(name = "`Allocated To`")
    private String allocatedTo;

    @Column(name = "`Allocation Amount`", nullable = false, precision = 10, scale = 2)
    private BigDecimal allocationAmount;

    @Column(name = "Decision", nullable = false)
    private String decision;

    @Column(name = "`Amount Approved`", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountApproved;

    @Column(name = "Notes")
    private String notes;

    @Column(name = "Timestamp", nullable = false)
    private LocalDateTime timestamp;

}