package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.FundingRequest;
import org.springframework.data.jpa.repository.JpaRepository;

public interface FundingRequestRepository extends JpaRepository<FundingRequest, Integer> {
}