package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "organization_membership_number")
public class OrganizationMembershipNumber {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "organization_id", nullable = false)
    private Organization organization;

    @Column(name = "fiscal_year", nullable = false)
    private String fiscalYear;

    @Column(name = "amount_member_count", nullable = false)
    private String amountMemberCount;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}