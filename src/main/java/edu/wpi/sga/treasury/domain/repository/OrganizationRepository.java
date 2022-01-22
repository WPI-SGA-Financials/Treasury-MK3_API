package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.Organization;
import org.springframework.data.jpa.repository.JpaRepository;

public interface OrganizationRepository extends JpaRepository<Organization, String> {
}