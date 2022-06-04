package edu.wpi.sga.treasury.domain.specification;

import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.model.ReallocationRequest;
import edu.wpi.sga.treasury.domain.model.ReallocationRequest_;
import edu.wpi.sga.treasury.domain.specification.util.SpecificationUtil;
import lombok.Builder;
import org.springframework.data.jpa.domain.Specification;

import javax.persistence.criteria.*;
import java.util.List;

@Builder
public class ReallocationSpecification implements Specification<ReallocationRequest> {

    private final PagedRequest request;

    @Override
    public Predicate toPredicate(Root<ReallocationRequest> root, CriteriaQuery<?> query, CriteriaBuilder cb) {
        SpecificationUtil<ReallocationRequest> util = new SpecificationUtil<>(cb);

        Join<ReallocationRequest, Organization> organizationJoin = root.join(ReallocationRequest_.ORGANIZATION);

        List<Order> order = List.of(
                cb.desc(root.get(ReallocationRequest_.HEARING_DATE)),
                cb.desc(root.get(ReallocationRequest_.DOT_NUMBER))
        );

        List<Predicate> predicates = util.getOrgBasedPredicate(request, organizationJoin);

        if (!request.getFiscalYear().isEmpty()) {
            Path<String> path = root.get(ReallocationRequest_.FISCAL_YEAR);

            predicates.add(util.filterIn(request.getFiscalYear(), path));
        }

        if (!request.getDescription().isEmpty()) {
            Path<String> path = root.get(ReallocationRequest_.DESCRIPTION);

            predicates.add(util.filterLike(request.getDescription(), path));
        }

        if (!request.getDotNumber().isEmpty()) {
            Path<String> path = root.get(ReallocationRequest_.DOT_NUMBER);

            predicates.add(util.filterLike(request.getDotNumber(), path));
        }

        return query.where(cb.and(predicates.toArray(new Predicate[0])))
                .orderBy(order)
                .getRestriction();
    }
}
