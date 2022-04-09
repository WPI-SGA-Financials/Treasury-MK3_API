package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.ReallocationRequest;
import edu.wpi.sga.treasury.domain.repository.custom.ReallocationRequestRepositoryCustom;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface ReallocationRequestRepository extends JpaRepository<ReallocationRequest, Integer>, ReallocationRequestRepositoryCustom {
    Optional<List<ReallocationRequest>> findAllByOrganizationNameOrderByHearingDateDesc(String organization);

    Page<ReallocationRequest> findAllByOrganizationIsInactiveIsFalseOrderByHearingDateDescDotNumberDesc(Pageable pageable);
}