package edu.wpi.sga.treasury.application.accessor.test_utils;

import edu.wpi.sga.treasury.domain.model.FundingRequest;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.model.ReallocationRequest;

import java.math.BigDecimal;

public class ReallocationRequestAccessorTestUtils {

    public static ReallocationRequest createSimpleReallocationRequest(String orgName, Integer id) {
        Organization org = new Organization();
        org.setName(orgName);

        ReallocationRequest rr = new ReallocationRequest();

        rr.setId(id);
        rr.setAmountAllocated(BigDecimal.valueOf(10.00));
        rr.setAmountApproved(BigDecimal.valueOf(5.00));
        rr.setOrganization(org);

        return rr;
    }
}
