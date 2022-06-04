package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.Organization;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.Optional;

public interface OrganizationRepository extends JpaRepository<Organization, Integer>, JpaSpecificationExecutor<Organization> {
    Page<Organization> findAllByIsInactiveIsFalse(Pageable page);

    Optional<Organization> findByName(String s);

    Page<Organization> findAll(Specification<Organization> specification, Pageable pageable);
}