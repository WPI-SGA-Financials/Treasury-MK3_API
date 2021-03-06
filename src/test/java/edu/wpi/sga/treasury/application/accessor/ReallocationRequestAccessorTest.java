package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.util.PagedHelperFunctions;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.model.ReallocationRequest;
import edu.wpi.sga.treasury.domain.repository.ReallocationRequestRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

@ExtendWith(MockitoExtension.class)
class ReallocationRequestAccessorTest {

    ReallocationRequestAccessor accessor;

    @Mock
    private ReallocationRequestRepository reallocationRequestRepository;
    @Mock
    private PagedHelperFunctions pagedHelperFunctions;

    @BeforeEach
    void setUp() {
        accessor = new ReallocationRequestAccessorImpl(reallocationRequestRepository, pagedHelperFunctions);
    }

    @Test
    @DisplayName("Get Reallocations For Organization")
    void getReallocationRequestsForOrganization() {
        // Arrange
        Organization org = new Organization();
        org.setId(1);
        org.setName("Cheese Club");

        ReallocationRequest request = new ReallocationRequest();
        request.setId(1);
        request.setOrganization(org);

        // Act


        // Assert
    }

    @Test
    @DisplayName("Get Reallocations For Organization not found")
    void getReallocationRequestsForOrganizationNotFound() {
        // Arrange


        // Act


        // Assert
    }

    @Test
    @DisplayName("Get Reallocation by ID")
    void getReallocationRequestById() {
        // Arrange


        // Act


        // Assert
    }

    @Test
    @DisplayName("Get Reallocation by ID not found")
    void getReallocationRequestByIdNotFound() {
        // Arrange


        // Act


        // Assert
    }

    @Test
    @DisplayName("Get Paged and Filtered list of Reallocations")
    void getReallocationRequestsFiltered() {
        // Arrange


        // Act


        // Assert
    }

    @Test
    @DisplayName("Get Paged list of Reallocations")
    void getReallocationRequests() {
        // Arrange


        // Act


        // Assert
    }
}