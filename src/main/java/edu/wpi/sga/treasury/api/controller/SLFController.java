package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.accessor.StudentLifeFeeAccessor;
import edu.wpi.sga.treasury.application.dto.StudentLifeFeeDto;
import lombok.RequiredArgsConstructor;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequiredArgsConstructor
public class SLFController {
    private final StudentLifeFeeAccessor studentLifeFeeAccessor;

    @GetMapping(value = "/financials/slf")
    public ListResponse<StudentLifeFeeDto> getStudentLifeFees() {
        return studentLifeFeeAccessor.getStudentLifeFees();
    }

    @GetMapping(value = "/financials/slf/{fy}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<StudentLifeFeeDto> getStudentLifeFeeByFY(@PathVariable String fy) {
        return studentLifeFeeAccessor.getStudentLifeFeeByFy(fy);
    }
}
