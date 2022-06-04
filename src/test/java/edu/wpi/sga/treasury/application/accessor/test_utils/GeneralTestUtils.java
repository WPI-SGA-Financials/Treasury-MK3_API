package edu.wpi.sga.treasury.application.accessor.test_utils;

import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;

import java.util.List;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;

public class GeneralTestUtils {
    public static void mockBasicFiltering(GeneralHelperFunctions generalHelperFunctions){
        Pageable pageable = PageRequest.of(0, 10);

        when(generalHelperFunctions.generatePagedRequest(any())).thenReturn(pageable);

        PagedRequest cleanedRequest = new PagedRequest();
        cleanedRequest.setName(List.of("Cheese Club", "Student"));

        when(generalHelperFunctions.cleanRequest(any())).thenReturn(cleanedRequest);
        when(generalHelperFunctions.determineFilterable(any())).thenReturn(true);
    }

    public static void mockBasicPagedRequest(GeneralHelperFunctions generalHelperFunctions) {
        Pageable pageable = PageRequest.of(0, 10);

        when(generalHelperFunctions.generatePagedRequest(any())).thenReturn(pageable);

        PagedRequest cleanedRequest = new PagedRequest();
        cleanedRequest.setPage(1);

        when(generalHelperFunctions.cleanRequest(any())).thenReturn(cleanedRequest);
        when(generalHelperFunctions.determineFilterable(any())).thenReturn(false);
    }
}
