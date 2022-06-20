package edu.wpi.sga.treasury.domain.model.funding_request;

import edu.wpi.sga.treasury.domain.model.Organization;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "funding_request")
public class FundingRequest {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(optional = false)
    @JoinColumn(name = "organization_id", nullable = false)
    private Organization organization;

    @Column(name = "description")
    private String description;

    @Column(name = "hearing_date", nullable = false)
    private LocalDate hearingDate;

    @Column(name = "fiscal_year", length = 6)
    private String fiscalYear;

    @Column(name = "date_of_event")
    private LocalDate dateOfEvent;

    @Column(name = "dot_number", nullable = false, length = 6)
    private String dotNumber;

    @Column(name = "amount_requested", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountRequested;

    @Column(name = "decision", nullable = false, length = 20)
    private String decision;

    @Column(name = "amount_approved", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountApproved;

    @Column(name = "notes", length = 512)
    private String notes;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

    @OneToOne(fetch = FetchType.LAZY, mappedBy = "fundingRequest")
    private FRAppeal frAppeal;
}