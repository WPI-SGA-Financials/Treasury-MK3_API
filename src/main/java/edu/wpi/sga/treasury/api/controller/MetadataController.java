package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.accessor.MetadataAccessor;
import edu.wpi.sga.treasury.application.dto.misc.AllMetadata;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.Map;

@RestController
@RequiredArgsConstructor
public class MetadataController {
    private final MetadataAccessor metadataAccessor;

    @GetMapping(value = "/metadata/all")
    public Response<AllMetadata> getAllMetadata() {
        return metadataAccessor.getAllMetadata();
    }

    @GetMapping(value = "/metadata/classification")
    public Response<Map<Integer, String>> getClubClassifications() {
        return metadataAccessor.getClassifications();
    }

    @GetMapping(value = "/metadata/club-types")
    public Response<Map<Integer, String>> getClubTypes() {
        return metadataAccessor.getClubTypes();
    }

    @GetMapping(value = "/metadata/fiscal-years")
    public Response<Map<Integer, String>> getFiscalYears() {
        return metadataAccessor.getFiscalYears();
    }

    @GetMapping(value = "/metadata/fiscal-classes")
    public ListResponse<String> getFiscalClasses() {
        return metadataAccessor.getFiscalClasses();
    }
}
