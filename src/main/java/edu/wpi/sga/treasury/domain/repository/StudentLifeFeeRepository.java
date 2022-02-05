package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.StudentLifeFee;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface StudentLifeFeeRepository extends JpaRepository<StudentLifeFee, Integer> {
    Optional<StudentLifeFee> findByFiscalYear(String fy);
}