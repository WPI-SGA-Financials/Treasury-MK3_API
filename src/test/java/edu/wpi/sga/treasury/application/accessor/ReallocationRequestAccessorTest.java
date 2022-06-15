package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDto;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.FundingRequest;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.model.ReallocationRequest;
import edu.wpi.sga.treasury.domain.repository.ReallocationRequestRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.Mockito;
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
import static edu.wpi.sga.treasury.application.accessor.test_utils.ReallocationRequestAccessorTestUtils.createSimpleReallocationRequest;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.ArgumentMatchers.any;
import static reactor.core.publisher.Mono.when;

@ExtendWith(MockitoExtension.class)
class ReallocationRequestAccessorTest {

    ReallocationRequestAccessor accessor;

    @Mock
    private ReallocationRequestRepository reallocationRequestRepository;
    @Mock
    private GeneralHelperFunctions generalHelperFunctions;

    @BeforeEach
    void setUp() {
        accessor = new ReallocationRequestAccessorImpl(reallocationRequestRepository, generalHelperFunctions);
    }

    @Test
    @DisplayName("Get Reallocations For Organization")
    void getReallocationRequestsForOrganization() {
        // Arrange
        ReallocationRequest rr = createSimpleReallocationRequest("Cheese Club",1);
        Mockito.when(reallocationRequestRepository.findAllByOrganizationNameOrderByHearingDateDesc(any()))
                .thenReturn(List.of(rr));

        // Act
        List<ReallocationRequestDto> returnedData = accessor.getReallocationRequestsForOrganization("Cheese Club");

        // Assert
        assertEquals(1, returnedData.size());
        assertEquals("Cheese Club", returnedData.get(0).getNameOfClub());
        assertEquals(BigDecimal.valueOf(5.00), returnedData.get(0).getAmountApproved());
    }

    @Test
    @DisplayName("Get Reallocations For Organization not found")
    void getReallocationRequestsForOrganizationNotFound() {
        // Arrange
        Mockito.when(reallocationRequestRepository.findAllByOrganizationNameOrderByHearingDateDesc(any()))
                .thenReturn(List.of());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class, () -> accessor.getReallocationRequestsForOrganization("Cheese"));

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());
    }

    @Test
    @DisplayName("Get Reallocation by ID")
    void getReallocationRequestById() {
        // Arrange
        ReallocationRequest fr = createSimpleReallocationRequest("Cheese Club", 1);

        Mockito.when(reallocationRequestRepository.findById(any())).thenReturn(Optional.of(fr));

        // Act
        ReallocationRequestDetailedDto returnedData = accessor.getReallocationRequestById(1);

        // Assert
        assertEquals(1, returnedData.getId());
        assertEquals("Cheese Club", returnedData.getNameOfClub());
        assertEquals(BigDecimal.valueOf(5.00), returnedData.getAmountApproved());
    }

    @Test
    @DisplayName("Get Reallocation by ID not found")
    void getReallocationRequestByIdNotFound() {
        // Arrange
        Mockito.when(reallocationRequestRepository.findById(any())).thenReturn(Optional.empty());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class, () -> accessor.getReallocationRequestById(1));

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());
    }

    @Test
    @DisplayName("Get Paged and Filtered list of Reallocations")
    void getReallocationRequestsFiltered() {
        // Arrange
        mockBasicFiltering(generalHelperFunctions);

        ReallocationRequest cheeseFR = createSimpleReallocationRequest("Cheese Club", 1);
        ReallocationRequest studentFR = createSimpleReallocationRequest("Student Government Association", 2);

        Mockito.when(reallocationRequestRepository.findReallocsByFilters(any()))
                .thenReturn(new PageImpl<>(List.of(cheeseFR, studentFR)));

        // Act
        PagedRequest request = new PagedRequest();
        request.setName(List.of("Cheese Club", "Student", " "));

        Page<ReallocationRequestDto> returnedData = accessor.getReallocationRequests(request);

        // Assert
        assertEquals(2, returnedData.getTotalElements());
        assertEquals(1, returnedData.getContent().get(0).getId());
        assertEquals("Cheese Club", returnedData.getContent().get(0).getNameOfClub());
        assertEquals("Student Government Association", returnedData.getContent().get(1).getNameOfClub());
        assertEquals(BigDecimal.valueOf(5.00), returnedData.getContent().get(1).getAmountApproved());
    }

    @Test
    @DisplayName("Get Paged list of Reallocations")
    void getReallocationRequests() {
        // Arrange
        mockBasicPagedRequest(generalHelperFunctions);

        ReallocationRequest fr101 = createSimpleReallocationRequest("Cheese Club", 1);
        fr101.setDotNumber("F.101");

        ReallocationRequest fr100 = createSimpleReallocationRequest("Student Government Association", 2);
        fr100.setDotNumber("F.100");

        Mockito.when(reallocationRequestRepository.findAllByOrganizationIsInactiveIsFalseOrderByHearingDateDescDotNumberDesc(any()))
                .thenReturn(new PageImpl<>(List.of(fr100, fr101)));

        // Act
        PagedRequest request = new PagedRequest();
        request.setPage(1);

        Page<ReallocationRequestDto> returnedData = accessor.getReallocationRequests(request);

        // Assert
        assertEquals(2, returnedData.getTotalElements());
        assertEquals(2, returnedData.getContent().get(0).getId());
        assertEquals("Cheese Club", returnedData.getContent().get(1).getNameOfClub());
        assertEquals("Student Government Association", returnedData.getContent().get(0).getNameOfClub());
        assertEquals("F.100", returnedData.getContent().get(0).getDotNumber());
    }
}