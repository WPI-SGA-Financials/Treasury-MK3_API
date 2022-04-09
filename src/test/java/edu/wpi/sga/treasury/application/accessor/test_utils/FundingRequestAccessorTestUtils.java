package edu.wpi.sga.treasury.application.accessor.test_utils;

import edu.wpi.sga.treasury.domain.model.FundingRequest;
import edu.wpi.sga.treasury.domain.model.Organization;

import java.math.BigDecimal;

public class FundingRequestAccessorTestUtils {
    public static FundingRequest createSimpleFundingRequest(String orgName, Integer id) {
        Organization org = new Organization();
        org.setName(orgName);

        FundingRequest fr = new FundingRequest();

        fr.setId(id);
        fr.setAmountRequested(BigDecimal.valueOf(10.00));
        fr.setAmountApproved(BigDecimal.valueOf(5.00));
        fr.setOrganization(org);

        return fr;
    }
}
