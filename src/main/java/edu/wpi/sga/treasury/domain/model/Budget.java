package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "Budget")
@Getter
@Setter
public class Budget {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "ID", nullable = false)
    private Integer id;

    @ManyToOne(optional = false)
    @JoinColumn(name = "`Name of Club`", nullable = false)
    private Organization organization;

    @Column(name = "`Fiscal Year`", nullable = false, length = 6)
    private String fiscalYear;

    @Column(name = "Notes")
    private String notes;

    @Column(name = "Timestamp", nullable = false)
    private LocalDateTime timestamp;

    @OneToOne(fetch = FetchType.LAZY, mappedBy = "budget")
    private BudgetLegacy budgetLegacy;

    @OneToMany(mappedBy = "budget")
    private List<BudgetSection> budgetSections = new ArrayList<>();

}