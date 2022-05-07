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
@Table(name = "reallocation")
public class ReallocationRequest {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "organization_id", nullable = false)
    private Organization organization;

    @Column(name = "description", nullable = false)
    private String description;

    @Column(name = "hearing_date", nullable = false)
    private LocalDate hearingDate;

    @Column(name = "fiscal_year", nullable = false)
    private String fiscalYear;

    @Column(name = "dot_number", nullable = false)
    private String dotNumber;

    @Column(name = "allocated_from")
    private String allocatedFrom;

    @Column(name = "allocated_to")
    private String allocatedTo;

    @Column(name = "amount_allocated", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountAllocated;

    @Column(name = "decision", nullable = false)
    private String decision;

    @Column(name = "amount_approved", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountApproved;

    @Column(name = "notes")
    private String notes;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}