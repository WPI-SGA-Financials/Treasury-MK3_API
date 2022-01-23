package edu.wpi.sga.treasury.api.contract.request;


import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
public class GeneralPagedRequest implements PagedRequest {
    private Integer page = 1;
    private Integer resultsPerPage = 10;

    private List<String> name = List.of();
    private List<String> acronym = List.of();
    private List<String> classification = List.of();
    private List<String> type = List.of();
    private boolean includeInactive = false;
}
