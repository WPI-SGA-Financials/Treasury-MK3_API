package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "organization")
@Getter
@Setter
public class Organization {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @Column(name = "name", nullable = false)
    private String name;

    @Column(name = "classification", length = 100)
    private String classification;

    @Column(name = "type_of_club", length = 100)
    private String typeOfClub;

    @Column(name = "account_number", length = 8)
    private String accountNumber;

    @Column(name = "acronym", length = 50)
    private String acronym;

    @Column(name = "is_inactive", nullable = false)
    private Boolean isInactive = false;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

    @OneToMany(mappedBy = "organization")
    private List<Budget> budgets = new ArrayList<>();

    @OneToMany(mappedBy = "organization")
    private List<FundingRequest> fundingRequests = new ArrayList<>();

    @OneToMany(mappedBy = "organization")
    private List<ReallocationRequest> reallocationRequests = new ArrayList<>();

}