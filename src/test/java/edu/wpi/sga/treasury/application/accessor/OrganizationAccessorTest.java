package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.OrganizationDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.util.PagedHelperFunctions;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.repository.OrganizationRepository;
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

import java.util.List;
import java.util.Optional;

import static edu.wpi.sga.treasury.application.accessor.test_utils.PagedTestUtils.mockBasicFiltering;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class OrganizationAccessorTest {

    OrganizationAccessor accessor;

    @Mock
    private OrganizationRepository organizationRepository;
    @Mock
    private PagedHelperFunctions pagedHelperFunctions;

    @BeforeEach
    void setUp() {
        accessor = new OrganizationAccessorImpl(organizationRepository, pagedHelperFunctions);
    }

    @Test
    @DisplayName("Get Paged and Filtered list of Organizations")
    void getOrganizationsFiltered() {
        // Arrange
        mockBasicFiltering(pagedHelperFunctions);

        Organization org = new Organization();
        org.setName("Cheese Club");
        org.setAcronym("Cheese");
        org.setClassification("Class 1 - Special Interest");

        when(organizationRepository.findAll(any(Specification.class), any(Pageable.class))).thenReturn(new PageImpl<>(List.of(org)));

        // Act
        PagedRequest request = new PagedRequest();
        request.setName(List.of("Cheese Club", "Student", " "));

        PagedResponse<OrganizationDto> returnedData = accessor.getOrganizations(request);

        // Assert
        assertEquals(1L, returnedData.getMaxResults());
        assertEquals("Cheese Club", returnedData.getData().get(0).getNameOfClub());
        assertEquals("Cheese", returnedData.getData().get(0).getAcronym());
    }

    @Test
    @DisplayName("Get Organization by Name")
    void getOrganization() {
        // Arrange
        Organization org = new Organization();
        org.setName("Cheese Club");
        org.setAcronym("Cheese");
        org.setClassification("Class 1 - Special Interest");

        when(organizationRepository.findByName(any())).thenReturn(Optional.of(org));

        // Act
        Response<OrganizationDto> returnedData = accessor.getOrganization("Cheese Club");

        // Assert
        assertEquals("Cheese Club", returnedData.getData().getNameOfClub());
        assertEquals("Class 1 - Special Interest", returnedData.getData().getClassification());
    }

    @Test
    @DisplayName("Get Organization by Name not Found")
    void getOrganizationNotFound() {
        // Arrange
        when(organizationRepository.findByName(any())).thenReturn(Optional.empty());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class,
                () -> accessor.getOrganization("Cheese Club"));

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());

    }
}