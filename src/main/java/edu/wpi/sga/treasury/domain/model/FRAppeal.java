package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "fr_appeal")
public class FRAppeal {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @OneToOne(optional = false)
    @JoinColumn(name = "funding_request_id", nullable = false)
    private FundingRequest fundingRequest;

    @Column(name = "new_dot_number", nullable = false, length = 6)
    private String newDotNumber;

    @Column(name = "appeal_date", nullable = false)
    private LocalDate appealDate;

    @Column(name = "description")
    private String description;

    @Column(name = "appeal_amount", nullable = false, precision = 10, scale = 2)
    private BigDecimal appealAmount;

    @Column(name = "decision", nullable = false, length = 20)
    private String decision;

    @Column(name = "approved_appeal", nullable = false, precision = 10, scale = 2)
    private BigDecimal approvedAppeal;

    @Column(name = "notes")
    private String notes;

    @Column(name = "minutes_link")
    private String minutesLink;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}