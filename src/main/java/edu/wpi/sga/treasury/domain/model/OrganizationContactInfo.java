package edu.wpi.sga.treasury.domain.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;

@Getter
@Setter
@Entity
@Table(name = "organization_contact_info")
public class OrganizationContactInfo {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "organization_id", nullable = false)
    private Organization organization;

    @Column(name = "president_email")
    private String presidentEmail;

    @Column(name = "treasurer_email")
    private String treasurerEmail;

    @Column(name = "last_modified", nullable = false)
    private LocalDateTime lastModified;

}