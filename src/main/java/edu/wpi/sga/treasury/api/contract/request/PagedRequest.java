package edu.wpi.sga.treasury.api.contract.request;

public interface PagedRequest {
    Integer getPage();
    Integer getResultsPerPage();
}
