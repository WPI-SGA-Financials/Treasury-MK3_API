package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDate;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "fr_workday_idt")
public class FRWorkdayIdt {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @OneToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "funding_request_id", nullable = false)
    private FundingRequest fundingRequest;

    @Column(name = "idt_submitted")
    private Boolean idtSubmitted;

    @Column(name = "workday_approved", length = 15)
    private String workdayApproved;

    @Column(name = "workday_approval_date")
    private LocalDate workdayApprovalDate;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}