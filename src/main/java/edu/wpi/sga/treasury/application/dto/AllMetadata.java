package edu.wpi.sga.treasury.application.dto;


import lombok.Builder;
import lombok.Getter;

import java.util.List;
import java.util.Map;

@Builder
@Getter
public class AllMetadata {
    private Map<Integer, String> clubClassifications;
    private Map<Integer, String> clubTypes;
    private Map<Integer, String> fiscalYears;
    private List<String> fiscalClasses;
}
