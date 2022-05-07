package edu.wpi.sga.treasury.domain.repository.custom;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.model.Organization_;
import edu.wpi.sga.treasury.domain.repository.custom.util.RepositoryHelperFunctions;
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
    public Page<Organization> findOrganizationsByFilters(PagedRequest request) {
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

    private Predicate getPredicate(PagedRequest request, CriteriaBuilder cb, Root<Organization> organization) {
        RepositoryHelperFunctions helperFunctions = new RepositoryHelperFunctions(cb);

        List<Predicate> orgBasedPredicates = new ArrayList<>();

        if (!request.getName().isEmpty()) {
            Path<String> path = organization.get(Organization_.NAME);

            orgBasedPredicates.add(helperFunctions.filterLike(request.getName(), path));
        }

        if (!request.getAcronym().isEmpty()) {
            Path<String> path = organization.get(Organization_.ACRONYM);

            orgBasedPredicates.add(helperFunctions.filterLike(request.getAcronym(), path));
        }

        if (!request.getClassification().isEmpty()) {
            Path<String> path = organization.get(Organization_.CLASSIFICATION);

            orgBasedPredicates.add(helperFunctions.filterEqual(request.getClassification(), path));
        }

        if (!request.getType().isEmpty()) {
            Path<String> path = organization.get(Organization_.TYPE_OF_CLUB);

            orgBasedPredicates.add(helperFunctions.filterEqual(request.getType(), path));
        }

        if(!request.isIncludeInactive()) {
            orgBasedPredicates.add(cb.isFalse(organization.get(Organization_.IS_INACTIVE)));
        }

        return orgBasedPredicates.stream().reduce(cb::and).orElse(cb.conjunction());
    }
}
