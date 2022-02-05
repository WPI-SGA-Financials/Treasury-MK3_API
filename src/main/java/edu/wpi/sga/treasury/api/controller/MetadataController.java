package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.api.contract.response.Response;
import edu.wpi.sga.treasury.application.accessor.MetadataAccessor;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;
import java.util.Map;

@RestController
@RequiredArgsConstructor
public class MetadataController {
    private final MetadataAccessor metadataAccessor;

    @GetMapping(value = "/metadata/classification")
    public Response<Map<Integer, String>> getClubClassifications() {
        Map<Integer, String> data = metadataAccessor.getClassifications();

        return Response.<Map<Integer, String>>builder()
                .data(data)
                .message("Successfully retrieved all Club Classifications")
                .build();
    }

    @GetMapping(value = "/metadata/club-types")
    public Response<Map<Integer, String>> getClubTypes() {
        Map<Integer, String> data = metadataAccessor.getClubTypes();

        return Response.<Map<Integer, String>>builder()
                .data(data)
                .message("Successfully retrieved all Club Types")
                .build();
    }

    @GetMapping(value = "/metadata/fiscal-years")
    public Response<Map<Integer, String>> getFiscalYears() {
        Map<Integer, String> data = metadataAccessor.getFiscalYears();

        return Response.<Map<Integer, String>>builder()
                .data(data)
                .message("Successfully retrieved all Fiscal Years")
                .build();
    }

    @GetMapping(value = "/metadata/fiscal-classes")
    public Response<List<String>> getFiscalClasses() {
        List<String> data = metadataAccessor.getFiscalClasses();

        return Response.<List<String>>builder()
                .data(data)
                .message("Successfully retrieved all Fiscal Classes")
                .build();
    }
}
