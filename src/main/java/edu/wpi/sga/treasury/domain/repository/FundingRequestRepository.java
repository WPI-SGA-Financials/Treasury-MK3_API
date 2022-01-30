package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.FundingRequest;
import edu.wpi.sga.treasury.domain.repository.custom.FundingRequestRepositoryCustom;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;

public interface FundingRequestRepository extends JpaRepository<FundingRequest, Integer>, FundingRequestRepositoryCustom {
    Page<FundingRequest> findAllByOrderByFundingDateDescDotNumberDesc(Pageable pageable);
}