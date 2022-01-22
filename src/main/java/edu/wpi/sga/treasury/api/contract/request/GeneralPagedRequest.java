package edu.wpi.sga.treasury.api.contract.request;


import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
public class GeneralPagedRequest implements PagedRequest {
    private Integer page = 1;
    private Integer resultsPerPage = 10;

    private List<String> name;
    private List<String> acronym;
    private List<String> classification;
    private List<String> type;
    private Boolean includeInactive = false;
}
