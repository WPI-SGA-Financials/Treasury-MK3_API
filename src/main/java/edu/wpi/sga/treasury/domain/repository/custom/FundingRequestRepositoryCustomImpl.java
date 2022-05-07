package edu.wpi.sga.treasury.domain.repository.custom;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.domain.model.FundingRequest;
import edu.wpi.sga.treasury.domain.model.FundingRequest_;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.repository.custom.util.RepositoryHelperFunctions;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.PageRequest;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.criteria.*;
import java.util.List;

public class FundingRequestRepositoryCustomImpl implements FundingRequestRepositoryCustom {
    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public Page<FundingRequest> findFundingRequestsByFilters(PagedRequest request) {
        CriteriaBuilder cb = entityManager.getCriteriaBuilder();
        CriteriaQuery<FundingRequest> query = cb.createQuery(FundingRequest.class);

        Root<FundingRequest> fr = query.from(FundingRequest.class);
        Join<FundingRequest, Organization> orgJoin = fr.join(FundingRequest_.ORGANIZATION);

        fr.alias("frs");
        orgJoin.alias("organizations");

        // Construct Filter
        Predicate filter = getPredicate(request, cb, fr, orgJoin);

        List<Order> order = List.of(
                cb.desc(fr.get(FundingRequest_.HEARING_DATE)),
                cb.desc(fr.get(FundingRequest_.DOT_NUMBER))
        );

        query.select(fr).where(filter).orderBy(order);

        int startPage = (request.getPage() * request.getResultsPerPage()) - request.getResultsPerPage();

        // Get Funding Requests based on Filter and pagination
        List<FundingRequest> fundingRequests = entityManager
                .createQuery(query)
                .setFirstResult(startPage)
                .setMaxResults(request.getResultsPerPage())
                .getResultList();

        // Get total amount matching filter
        Long count = getCount(filter, cb);

        return new PageImpl<>(fundingRequests, PageRequest.of(request.getPage() - 1, request.getResultsPerPage()), count);
    }

    private Long getCount(Predicate filter, CriteriaBuilder cb) {
        CriteriaQuery<Long> countQuery = cb.createQuery(Long.class);

        Root<FundingRequest> frRoot = countQuery.from(FundingRequest.class);
        Join<FundingRequest, Organization> orgJoin = frRoot.join(FundingRequest_.ORGANIZATION);

        frRoot.alias("frs");
        orgJoin.alias("organizations");

        countQuery.select(cb.count(orgJoin)).where(filter);

        return entityManager.createQuery(countQuery).getSingleResult();
    }

    private Predicate getPredicate(PagedRequest request, CriteriaBuilder cb, Root<FundingRequest> fr, Join<FundingRequest, Organization> frOrg) {
        RepositoryHelperFunctions helperFunctions = new RepositoryHelperFunctions(cb);

        List<Predicate> predicates = helperFunctions.getOrgBasedPredicate(request, frOrg);

        if (!request.getFiscalYear().isEmpty()) {
            Path<String> path = fr.get(FundingRequest_.FISCAL_YEAR);

            predicates.add(helperFunctions.filterEqual(request.getFiscalYear(), path));
        }

        if (!request.getDescription().isEmpty()) {
            Path<String> path = fr.get(FundingRequest_.DESCRIPTION);

            predicates.add(helperFunctions.filterLike(request.getDescription(), path));
        }

        if (!request.getDotNumber().isEmpty()) {
            Path<String> path = fr.get(FundingRequest_.DOT_NUMBER);

            predicates.add(helperFunctions.filterLike(request.getDotNumber(), path));
        }

        return predicates.stream().reduce(cb::and).orElse(cb.conjunction());
    }
}
