package edu.wpi.sga.treasury.application.accessor.test_utils;

import edu.wpi.sga.treasury.domain.model.Budget;

public class BudgetAccessorTestUtils {
    public static Budget createSimpleBudget() {
        Budget budget = new Budget();
        budget.setId(1);
        budget.setFiscalYear("FY 19");
        return budget;
    }
}
