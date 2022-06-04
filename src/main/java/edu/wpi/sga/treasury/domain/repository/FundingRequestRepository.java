package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.funding_request.FundingRequest;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.List;

public interface FundingRequestRepository extends JpaRepository<FundingRequest, Integer>, JpaSpecificationExecutor<FundingRequest> {
    Page<FundingRequest> findAllByOrganizationIsInactiveIsFalseOrderByHearingDateDescDotNumberDesc(Pageable pageable);

    List<FundingRequest> findAllByOrganizationNameOrderByHearingDateDesc(String organization);

    Page<FundingRequest> findAll(Specification<FundingRequest> specification, Pageable pageable);
}