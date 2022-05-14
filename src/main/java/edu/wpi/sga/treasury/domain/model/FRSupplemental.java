package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "fr_supplemental")
public class FRSupplemental {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "funding_request_id", nullable = false)
    private FundingRequest fundingRequest;

    @Column(name = "item_type", nullable = false, length = 100)
    private String itemType;

    @Column(name = "other_type", length = 100)
    private String otherType;

    @Column(name = "amount_requested", nullable = false, precision = 10, scale = 2)
    private BigDecimal amountRequested;

    @Column(name = "amended")
    private Boolean amended;

    @Column(name = "amended_amount", precision = 10, scale = 2)
    private BigDecimal amendedAmount;

    @Column(name = "notes")
    private String notes;

    @Column(name = "last_modified")
    private LocalDateTime lastModified;

}