package edu.wpi.sga.treasury.application.util;

import edu.wpi.sga.treasury.api.contract.request.GeneralPagedRequest;
import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Component;

@Component
public class GeneralHelperFunctions {
    public Pageable generatePagedRequest(PagedRequest request) {
        return PageRequest.of(request.getPage() - 1, request.getResultsPerPage());
    }

    public boolean determineGeneralFilterable(GeneralPagedRequest request) {
        if (!request.getName().isEmpty()) return true;
        if (!request.getAcronym().isEmpty()) return true;
        if (!request.getClassification().isEmpty()) return true;
        if (!request.getType().isEmpty()) return true;
        return request.isIncludeInactive();
    }
}
