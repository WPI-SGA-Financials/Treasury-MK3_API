package edu.wpi.sga.treasury.domain.repository.custom;

import edu.wpi.sga.treasury.api.contract.request.GeneralPagedRequest;
import edu.wpi.sga.treasury.domain.model.Organization;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.PageRequest;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.criteria.*;
import java.util.ArrayList;
import java.util.List;

public class OrganizationRepositoryCustomImpl implements OrganizationRepositoryCustom {
    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public Page<Organization> findOrganizationsByFilters(GeneralPagedRequest request) {
        CriteriaBuilder cb = entityManager.getCriteriaBuilder();
        CriteriaQuery<Organization> query = cb.createQuery(Organization.class);
        Root<Organization> organization = query.from(Organization.class);

        // Construct Filter
        Predicate filter = getPredicate(request, cb, organization);

        query.select(organization).where(filter);

        int startPage = (request.getPage() * request.getResultsPerPage()) - request.getResultsPerPage();

        // Get Organizations based on Filter and pagination
        List<Organization> organizations = entityManager
                .createQuery(query)
                .setFirstResult(startPage)
                .setMaxResults(request.getResultsPerPage())
                .getResultList();

        // Get total amount matching filter
        Long count = getCount(cb, filter);

        return new PageImpl<>(organizations, PageRequest.of(request.getPage() - 1, request.getResultsPerPage()), count);
    }

    private Long getCount(CriteriaBuilder cb, Predicate filter) {
        CriteriaQuery<Long> countQuery = cb.createQuery(Long.class);
        Root<Organization> organizationCount = countQuery.from(Organization.class);
        countQuery.select(cb.count(organizationCount)).where(cb.and(filter));

        return entityManager.createQuery(countQuery).getSingleResult();
    }

    private Predicate getPredicate(GeneralPagedRequest request, CriteriaBuilder cb, Root<Organization> organization) {
        Predicate filter = cb.conjunction();

        if (!request.getName().isEmpty()) {
            Path<String> path = organization.get("name");

            List<Predicate> predicates = new ArrayList<>();
            request.getName().forEach(s -> predicates.add(cb.like(cb.lower(path), "%" + s.toLowerCase() + "%")));
            filter = cb.and(filter, cb.or(predicates.toArray(new Predicate[0])));
        }

        if (!request.getAcronym().isEmpty()) {
            Path<String> path = organization.get("acronym");

            List<Predicate> predicates = new ArrayList<>();
            request.getAcronym().forEach(s -> predicates.add(cb.like(cb.lower(path), "%" + s.toLowerCase() + "%")));
            filter = cb.and(filter, cb.or(predicates.toArray(new Predicate[0])));
        }

        if (!request.getClassification().isEmpty()) {
            Path<String> path = organization.get("classification");

            List<Predicate> predicates = new ArrayList<>();
            request.getClassification().forEach(s -> predicates.add(cb.like(cb.lower(path), s.toLowerCase())));
            filter = cb.and(filter, cb.or(predicates.toArray(new Predicate[0])));
        }

        if (!request.getType().isEmpty()) {
            Path<String> path = organization.get("typeOfClub");

            List<Predicate> predicates = new ArrayList<>();
            request.getType().forEach(s -> predicates.add(cb.like(cb.lower(path), s.toLowerCase())));
            filter = cb.and(filter, cb.or(predicates.toArray(new Predicate[0])));
        }

        if(!request.isIncludeInactive()) {
            filter = cb.and(filter, cb.isFalse(organization.get("inactive")));
        }

        return filter;
    }
}
