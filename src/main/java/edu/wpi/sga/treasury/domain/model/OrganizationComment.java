package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDate;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "organization_comment")
public class OrganizationComment {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "organization_id", nullable = false)
    private Organization organization;

    @Column(name = "comment_date", nullable = false)
    private LocalDate commentDate;

    @Column(name = "comment", nullable = false)
    private String comment;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}