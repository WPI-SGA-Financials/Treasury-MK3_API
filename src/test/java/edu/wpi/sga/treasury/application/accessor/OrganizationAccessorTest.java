package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.repository.OrganizationRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.http.HttpStatus;
import org.springframework.web.server.ResponseStatusException;

import java.util.Optional;

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
    private GeneralHelperFunctions generalHelperFunctions;

    @BeforeEach
    void setUp() {
        accessor = new OrganizationAccessorImpl(organizationRepository, generalHelperFunctions);
    }

    @Test
    @DisplayName("Get Paged and Filtered list of Organizations")
    void getOrganizationsFiltered() {
    }

    @Test
    @DisplayName("Get Paged list of Organizations")
    void getOrganizations() {
    }

    @Test
    @DisplayName("Get Organization by Name")
    void getOrganization() {
        // Arrange
        Organization org = new Organization();

        // Act


        // Assert
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