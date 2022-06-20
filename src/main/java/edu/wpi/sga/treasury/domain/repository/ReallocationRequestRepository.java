package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.ReallocationRequest;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.List;

public interface ReallocationRequestRepository extends JpaRepository<ReallocationRequest, Integer>, JpaSpecificationExecutor<ReallocationRequest> {
    List<ReallocationRequest> findAllByOrganizationNameOrderByHearingDateDesc(String organization);

    Page<ReallocationRequest> findAllByOrganizationIsInactiveIsFalseOrderByHearingDateDescDotNumberDesc(Pageable pageable);
}