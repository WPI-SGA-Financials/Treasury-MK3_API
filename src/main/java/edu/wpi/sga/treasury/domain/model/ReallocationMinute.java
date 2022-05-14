package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "reallocation_minute")
public class ReallocationMinute {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "reallocation_id", nullable = false)
    private ReallocationRequest reallocation;

    @Column(name = "agenda_number", nullable = false, length = 9)
    private String agendaNumber;

    @Column(name = "minutes_link", nullable = false)
    private String minutesLink;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}