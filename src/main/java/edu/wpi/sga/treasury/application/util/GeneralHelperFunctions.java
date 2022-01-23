package edu.wpi.sga.treasury.application.util;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Component;

import java.util.Arrays;

@Component
public class GeneralHelperFunctions {
    public Pageable generatePagedRequest(PagedRequest request) {
        return PageRequest.of(request.getPage() - 1, request.getResultsPerPage());
    }

    public boolean determineFilterable(PagedRequest request) {
        if (!request.getName().isEmpty()) return true;
        if (!request.getAcronym().isEmpty()) return true;
        if (!request.getClassification().isEmpty()) return true;
        if (!request.getType().isEmpty()) return true;
        if (!request.getDescription().isEmpty()) return true;
        if (!request.getFiscalClass().isEmpty()) return true;
        if (!request.getFiscalYear().isEmpty()) return true;
        return request.isIncludeInactive();
    }

    public PagedRequest cleanRequest(PagedRequest request) {
        if (!request.getName().isEmpty()) {
            request.getName().replaceAll(String::trim);
            request.getName().removeAll(Arrays.asList("", null));
        }

        if (!request.getAcronym().isEmpty()) {
            request.getAcronym().replaceAll(String::trim);
            request.getAcronym().removeAll(Arrays.asList("", null));
        }

        if (!request.getClassification().isEmpty()) {
            request.getClassification().replaceAll(String::trim);
            request.getClassification().removeAll(Arrays.asList("", null));
        }

        if (!request.getType().isEmpty()) {
            request.getType().replaceAll(String::trim);
            request.getType().removeAll(Arrays.asList("", null));
        }

        if (!request.getDescription().isEmpty()) {
            request.getDescription().replaceAll(String::trim);
            request.getDescription().removeAll(Arrays.asList("", null));
        }

        if (!request.getFiscalClass().isEmpty()) {
            request.getFiscalClass().replaceAll(String::trim);
            request.getFiscalClass().removeAll(Arrays.asList("", null));
        }

        if (!request.getFiscalYear().isEmpty()) {
            request.getFiscalYear().replaceAll(String::trim);
            request.getFiscalYear().removeAll(Arrays.asList("", null));
        }

        return request;
    }
}
