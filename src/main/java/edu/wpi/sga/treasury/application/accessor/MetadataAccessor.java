package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.AllMetadata;

import java.util.List;
import java.util.Map;

public interface MetadataAccessor {
    AllMetadata getAllMetadata();

    /**
     *
     * @return
     */
    Map<Integer, String> getClassifications();

    /**
     *
     * @return
     */
    Map<Integer, String> getClubTypes();

    /**
     *
     * @return
     */
    Map<Integer, String> getFiscalYears();

    /**
     *
     * @return
     */
    List<String> getFiscalClasses();
}
