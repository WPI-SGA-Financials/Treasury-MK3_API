package edu.wpi.sga.treasury.application.accessor;

import java.util.List;

public interface MetadataAccessor {
    /**
     *
     * @return
     */
    List<?> getClassifications();

    /**
     *
     * @return
     */
    List<?> getClubTypes();

    /**
     *
     * @return
     */
    List<?> getFiscalYears();

    /**
     *
     * @return
     */
    List<String> getFiscalClasses();
}
