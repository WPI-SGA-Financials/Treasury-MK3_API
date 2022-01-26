package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.Budget;
import edu.wpi.sga.treasury.domain.repository.custom.BudgetRepositoryCustom;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface BudgetRepository extends JpaRepository<Budget, Integer>, BudgetRepositoryCustom {
    List<Budget> findAllByOrganizationNameIs(String name);
}