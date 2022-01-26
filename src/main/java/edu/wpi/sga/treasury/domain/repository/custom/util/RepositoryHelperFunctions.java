package edu.wpi.sga.treasury.domain.repository.custom.util;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;

import javax.persistence.criteria.*;
import java.util.ArrayList;
import java.util.List;

public class RepositoryHelperFunctions {
    public Predicate getOrgBasedPredicate(PagedRequest request, CriteriaBuilder cb, Join<?, ?> root) {
        Predicate filter = cb.conjunction();

        if (!request.getName().isEmpty()) {
            Path<String> path = root.get("name");

            List<Predicate> predicates = new ArrayList<>();
            request.getName().forEach(s -> predicates.add(cb.like(cb.lower(path), "%" + s.toLowerCase() + "%")));
            filter = cb.and(filter, cb.or(predicates.toArray(new Predicate[0])));
        }

        if (!request.getAcronym().isEmpty()) {
            Path<String> path = root.get("acronym");

            List<Predicate> predicates = new ArrayList<>();
            request.getAcronym().forEach(s -> predicates.add(cb.like(cb.lower(path), "%" + s.toLowerCase() + "%")));
            filter = cb.and(filter, cb.or(predicates.toArray(new Predicate[0])));
        }

        if (!request.getClassification().isEmpty()) {
            Path<String> path = root.get("classification");

            List<Predicate> predicates = new ArrayList<>();
            request.getClassification().forEach(s -> predicates.add(cb.like(cb.lower(path), s.toLowerCase())));
            filter = cb.and(filter, cb.or(predicates.toArray(new Predicate[0])));
        }

        if (!request.getType().isEmpty()) {
            Path<String> path = root.get("typeOfClub");

            List<Predicate> predicates = new ArrayList<>();
            request.getType().forEach(s -> predicates.add(cb.like(cb.lower(path), s.toLowerCase())));
            filter = cb.and(filter, cb.or(predicates.toArray(new Predicate[0])));
        }

        if(!request.isIncludeInactive()) {
            filter = cb.and(filter, cb.isFalse(root.get("inactive")));
        }

        return filter;
    }
}
