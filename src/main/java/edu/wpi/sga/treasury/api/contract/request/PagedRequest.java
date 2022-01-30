package edu.wpi.sga.treasury.api.contract.request;

import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
public class PagedRequest {
    private Integer page = 1;
    private Integer resultsPerPage = 10;

    private List<String> name = List.of();
    private List<String> acronym = List.of();
    private List<String> classification = List.of();
    private List<String> type = List.of();
    private boolean includeInactive = false;

    private List<String> description = List.of();
    private List<String> dotNumber = List.of();
    private List<String> fiscalClass = List.of();
    private List<String> fiscalYear = List.of();
    private Integer minimumRequestedAmount = 0;
}
