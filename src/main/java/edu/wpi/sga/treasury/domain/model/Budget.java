package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "budget")
@Getter
@Setter
public class Budget {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(optional = false)
    @JoinColumn(name = "organization_id", nullable = false)
    private Organization organization;

    @Column(name = "fiscal_year", nullable = false, length = 6)
    private String fiscalYear;

    @Column(name = "notes")
    private String notes;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

    @OneToOne(fetch = FetchType.LAZY, mappedBy = "budget")
    private BudgetLegacy budgetLegacy;

    @OneToMany(mappedBy = "budget")
    private List<BudgetSection> budgetSections = new ArrayList<>();

}