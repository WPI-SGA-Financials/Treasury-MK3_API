package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "Organizations")
@Getter
@Setter
public class Organization {
    @Id
    @Column(name = "`Name of Club`", nullable = false)
    private String name;

    @Column(name = "Classification", length = 100)
    private String classification;

    @Column(name = "`Type of Club`", length = 100)
    private String typeOfClub;

    @Column(name = "`Account Number`", length = 8)
    private String accountNumber;

    @Column(name = "`Acronym 1`", length = 50)
    private String acronym;

    @Column(name = "`Inactive?`", nullable = false)
    private Boolean inactive = false;

    @Column(name = "Timestamp", nullable = false)
    private LocalDateTime timestamp;

    @OneToMany(mappedBy = "organization")
    private List<Budget> budgets = new ArrayList<>();

    @OneToMany(mappedBy = "organization")
    private List<FundingRequest> fundingRequests = new ArrayList<>();

    @OneToMany(mappedBy = "organization")
    private List<ReallocationRequest> reallocationRequests = new ArrayList<>();

}