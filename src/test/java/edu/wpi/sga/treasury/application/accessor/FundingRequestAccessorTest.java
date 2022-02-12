package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDto;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.repository.FundingRequestRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.data.domain.Page;
import org.springframework.http.HttpStatus;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;

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


        // Act
        List<FundingRequestDto> returnedData = accessor.getFundingRequestsForOrganization("Cheese Club");

        // Assert

    }

    @Test
    @DisplayName("Get Funding Requests for Organization not found")
    void getFundingRequestsForOrganizationNotFound() {
        // Arrange
        when(fundingRequestRepository.findAllByOrganizationNameOrderByFundingDateDesc(any()))
                .thenReturn(Optional.empty());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class, () -> {
            accessor.getFundingRequestsForOrganization("Cheese");
        });

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());
    }

    @Test
    @DisplayName("Get Funding Request by ID")
    void getFundingRequestById() {
        // Arrange


        // Act
        FundingRequestDetailedDto returnedData = accessor.getFundingRequestById(1);

        // Assert

    }

    @Test
    @DisplayName("Get Funding Request by ID not found")
    void getFundingRequestByIdNotFound() {
        // Arrange
        when(fundingRequestRepository.findById(any())).thenReturn(Optional.empty());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class, () -> {
            accessor.getFundingRequestById(1);
        });

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());
    }

    @Test
    @DisplayName("Get Paged and Filtered list of Funding Requests")
    void getFundingRequestsFiltered() {
        // Arrange


        // Act
        PagedRequest request = new PagedRequest();
        request.setName(List.of("Cheese Club", "Student", " "));

        Page<FundingRequestDto> returnedData = accessor.getFundingRequests(request);

        // Assert

    }

    @Test
    @DisplayName("Get Paged list of Funding Requests")
    void getFundingRequests() {
        // Arrange


        // Act
        PagedRequest request = new PagedRequest();
        request.setPage(1);

        Page<FundingRequestDto> returnedData = accessor.getFundingRequests(request);

        // Assert

    }
}