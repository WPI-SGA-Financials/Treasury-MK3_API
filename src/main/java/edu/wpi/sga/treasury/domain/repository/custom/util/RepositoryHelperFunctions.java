package edu.wpi.sga.treasury.domain.repository.custom.util;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.domain.model.Organization_;

import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.Join;
import javax.persistence.criteria.Path;
import javax.persistence.criteria.Predicate;
import java.util.ArrayList;
import java.util.List;

public class RepositoryHelperFunctions {
    public Predicate getOrgBasedPredicate(PagedRequest request, CriteriaBuilder cb, Join<?, ?> root) {
        List<Predicate> orgBasedPredicates = new ArrayList<>();

        if (!request.getName().isEmpty()) {
            Path<String> path = root.get(Organization_.NAME);

            filterLike(request.getName(), cb, path, orgBasedPredicates);
        }

        if (!request.getAcronym().isEmpty()) {
            Path<String> path = root.get(Organization_.ACRONYM);

            filterLike(request.getAcronym(), cb, path, orgBasedPredicates);
        }

        if (!request.getClassification().isEmpty()) {
            Path<String> path = root.get(Organization_.CLASSIFICATION);

            filterEqual(request.getType(), cb, path, orgBasedPredicates);
        }

        if (!request.getType().isEmpty()) {
            Path<String> path = root.get(Organization_.TYPE_OF_CLUB);

            filterEqual(request.getType(), cb, path, orgBasedPredicates);
        }

        if (!request.isIncludeInactive()) {
            orgBasedPredicates.add(cb.isFalse(root.get(Organization_.INACTIVE)));
        }

        return orgBasedPredicates.stream().reduce(cb::and).orElse(cb.conjunction());
    }

    public void filterLike(List<String> request, CriteriaBuilder cb, Path<String> path, List<Predicate> orgBasedPredicates) {
        List<Predicate> predicates = new ArrayList<>();
        request.forEach(s -> predicates.add(cb.like(cb.lower(path), "%" + s.toLowerCase() + "%")));
        orgBasedPredicates.add(cb.or(predicates.toArray(new Predicate[0])));
    }

    public void filterEqual(List<String> request, CriteriaBuilder cb, Path<String> path, List<Predicate> orgBasedPredicates) {
        List<Predicate> predicates = new ArrayList<>();
        request.forEach(s -> predicates.add(cb.equal(cb.lower(path), s.toLowerCase())));
        orgBasedPredicates.add(cb.or(predicates.toArray(new Predicate[0])));
    }
}
