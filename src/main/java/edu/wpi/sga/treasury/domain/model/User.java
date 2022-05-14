package edu.wpi.sga.treasury.domain.model;

import edu.wpi.sga.treasury.application.enums.Role;
import edu.wpi.sga.treasury.application.enums.WorkingLevel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.Collection;

@Getter
@Setter
@Entity
@Table(name = "user")
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @Column(name = "first_name", nullable = false, length = 100)
    private String firstName;

    @Column(name = "last_name", nullable = false, length = 100)
    private String lastName;

    @Enumerated(EnumType.STRING)
    @Column(name = "role", nullable = false)
    private Role role;

    @Enumerated(EnumType.STRING)
    @Column(name = "working_level", nullable = false)
    private WorkingLevel workingLevel;

    @Column(name = "msal_id", nullable = false)
    private String msalId;

    @ManyToMany(cascade = CascadeType.PERSIST)
    @JoinTable(name = "organization_user",
            joinColumns = @JoinColumn(name = "user_id"),
            inverseJoinColumns = @JoinColumn(name = "organization_id"))
    private Collection<Organization> organizations = new ArrayList<>();

}