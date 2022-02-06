package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.Instant;
import java.util.ArrayList;
import java.util.List;

@Getter
@Setter
@Entity
@Table(name = "BudgetSection")
public class BudgetSection {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "ID", nullable = false)
    private Integer id;

    @ManyToOne(optional = false)
    @JoinColumn(name = "B_ID", nullable = false)
    private Budget budget;

    @Column(name = "Section_Name", nullable = false)
    private String sectionName;

    @Column(name = "Timestamp", nullable = false)
    private Instant timestamp;

    @OneToMany(mappedBy = "budgetSection")
    private List<BudgetLineItem> budgetLineItems = new ArrayList<>();

}