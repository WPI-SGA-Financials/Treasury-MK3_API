package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "reclassification_minute")
public class ReclassificationMinute {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "reclassification_id", nullable = false)
    private Reclassification reclassification;

    @Column(name = "agenda_number", nullable = false, length = 9)
    private String agendaNumber;

    @Column(name = "minutes_link", nullable = false)
    private String minutesLink;

    @Column(name = "last_modified")
    private LocalDateTime lastModified;

}