package edu.wpi.sga.treasury.domain.repository.custom;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.domain.model.Budget;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.repository.custom.util.RepositoryHelperFunctions;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.PageRequest;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.criteria.*;
import java.util.List;

public class BudgetRepositoryCustomImpl implements BudgetRepositoryCustom {
    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public Page<Budget> findBudgetsByFilters(PagedRequest request) {
        CriteriaBuilder cb = entityManager.getCriteriaBuilder();
        CriteriaQuery<Budget> query = cb.createQuery(Budget.class);
        Root<Budget> budget = query.from(Budget.class);
        Join<Budget, Organization> orgJoin = budget.join("organization");

        // Construct Filter
        Predicate filter = getPredicate(request, cb, orgJoin);

        query.select(budget).where(filter);

        int startPage = (request.getPage() * request.getResultsPerPage()) - request.getResultsPerPage();

        // Get Budgets based on Filter and pagination
        List<Budget> budgets = entityManager
                .createQuery(query)
                .setFirstResult(startPage)
                .setMaxResults(request.getResultsPerPage())
                .getResultList();

        // Get total amount matching filter
        Long count = getCount(cb, filter);

        return new PageImpl<>(budgets, PageRequest.of(request.getPage() - 1, request.getResultsPerPage()), count);
    }

    private Long getCount(CriteriaBuilder cb, Predicate filter) {
        CriteriaQuery<Long> countQuery = cb.createQuery(Long.class);
        Root<Organization> organizationCount = countQuery.from(Organization.class);
        countQuery.select(cb.count(organizationCount)).where(cb.and(filter));

        return entityManager.createQuery(countQuery).getSingleResult();
    }

    private Predicate getPredicate(PagedRequest request, CriteriaBuilder cb, Join<Budget, Organization> budgetOrg) {
        Predicate filter = cb.conjunction();

        RepositoryHelperFunctions helperFunctions = new RepositoryHelperFunctions();
        filter = cb.and(filter, helperFunctions.getOrgBasedPredicate(request, cb, budgetOrg));

        return filter;
    }
}