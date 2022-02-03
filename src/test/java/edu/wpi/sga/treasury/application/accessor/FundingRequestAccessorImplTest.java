package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.repository.FundingRequestRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import static org.junit.jupiter.api.Assertions.*;

@ExtendWith(MockitoExtension.class)
class FundingRequestAccessorImplTest {

    FundingRequestAccessor accessor;

    // Repositories
    @Mock
    private FundingRequestRepository fundingRequestRepository;

    // Util
    @Mock
    private GeneralHelperFunctions generalHelperFunctions;

    @BeforeEach
    void setUp() {
        accessor = new FundingRequestAccessorImpl(fundingRequestRepository, generalHelperFunctions);
    }

    @Test
    void getFundingRequestsForOrganization() {
    }

    @Test
    void getFundingRequests() {
    }

    @Test
    void getFundingRequestById() {
    }
}