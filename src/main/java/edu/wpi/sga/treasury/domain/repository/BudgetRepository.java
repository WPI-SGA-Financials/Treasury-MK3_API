package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.budget.Budget;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.List;

public interface BudgetRepository extends JpaRepository<Budget, Integer>, JpaSpecificationExecutor<Budget> {
    List<Budget> findAllByOrganizationNameIsOrderByFiscalYearDesc(String name);

    Page<Budget> findAllByOrganizationIsInactiveIsFalseOrderByOrganizationAscFiscalYearDesc(Pageable pageable);

    Page<Budget> findAll(Specification<Budget> specification, Pageable pageable);
}