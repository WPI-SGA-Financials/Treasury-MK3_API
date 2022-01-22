package edu.wpi.sga.treasury.api.contract.response;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Builder
@Getter
@Setter
public class PagedResponse<T> {
    private T data;
    private String message;
    private int pageNumber;
    private int resultsPerPage;
    private int maxResults;
}
