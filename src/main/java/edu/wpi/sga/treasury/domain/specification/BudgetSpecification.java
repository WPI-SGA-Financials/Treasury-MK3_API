package edu.wpi.sga.treasury.domain.specification;

import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.domain.model.*;
import edu.wpi.sga.treasury.domain.model.budget.Budget;
import edu.wpi.sga.treasury.domain.model.budget.BudgetLegacy;
import edu.wpi.sga.treasury.domain.model.budget.BudgetLineItem;
import edu.wpi.sga.treasury.domain.model.budget.BudgetSection;
import edu.wpi.sga.treasury.domain.specification.util.SpecificationUtil;
import lombok.Builder;
import org.springframework.data.jpa.domain.Specification;

import javax.persistence.criteria.*;
import java.util.List;

@Builder
public class BudgetSpecification implements Specification<Budget> {

    private final PagedRequest request;

    @Override
    public Predicate toPredicate(Root<Budget> root, CriteriaQuery<?> query, CriteriaBuilder cb) {
        SpecificationUtil<Budget> util = new SpecificationUtil<>(cb);

        Join<Budget, Organization> organizationJoin = root.join(Budget_.ORGANIZATION);

        List<Order> order = List.of(
                cb.asc(organizationJoin.get(Organization_.NAME)),
                cb.desc(root.get(Budget_.FISCAL_YEAR))
        );

        List<Predicate> predicates = util.getOrgBasedPredicate(request, organizationJoin);

        if (!request.getFiscalYear().isEmpty()) {
            Path<String> path = root.get(Budget_.FISCAL_YEAR);

            predicates.add(util.filterIn(request.getFiscalYear(), path));
        }

        if (!request.getFiscalClass().isEmpty()) {
            Subquery<Integer> subqueryLegacy = getLegacyFiscalClassSubquery(request.getFiscalClass(), cb, root, query);

            Subquery<Integer> subquerySections = getSectionFiscalClassSubquery(request.getFiscalClass(), cb, root, query);

            predicates.add(cb.or(cb.exists(subqueryLegacy), cb.exists(subquerySections)));
        }

        return query.where(cb.and(predicates.toArray(new Predicate[0])))
                .orderBy(order)
                .getRestriction();
    }

    private Subquery<Integer> getLegacyFiscalClassSubquery(List<String> fiscalClass, CriteriaBuilder cb, Root<Budget> budget, CriteriaQuery<?> query) {
        Subquery<Integer> subqueryLegacy = query.subquery(Integer.class);

        Root<BudgetLegacy> legacySubRoot = subqueryLegacy.from(BudgetLegacy.class);

        // Select Budget ID
        subqueryLegacy.select(legacySubRoot.get(BudgetLegacy_.BUDGET).get(Budget_.ID));

        subqueryLegacy.where(cb.and(
                cb.function("fnc_fiscal_class",
                                String.class,
                                legacySubRoot.get(BudgetLegacy_.AMOUNT_PROPOSED),
                                legacySubRoot.get(BudgetLegacy_.APPROVED_APPEAL))
                        .in(fiscalClass),
                cb.equal(legacySubRoot.get(BudgetLegacy_.BUDGET).get(Budget_.ID), budget.get(Budget_.ID))
        ));

        return subqueryLegacy;
    }

    private Subquery<Integer> getSectionFiscalClassSubquery(List<String> fiscalClass, CriteriaBuilder cb, Root<Budget> budget, CriteriaQuery<?> query) {
        Subquery<Integer> subquerySection = query.subquery(Integer.class);

        Root<BudgetSection> sectionSubRoot = subquerySection.from(BudgetSection.class);
        Join<BudgetSection, BudgetLineItem> lineItemJoin = sectionSubRoot.join(BudgetSection_.BUDGET_LINE_ITEMS);

        // Select Budget ID
        subquerySection.select(sectionSubRoot.get(BudgetSection_.BUDGET).get(Budget_.ID));

        subquerySection.where(cb.equal(sectionSubRoot.get(BudgetLegacy_.BUDGET).get(Budget_.ID), budget.get(Budget_.ID)));

        subquerySection.groupBy(sectionSubRoot.get(BudgetSection_.BUDGET));

        subquerySection.having(cb.function("fnc_fiscal_class",
                        String.class,
                        cb.sum(lineItemJoin.get(BudgetLineItem_.AMOUNT_PROPOSED)),
                        cb.sum(lineItemJoin.get(BudgetLineItem_.APPROVED_APPEAL)))
                .in(fiscalClass));

        return subquerySection;
    }
}
