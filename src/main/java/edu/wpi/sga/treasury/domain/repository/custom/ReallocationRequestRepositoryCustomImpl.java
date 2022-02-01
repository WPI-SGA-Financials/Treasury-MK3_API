package edu.wpi.sga.treasury.domain.repository.custom;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.domain.model.*;
import edu.wpi.sga.treasury.domain.repository.custom.util.RepositoryHelperFunctions;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.PageRequest;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.criteria.*;
import java.util.List;

public class ReallocationRequestRepositoryCustomImpl implements ReallocationRequestRepositoryCustom {
    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public Page<ReallocationRequest> findReallocsByFilters(PagedRequest request) {
        CriteriaBuilder cb = entityManager.getCriteriaBuilder();
        CriteriaQuery<ReallocationRequest> query = cb.createQuery(ReallocationRequest.class);

        Root<ReallocationRequest> realloc = query.from(ReallocationRequest.class);
        Join<ReallocationRequest, Organization> orgJoin = realloc.join(ReallocationRequest_.ORGANIZATION);

        realloc.alias("reallocs");
        orgJoin.alias("organizations");

        // Construct Filter
        Predicate filter = getPredicate(request, cb, realloc, orgJoin);

        List<Order> order = List.of(
                cb.desc(realloc.get(ReallocationRequest_.HEARING_DATE)),
                cb.desc(realloc.get(ReallocationRequest_.DOT_NUMBER))
        );

        query.select(realloc).where(filter).orderBy(order);

        int startPage = (request.getPage() * request.getResultsPerPage()) - request.getResultsPerPage();

        // Get Funding Requests based on Filter and pagination
        List<ReallocationRequest> reallocationRequests = entityManager
                .createQuery(query)
                .setFirstResult(startPage)
                .setMaxResults(request.getResultsPerPage())
                .getResultList();

        // Get total amount matching filter
        Long count = getCount(filter, cb);

        return new PageImpl<>(reallocationRequests, PageRequest.of(request.getPage() - 1, request.getResultsPerPage()), count);
    }

    private Long getCount(Predicate filter, CriteriaBuilder cb) {
        CriteriaQuery<Long> countQuery = cb.createQuery(Long.class);

        Root<ReallocationRequest> reallocRoot = countQuery.from(ReallocationRequest.class);
        Join<ReallocationRequest, Organization> orgJoin = reallocRoot.join(ReallocationRequest_.ORGANIZATION);

        reallocRoot.alias("reallocs");
        orgJoin.alias("organizations");

        countQuery.select(cb.count(orgJoin)).where(filter);

        return entityManager.createQuery(countQuery).getSingleResult();
    }

    private Predicate getPredicate(PagedRequest request, CriteriaBuilder cb, Root<ReallocationRequest> realloc, Join<ReallocationRequest, Organization> reallocOrg) {
        RepositoryHelperFunctions helperFunctions = new RepositoryHelperFunctions(cb);

        List<Predicate> predicates = helperFunctions.getOrgBasedPredicate(request, reallocOrg);

        if (!request.getFiscalYear().isEmpty()) {
            Path<String> path = realloc.get(ReallocationRequest_.FISCAL_YEAR);

            predicates.add(helperFunctions.filterEqual(request.getFiscalYear(), path));
        }

        if (!request.getDescription().isEmpty()) {
            Path<String> path = realloc.get(ReallocationRequest_.DESCRIPTION);

            predicates.add(helperFunctions.filterLike(request.getDescription(), path));
        }

        if (!request.getDotNumber().isEmpty()) {
            Path<String> path = realloc.get(ReallocationRequest_.DOT_NUMBER);

            predicates.add(helperFunctions.filterLike(request.getDotNumber(), path));
        }

        return predicates.stream().reduce(cb::and).orElse(cb.conjunction());
    }
}
