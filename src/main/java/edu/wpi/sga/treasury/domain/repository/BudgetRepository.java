package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.Budget;
import edu.wpi.sga.treasury.domain.repository.custom.BudgetRepositoryCustom;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface BudgetRepository extends JpaRepository<Budget, Integer>, BudgetRepositoryCustom {
    List<Budget> findAllByOrganizationNameIsOrderByFiscalYearDesc(String name);

    Page<Budget> findAllByOrganizationIsInactiveIsFalseOrderByOrganizationAscFiscalYearDesc(Pageable pageable);
}