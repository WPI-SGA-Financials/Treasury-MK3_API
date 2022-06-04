package edu.wpi.sga.treasury.domain.specification;

import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.model.Organization_;
import edu.wpi.sga.treasury.domain.specification.util.SpecificationUtil;
import lombok.Builder;
import org.springframework.data.jpa.domain.Specification;

import javax.persistence.criteria.*;
import java.util.ArrayList;
import java.util.List;

@Builder
public class OrganizationSpecification implements Specification<Organization> {

    private PagedRequest request;

    @Override
    public Predicate toPredicate(Root<Organization> root, CriteriaQuery<?> query, CriteriaBuilder cb) {
        SpecificationUtil util = new SpecificationUtil(cb);

        List<Predicate> predicates = new ArrayList<>();

        if (!request.getName().isEmpty()) {
            Path<String> path = root.get(Organization_.NAME);

            predicates.add(util.filterIn(request.getName(), path));
        }

        if (!request.getAcronym().isEmpty()) {
            Path<String> path = root.get(Organization_.ACRONYM);

            predicates.add(util.filterLike(request.getAcronym(), path));
        }

        if (!request.getClassification().isEmpty()) {
            Path<String> path = root.get(Organization_.CLASSIFICATION);

            predicates.add(util.filterIn(request.getClassification(), path));
        }

        if (!request.getType().isEmpty()) {
            Path<String> path = root.get(Organization_.TYPE_OF_CLUB);

            predicates.add(util.filterIn(request.getType(), path));
        }

        if(!request.isIncludeInactive()) {
            predicates.add(cb.isFalse(root.get(Organization_.IS_INACTIVE)));
        }

        return query.where(cb.and(predicates.toArray(new Predicate[0]))).getRestriction();
    }
}
