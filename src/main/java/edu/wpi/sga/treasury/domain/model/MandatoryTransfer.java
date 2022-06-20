package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.LinkedHashSet;
import java.util.Set;

@Getter
@Setter
@Entity
@Table(name = "mandatory_transfer")
public class MandatoryTransfer {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "organization_id", nullable = false)
    private Organization organization;

    @Column(name = "fund_name", nullable = false)
    private String fundName;

    @Column(name = "fiscal_year", nullable = false)
    private String fiscalYear;

    @Column(name = "worktag", nullable = false)
    private String worktag;

    @Column(name = "amount_requested", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountRequested;

    @Column(name = "amount_proposed", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountProposed;

    @Column(name = "amount_approved", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountApproved;

    @Column(name = "notes")
    private String notes;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

    @OneToMany(mappedBy = "mandatoryTransfer")
    private Set<MandatoryTransferLineItem> mandatoryTransferLineItems = new LinkedHashSet<>();

}