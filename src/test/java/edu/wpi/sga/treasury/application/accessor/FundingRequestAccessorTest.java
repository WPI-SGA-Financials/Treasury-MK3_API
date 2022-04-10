package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDto;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.FundingRequest;
import edu.wpi.sga.treasury.domain.repository.FundingRequestRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.http.HttpStatus;
import org.springframework.web.server.ResponseStatusException;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

import static edu.wpi.sga.treasury.application.accessor.test_utils.FundingRequestAccessorTestUtils.createSimpleFundingRequest;
import static edu.wpi.sga.treasury.application.accessor.test_utils.GeneralTestUtils.mockBasicFiltering;
import static edu.wpi.sga.treasury.application.accessor.test_utils.GeneralTestUtils.mockBasicPagedRequest;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class FundingRequestAccessorTest {

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
    @DisplayName("Get Funding Requests for Organization")
    void getFundingRequestsForOrganization() {
        // Arrange
        FundingRequest fr = createSimpleFundingRequest("Cheese Club", 1);

        when(fundingRequestRepository.findAllByOrganizationNameOrderByHearingDateDesc(any()))
                .thenReturn(List.of(fr));

        // Act
        List<FundingRequestDto> returnedData = accessor.getFundingRequestsForOrganization("Cheese Club");

        // Assert
        assertEquals(1, returnedData.size());
        assertEquals("Cheese Club", returnedData.get(0).getNameOfClub());
        assertEquals(BigDecimal.valueOf(5.00), returnedData.get(0).getAmountApproved());
    }

    @Test
    @DisplayName("Get Funding Requests for Organization not found")
    void getFundingRequestsForOrganizationNotFound() {
        // Arrange
        when(fundingRequestRepository.findAllByOrganizationNameOrderByHearingDateDesc(any()))
                .thenReturn(List.of());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class, () -> accessor.getFundingRequestsForOrganization("Cheese"));

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());
    }

    @Test
    @DisplayName("Get Funding Request by ID")
    void getFundingRequestById() {
        // Arrange
        FundingRequest fr = createSimpleFundingRequest("Cheese Club", 1);

        when(fundingRequestRepository.findById(any())).thenReturn(Optional.of(fr));

        // Act
        FundingRequestDetailedDto returnedData = accessor.getFundingRequestById(1);

        // Assert
        assertEquals(1, returnedData.getId());
        assertEquals("Cheese Club", returnedData.getNameOfClub());
        assertEquals(BigDecimal.valueOf(5.00), returnedData.getAmountApproved());
    }

    @Test
    @DisplayName("Get Funding Request by ID not found")
    void getFundingRequestByIdNotFound() {
        // Arrange
        when(fundingRequestRepository.findById(any())).thenReturn(Optional.empty());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class, () -> accessor.getFundingRequestById(1));

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());
    }

    @Test
    @DisplayName("Get Paged and Filtered list of Funding Requests")
    void getFundingRequestsFiltered() {
        // Arrange
        mockBasicFiltering(generalHelperFunctions);

        FundingRequest cheeseFR = createSimpleFundingRequest("Cheese Club", 1);
        FundingRequest studentFR = createSimpleFundingRequest("Student Government Association", 2);

        when(fundingRequestRepository.findFundingRequestsByFilters(any()))
                .thenReturn(new PageImpl<>(List.of(cheeseFR, studentFR)));

        // Act
        PagedRequest request = new PagedRequest();
        request.setName(List.of("Cheese Club", "Student", " "));

        Page<FundingRequestDto> returnedData = accessor.getFundingRequests(request);

        // Assert
        assertEquals(2, returnedData.getTotalElements());
        assertEquals(1, returnedData.getContent().get(0).getId());
        assertEquals("Cheese Club", returnedData.getContent().get(0).getNameOfClub());
        assertEquals("Student Government Association", returnedData.getContent().get(1).getNameOfClub());
        assertEquals(BigDecimal.valueOf(5.00), returnedData.getContent().get(1).getAmountApproved());
    }

    @Test
    @DisplayName("Get Paged list of Funding Requests")
    void getFundingRequests() {
        // Arrange
        mockBasicPagedRequest(generalHelperFunctions);

        FundingRequest fr101 = createSimpleFundingRequest("Cheese Club", 1);
        fr101.setDotNumber("F.101");

        FundingRequest fr100 = createSimpleFundingRequest("Student Government Association", 2);
        fr100.setDotNumber("F.100");

        when(fundingRequestRepository.findAllByOrganizationIsInactiveIsFalseOrderByHearingDateDescDotNumberDesc(any()))
                .thenReturn(new PageImpl<>(List.of(fr100, fr101)));

        // Act
        PagedRequest request = new PagedRequest();
        request.setPage(1);

        Page<FundingRequestDto> returnedData = accessor.getFundingRequests(request);

        // Assert
        assertEquals(2, returnedData.getTotalElements());
        assertEquals(2, returnedData.getContent().get(0).getId());
        assertEquals("Cheese Club", returnedData.getContent().get(1).getNameOfClub());
        assertEquals("Student Government Association", returnedData.getContent().get(0).getNameOfClub());
        assertEquals("F.100", returnedData.getContent().get(0).getDotNumber());
    }
}