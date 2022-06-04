package edu.wpi.sga.treasury.domain.model.budget;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.Instant;
import java.util.ArrayList;
import java.util.List;

@Getter
@Setter
@Entity
@Table(name = "budget_section")
public class BudgetSection {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(optional = false)
    @JoinColumn(name = "budget_id", nullable = false)
    private Budget budget;

    @Column(name = "section_name", nullable = false)
    private String sectionName;

    @Column(name = "last_modified", nullable = false)
    private Instant lastModified;

    @OneToMany(mappedBy = "budgetSection")
    private List<BudgetLineItem> budgetLineItems = new ArrayList<>();

}