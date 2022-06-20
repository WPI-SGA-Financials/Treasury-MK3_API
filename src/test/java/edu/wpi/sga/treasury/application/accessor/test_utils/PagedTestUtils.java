package edu.wpi.sga.treasury.application.accessor.test_utils;

import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.util.PagedHelperFunctions;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;

import java.util.List;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;

public class PagedTestUtils {
    public static void mockBasicFiltering(PagedHelperFunctions pagedHelperFunctions){
        Pageable pageable = PageRequest.of(0, 10);

        when(pagedHelperFunctions.generatePagedRequest(any())).thenReturn(pageable);

        PagedRequest cleanedRequest = new PagedRequest();
        cleanedRequest.setName(List.of("Cheese Club", "Student"));

        when(pagedHelperFunctions.cleanRequest(any())).thenReturn(cleanedRequest);
    }

    public static void mockBasicPagedRequest(PagedHelperFunctions pagedHelperFunctions) {
        Pageable pageable = PageRequest.of(0, 10);

        when(pagedHelperFunctions.generatePagedRequest(any())).thenReturn(pageable);

        PagedRequest cleanedRequest = new PagedRequest();
        cleanedRequest.setPage(1);

        when(pagedHelperFunctions.cleanRequest(any())).thenReturn(cleanedRequest);
    }
}
