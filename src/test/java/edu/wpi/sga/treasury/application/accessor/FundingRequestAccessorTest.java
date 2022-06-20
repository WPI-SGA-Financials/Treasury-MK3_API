package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.funding_request.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.funding_request.FundingRequestDto;
import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.util.PagedHelperFunctions;
import edu.wpi.sga.treasury.domain.model.funding_request.FundingRequest;
import edu.wpi.sga.treasury.domain.repository.FundingRequestRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.http.HttpStatus;
import org.springframework.web.server.ResponseStatusException;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

import static edu.wpi.sga.treasury.application.accessor.test_utils.FundingRequestAccessorTestUtils.createSimpleFundingRequest;
import static edu.wpi.sga.treasury.application.accessor.test_utils.PagedTestUtils.mockBasicFiltering;
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
    private PagedHelperFunctions pagedHelperFunctions;

    @BeforeEach
    void setUp() {
        accessor = new FundingRequestAccessorImpl(fundingRequestRepository, pagedHelperFunctions);
    }

    @Test
    @DisplayName("Get Funding Requests for Organization")
    void getFundingRequestsForOrganization() {
        // Arrange
        FundingRequest fr = createSimpleFundingRequest("Cheese Club", 1);

        when(fundingRequestRepository.findAllByOrganizationNameOrderByHearingDateDesc(any()))
                .thenReturn(List.of(fr));

        // Act
        ListResponse<FundingRequestDto> returnedData = accessor.getFundingRequestsForOrganization("Cheese Club");

        // Assert
        assertEquals(1, returnedData.getData().size());
        assertEquals("Cheese Club", returnedData.getData().get(0).getNameOfClub());
        assertEquals(BigDecimal.valueOf(5.00), returnedData.getData().get(0).getAmountApproved());
    }

    @Test
    @DisplayName("Get Funding Request by ID")
    void getFundingRequestById() {
        // Arrange
        FundingRequest fr = createSimpleFundingRequest("Cheese Club", 1);

        when(fundingRequestRepository.findById(any())).thenReturn(Optional.of(fr));

        // Act
        Response<FundingRequestDetailedDto> returnedData = accessor.getFundingRequestById(1);

        // Assert
        assertEquals(1, returnedData.getData().getId());
        assertEquals("Cheese Club", returnedData.getData().getNameOfClub());
        assertEquals(BigDecimal.valueOf(5.00), returnedData.getData().getAmountApproved());
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
        mockBasicFiltering(pagedHelperFunctions);

        FundingRequest cheeseFR = createSimpleFundingRequest("Cheese Club", 1);
        FundingRequest studentFR = createSimpleFundingRequest("Student Government Association", 2);

        when(fundingRequestRepository.findAll(any(Specification.class), any(Pageable.class)))
                .thenReturn(new PageImpl<>(List.of(cheeseFR, studentFR)));

        // Act
        PagedRequest request = new PagedRequest();
        request.setName(List.of("Cheese Club", "Student", " "));

        PagedResponse<FundingRequestDto> returnedData = accessor.getFundingRequests(request);

        // Assert
        assertEquals(2, returnedData.getMaxResults());
        assertEquals(1, returnedData.getData().get(0).getId());
        assertEquals("Cheese Club", returnedData.getData().get(0).getNameOfClub());
        assertEquals("Student Government Association", returnedData.getData().get(1).getNameOfClub());
        assertEquals(BigDecimal.valueOf(5.00), returnedData.getData().get(1).getAmountApproved());
    }
}