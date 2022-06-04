package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.AllMetadata;
import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;

import java.util.Map;

public interface MetadataAccessor {
    Response<AllMetadata> getAllMetadata();

    /**
     * @return
     */
    Response<Map<Integer, String>> getClassifications();

    /**
     * @return
     */
    Response<Map<Integer, String>> getClubTypes();

    /**
     * @return
     */
    Response<Map<Integer, String>> getFiscalYears();

    /**
     * @return
     */
    ListResponse<String> getFiscalClasses();
}
