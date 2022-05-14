package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDate;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "student_organization_council")
public class StudentOrganizationCouncil {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @Column(name = "name_of_club", nullable = false)
    private String nameOfClub;

    @Column(name = "acronym")
    private String acronym;

    @Column(name = "hearing_date", nullable = false)
    private LocalDate hearingDate;

    @Column(name = "fiscal_year", nullable = false)
    private String fiscalYear;

    @Column(name = "type_of_club", nullable = false)
    private String typeOfClub;

    @Column(name = "president_email")
    private String presidentEmail;

    @Column(name = "treasurer_email")
    private String treasurerEmail;

    @Column(name = "projected_active_members")
    private Integer projectedActiveMembers;

    @Column(name = "decision", nullable = false)
    private String decision;

    @Column(name = "notes")
    private String notes;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "organization_id")
    private Organization organization;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}