package edu.wpi.sga.treasury.api.contract.response;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;
import java.util.List;

@Builder
@Getter
@Setter
public class PagedResponse<T> {
    private List<T> data;
    private String message;

    @Builder.Default
    private LocalDateTime timestamp = LocalDateTime.now();

    private int pageNumber;
    private int resultsPerPage;
    private int maxResults;
}
