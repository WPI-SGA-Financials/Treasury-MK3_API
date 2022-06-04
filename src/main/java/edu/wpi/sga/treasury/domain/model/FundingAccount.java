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
@Table(name = "funding_account")
public class FundingAccount {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @Column(name = "account_name", nullable = false)
    private String accountName;

    @Column(name = "fiscal_year", nullable = false)
    private String fiscalYear;

    @Column(name = "fall_transfer", precision = 10, scale = 2)
    private BigDecimal fallTransfer;

    @Column(name = "spring_transfer", precision = 10, scale = 2)
    private BigDecimal springTransfer;

    @Column(name = "worktag", nullable = false, length = 8)
    private String worktag;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

    @OneToMany(mappedBy = "fundingAccountFrom")
    private Set<Transfer> fromTransfers = new LinkedHashSet<>();

    @OneToMany(mappedBy = "fundingAccountTo")
    private Set<Transfer> toTransfers = new LinkedHashSet<>();

}