package edu.wpi.sga.treasury.application.accessor;

import java.util.List;

public interface StudentLifeFeeAccessor {
    /**
     *
     * @return
     */
    List<?> getStudentLifeFees();

    /**
     *
     * @param fy
     * @return
     */
    Object getStudentLifeFeeByFy(int fy);
}
