package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.BudgetDto;
import edu.wpi.sga.treasury.application.util.BudgetHelperFunctions;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.Budget;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.repository.BudgetRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.http.HttpStatus;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;

import static edu.wpi.sga.treasury.application.accessor.test_utils.BudgetAccessorTestUtils.createSimpleBudget;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class BudgetAccessorImplTest {

    BudgetAccessor accessor;

    // Repositories
    @Mock
    private BudgetRepository budgetRepository;

    // Util
    @Mock
    private BudgetHelperFunctions budgetHelperFunctions;
    @Mock
    private GeneralHelperFunctions generalHelperFunctions;

    @BeforeEach
    void setUp() {
        accessor = new BudgetAccessorImpl(budgetRepository, budgetHelperFunctions, generalHelperFunctions);
    }

    @Test
    @DisplayName("Get Budgets for Organization")
    void getBudgetsForOrganization() {
        // Arrange
        Organization org = new Organization();
        org.setName("Student Government Association");

        Budget budget = createSimpleBudget();
        budget.setOrganization(org);

        when(budgetRepository.findAllByOrganizationNameIsOrderByFiscalYearDesc(any()))
                .thenReturn(Optional.of(List.of(budget)));

        BudgetDto budgetDto = BudgetDto.builder()
                .id(1)
                .fiscalYear("FY 19")
                .nameOfClub("Student Government Association")
                .build();

        when(budgetHelperFunctions.translateBudgetToBudgetDto(any()))
                .thenReturn(budgetDto);

        // Act
        List<BudgetDto> returnedData = accessor.getBudgetsForOrganization("Student Government Association");

        // Assert
        assertEquals(1, returnedData.get(0).getId());
        assertEquals("FY 19", returnedData.get(0).getFiscalYear());
        assertEquals("Student Government Association", returnedData.get(0).getNameOfClub());
    }

    @Test
    @DisplayName("Get Budgets for Organization not found")
    void getBudgetsForOrganizationNotFound() {
        // Arrange
        when(budgetRepository.findAllByOrganizationNameIsOrderByFiscalYearDesc(any()))
                .thenReturn(Optional.empty());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class, () -> {
            accessor.getBudgetsForOrganization("Cheese Club");
        });

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());
    }

    @Test
    @DisplayName("Get Budget by ID")
    void getBudgetById() {
        // Arrange
        Budget budget = createSimpleBudget();

        when(budgetRepository.findById(any())).thenReturn(Optional.of(budget));

        BudgetDetailedDto detailedDto = BudgetDetailedDto.builder()
                .id(1)
                .fiscalYear("FY 19")
                .build();

        when(budgetHelperFunctions.translateBudgetToBudgetDetailedDto(any())).thenReturn(detailedDto);

        // Act
        BudgetDetailedDto returnedData = accessor.getBudgetById(1);

        // Assert
        assertEquals(1, returnedData.getId());
        assertEquals("FY 19", returnedData.getFiscalYear());
    }

    @Test
    @DisplayName("Get Budget by ID not found")
    void getBudgetByIdNotFound() {
        // Arrange
        when(budgetRepository.findById(any())).thenReturn(Optional.empty());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class, () -> {
            accessor.getBudgetById(1);
        });

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());
    }

    @Test
    void getBudgets() {
        // Arrange


        // Act


        // Assert

    }
}