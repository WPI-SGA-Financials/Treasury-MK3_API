package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDetailedDto;
import edu.wpi.sga.treasury.application.dto.budget.BudgetDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.mapper.custom.BudgetMapperCustom;
import edu.wpi.sga.treasury.application.util.PagedHelperFunctions;
import edu.wpi.sga.treasury.domain.model.budget.Budget;
import edu.wpi.sga.treasury.domain.model.Organization;
import edu.wpi.sga.treasury.domain.repository.BudgetRepository;
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

import static edu.wpi.sga.treasury.application.accessor.test_utils.BudgetAccessorTestUtils.createSimpleBudget;
import static edu.wpi.sga.treasury.application.accessor.test_utils.PagedTestUtils.mockBasicFiltering;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class BudgetAccessorTest {

    BudgetAccessor accessor;

    // Repositories
    @Mock
    private BudgetRepository budgetRepository;

    // Util
    @Mock
    private BudgetMapperCustom budgetMapperCustom;
    @Mock
    private PagedHelperFunctions pagedHelperFunctions;

    @BeforeEach
    void setUp() {
        accessor = new BudgetAccessorImpl(budgetRepository, budgetMapperCustom, pagedHelperFunctions);
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
                .thenReturn(List.of(budget));

        BudgetDto budgetDto = BudgetDto.builder()
                .id(1)
                .fiscalYear("FY 19")
                .nameOfClub("Student Government Association")
                .build();

        when(budgetMapperCustom.toBudgetDtos(any()))
                .thenReturn(List.of(budgetDto));

        // Act
        ListResponse<BudgetDto> returnedData = accessor.getBudgetsForOrganization("Student Government Association");

        // Assert
        assertEquals(1, returnedData.getData().get(0).getId());
        assertEquals("FY 19", returnedData.getData().get(0).getFiscalYear());
        assertEquals("Student Government Association", returnedData.getData().get(0).getNameOfClub());
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

        when(budgetMapperCustom.toBudgetDetailedDto(any())).thenReturn(detailedDto);

        // Act
        Response<BudgetDetailedDto> returnedData = accessor.getBudgetById(1);

        // Assert
        assertEquals(1, returnedData.getData().getId());
        assertEquals("FY 19", returnedData.getData().getFiscalYear());
    }

    @Test
    @DisplayName("Get Budget by ID not found")
    void getBudgetByIdNotFound() {
        // Arrange
        when(budgetRepository.findById(any())).thenReturn(Optional.empty());

        // Act
        ResponseStatusException exception = assertThrows(ResponseStatusException.class, () -> accessor.getBudgetById(1));

        // Assert
        assertEquals(HttpStatus.NOT_FOUND, exception.getStatus());
    }

    @Test
    @DisplayName("Get Paged and Filtered list of Budgets")
    void getBudgetsFiltered() {
        // Arrange
        mockBasicFiltering(pagedHelperFunctions);

        Budget budget = createSimpleBudget();

        when(budgetRepository.findAll(any(Specification.class), any(Pageable.class))).thenReturn(new PageImpl<>(List.of(budget)));

        BudgetDto budgetDto = BudgetDto.builder()
                .id(1)
                .fiscalYear("FY 19")
                .build();

        when(budgetMapperCustom.toBudgetDtos(any())).thenReturn(List.of(budgetDto));

        // Act
        PagedRequest request = new PagedRequest();
        request.setName(List.of("Cheese Club", "Student", " "));

        PagedResponse<BudgetDto> returnedData = accessor.getBudgets(request);

        // Assert
        assertEquals(1L, returnedData.getMaxResults());
        assertEquals(1, returnedData.getData().get(0).getId());
        assertEquals("FY 19", returnedData.getData().get(0).getFiscalYear());
    }
}