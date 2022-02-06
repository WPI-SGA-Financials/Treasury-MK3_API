package edu.wpi.sga.treasury.domain.repository.custom.util;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.domain.model.Organization_;
import lombok.RequiredArgsConstructor;

import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.Join;
import javax.persistence.criteria.Path;
import javax.persistence.criteria.Predicate;
import java.util.ArrayList;
import java.util.List;

@RequiredArgsConstructor
public class RepositoryHelperFunctions {
    private final CriteriaBuilder cb;

    public List<Predicate> getOrgBasedPredicate(PagedRequest request, Join<?, ?> root) {
        List<Predicate> orgBasedPredicates = new ArrayList<>();

        if (!request.getName().isEmpty()) {
            Path<String> path = root.get(Organization_.NAME);

            orgBasedPredicates.add(filterLike(request.getName(), path));
        }

        if (!request.getAcronym().isEmpty()) {
            Path<String> path = root.get(Organization_.ACRONYM);

            orgBasedPredicates.add(filterLike(request.getAcronym(), path));
        }

        if (!request.getClassification().isEmpty()) {
            Path<String> path = root.get(Organization_.CLASSIFICATION);

            orgBasedPredicates.add(filterEqual(request.getClassification(), path));
        }

        if (!request.getType().isEmpty()) {
            Path<String> path = root.get(Organization_.TYPE_OF_CLUB);

            orgBasedPredicates.add(filterEqual(request.getType(), path));
        }

        if (!request.isIncludeInactive()) {
            orgBasedPredicates.add(cb.isFalse(root.get(Organization_.INACTIVE)));
        }

        return orgBasedPredicates;
    }

    public Predicate filterLike(List<String> request, Path<String> path) {
        List<Predicate> predicates = new ArrayList<>();
        request.forEach(s -> predicates.add(cb.like(cb.lower(path), "%" + s.toLowerCase() + "%")));
        return cb.or(predicates.toArray(new Predicate[0]));
    }

    public Predicate filterEqual(List<String> request, Path<String> path) {
        List<Predicate> predicates = new ArrayList<>();
        request.forEach(s -> predicates.add(cb.equal(cb.lower(path), s.toLowerCase())));
        return cb.or(predicates.toArray(new Predicate[0]));
    }
}
