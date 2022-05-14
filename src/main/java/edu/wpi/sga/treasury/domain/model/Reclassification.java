package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.LinkedHashSet;
import java.util.Set;

@Getter
@Setter
@Entity
@Table(name = "reclassification")
public class Reclassification {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "organization_id", nullable = false)
    private Organization organization;

    @Column(name = "hearing_date", nullable = false)
    private LocalDate hearingDate;

    @Column(name = "fiscal_year", nullable = false)
    private String fiscalYear;

    @Column(name = "dot_number", nullable = false)
    private String dotNumber;

    @Column(name = "original_class", nullable = false)
    private String originalClass;

    @Column(name = "requested_class", nullable = false)
    private String requestedClass;

    @Column(name = "decision", nullable = false)
    private String decision;

    @Column(name = "approved_class", nullable = false)
    private String approvedClass;

    @Column(name = "notes")
    private String notes;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

    @OneToMany(mappedBy = "reclassification")
    private Set<ReclassificationMinute> reclassificationMinutes = new LinkedHashSet<>();

}