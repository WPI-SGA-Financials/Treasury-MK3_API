package edu.wpi.sga.treasury.domain.specification.util;

import lombok.RequiredArgsConstructor;

import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.Path;
import javax.persistence.criteria.Predicate;
import java.util.ArrayList;
import java.util.List;

@RequiredArgsConstructor
public class SpecificationUtil {

    private final CriteriaBuilder cb;

    public Predicate filterLike(List<String> request, Path<String> path) {
        List<Predicate> predicates = new ArrayList<>();
        request.forEach(s -> predicates.add(cb.like(cb.lower(path), "%" + s.toLowerCase() + "%")));
        return cb.or(predicates.toArray(new Predicate[0]));
    }

    public Predicate filterIn(List<String> request, Path<String> path) {
        return cb.or(path.in(request));
    }
}
