package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.time.LocalDateTime;

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

}