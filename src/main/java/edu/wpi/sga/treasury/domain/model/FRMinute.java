package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "fr_minute")
public class FRMinute {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @OneToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "funding_request_id", nullable = false)
    private FundingRequest fundingRequest;

    @Column(name = "agenda_number", nullable = false, length = 9)
    private String agendaNumber;

    @Column(name = "minutes_link")
    private String minutesLink;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}