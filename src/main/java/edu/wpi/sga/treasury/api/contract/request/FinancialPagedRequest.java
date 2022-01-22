package edu.wpi.sga.treasury.api.contract.request;

import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
public class FinancialPagedRequest implements PagedRequest {
    private Integer page = 1;
    private Integer resultsPerPage = 10;

    private List<String> name;
    private List<String> acronym;
    private List<String> classification;
    private List<String> type;
    private Boolean includeInactive = false;

    private List<String> description;
    private List<String> fiscalClass;
    private List<String> fiscalYear;
    private Integer minimumRequestedAmount;
}
