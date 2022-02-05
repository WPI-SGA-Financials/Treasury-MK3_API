package edu.wpi.sga.treasury.application.accessor;

import java.util.List;
import java.util.Map;

public interface MetadataAccessor {
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
