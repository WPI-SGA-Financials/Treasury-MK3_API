package edu.wpi.sga.treasury.api.contract.response;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Builder
@Getter
@Setter
public class PagedResponse<T> {
    private List<T> data;
    private String message;
    private int pageNumber;
    private int resultsPerPage;
    private int maxResults;
}
