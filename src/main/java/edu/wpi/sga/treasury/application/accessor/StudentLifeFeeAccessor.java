package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.StudentLifeFeeDto;

import java.util.List;

public interface StudentLifeFeeAccessor {
    /**
     * Get all Student Life Fees
     *
     * @return List of Student Life Fees
     */
    List<StudentLifeFeeDto> getStudentLifeFees();

    /**
     * Get a Student Life Fee by Fiscal Year
     *
     * @param fy Fiscal Year
     * @return Student Life Fee for the Fiscal Year
     */
    StudentLifeFeeDto getStudentLifeFeeByFy(String fy);
}
