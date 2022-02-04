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

public class BudgetRepositoryCustomImpl implements BudgetRepositoryCustom {
    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public Page<Budget> findBudgetsByFilters(PagedRequest request) {
        CriteriaBuilder cb = entityManager.getCriteriaBuilder();
        CriteriaQuery<Budget> query = cb.createQuery(Budget.class);

        Root<Budget> budget = query.from(Budget.class);
        Join<Budget, Organization> orgJoin = budget.join(Budget_.ORGANIZATION);

        budget.alias("budgets");
        orgJoin.alias("organizations");

        // Construct Filter
        Predicate filter = getPredicate(request, cb, budget, orgJoin, query);

        List<Order> order = List.of(
                cb.asc(orgJoin.get(Organization_.NAME)),
                cb.desc(budget.get(Budget_.FISCAL_YEAR))
        );

        query.select(budget).where(filter).orderBy(order);

        int startPage = (request.getPage() * request.getResultsPerPage()) - request.getResultsPerPage();

        // Get Budgets based on Filter and pagination
        List<Budget> budgets = entityManager
                .createQuery(query)
                .setFirstResult(startPage)
                .setMaxResults(request.getResultsPerPage())
                .getResultList();

        // Get total amount matching filter
        Long count = getCount(filter, cb);

        return new PageImpl<>(budgets, PageRequest.of(request.getPage() - 1, request.getResultsPerPage()), count);
    }

    private Long getCount(Predicate filter, CriteriaBuilder cb) {
        CriteriaQuery<Long> countQuery = cb.createQuery(Long.class);

        Root<Budget> budgetRoot = countQuery.from(Budget.class);
        Join<Budget, Organization> orgJoin = budgetRoot.join(Budget_.ORGANIZATION);

        budgetRoot.alias("budgets");
        orgJoin.alias("organizations");

        countQuery.select(cb.count(orgJoin)).where(filter);

        return entityManager.createQuery(countQuery).getSingleResult();
    }

    private Predicate getPredicate(PagedRequest request, CriteriaBuilder cb, Root<Budget> budget, Join<Budget, Organization> budgetOrg, CriteriaQuery<Budget> query) {
        RepositoryHelperFunctions helperFunctions = new RepositoryHelperFunctions(cb);

        List<Predicate> predicates = helperFunctions.getOrgBasedPredicate(request, budgetOrg);

        if (!request.getFiscalYear().isEmpty()) {
            Path<String> path = budget.get(Budget_.FISCAL_YEAR);

            predicates.add(helperFunctions.filterEqual(request.getFiscalYear(), path));
        }

        if (!request.getFiscalClass().isEmpty()) {
            Subquery<Integer> subqueryLegacy = getLegacyFiscalClassSubquery(request, cb, budget, query);

            Subquery<Integer> subquerySections = getSectionFiscalClassSubquery(request, cb, budget, query);

            predicates.add(cb.or(cb.exists(subqueryLegacy), cb.exists(subquerySections)));
        }

        return predicates.stream().reduce(cb::and).orElse(cb.conjunction());
    }

    private Subquery<Integer> getLegacyFiscalClassSubquery(PagedRequest request, CriteriaBuilder cb, Root<Budget> budget, CriteriaQuery<Budget> query) {
        Subquery<Integer> subqueryLegacy = query.subquery(Integer.class);

        Root<BudgetLegacy> legacySubRoot = subqueryLegacy.from(BudgetLegacy.class);

        // Select Budget ID
        subqueryLegacy.select(legacySubRoot.get(BudgetLegacy_.BUDGET).get(Budget_.ID));

        subqueryLegacy.where(cb.and(
                cb.function("fnc_FiscalClass",
                                String.class,
                                legacySubRoot.get(BudgetLegacy_.AMOUNT_PROPOSED),
                                legacySubRoot.get(BudgetLegacy_.APPROVED_APPEAL))
                        .in(request.getFiscalClass()),
                cb.equal(legacySubRoot.get(BudgetLegacy_.BUDGET).get(Budget_.ID), budget.get(Budget_.ID))
        ));

        return subqueryLegacy;
    }

    private Subquery<Integer> getSectionFiscalClassSubquery(PagedRequest request, CriteriaBuilder cb, Root<Budget> budget, CriteriaQuery<Budget> query) {
        Subquery<Integer> subquerySection = query.subquery(Integer.class);

        Root<BudgetSection> sectionSubRoot = subquerySection.from(BudgetSection.class);
        Join<BudgetSection, BudgetLineItem> lineItemJoin = sectionSubRoot.join(BudgetSection_.BUDGET_LINE_ITEMS);

        // Select Budget ID
        subquerySection.select(sectionSubRoot.get(BudgetSection_.BUDGET).get(Budget_.ID));

        subquerySection.where(cb.equal(sectionSubRoot.get(BudgetLegacy_.BUDGET).get(Budget_.ID), budget.get(Budget_.ID)));

        subquerySection.groupBy(sectionSubRoot.get(BudgetSection_.BUDGET));

        subquerySection.having(cb.function("fnc_FiscalClass",
                        String.class,
                        cb.sum(lineItemJoin.get(BudgetLineItem_.AMOUNT_PROPOSED)),
                        cb.sum(lineItemJoin.get(BudgetLineItem_.APPROVED_APPEAL)))
                .in(request.getFiscalClass()));

        return subquerySection;
    }
}