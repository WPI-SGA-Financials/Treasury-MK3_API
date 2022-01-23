package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.repository.custom.OrganizationRepositoryCustom;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;

public interface OrganizationRepository extends JpaRepository<Organization, String>, OrganizationRepositoryCustom {
    Page<Organization> findAllByInactiveIsFalse(Pageable page);
}