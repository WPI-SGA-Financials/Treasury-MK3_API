package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.StudentLifeFeeDto;
import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;

public interface StudentLifeFeeAccessor {
    /**
     * Get all Student Life Fees
     *
     * @return List of Student Life Fees
     */
    ListResponse<StudentLifeFeeDto> getStudentLifeFees();

    /**
     * Get a Student Life Fee by Fiscal Year
     *
     * @param fy Fiscal Year
     * @return Student Life Fee for the Fiscal Year
     */
    Response<StudentLifeFeeDto> getStudentLifeFeeByFy(String fy);
}
