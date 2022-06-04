package edu.wpi.sga.treasury.api.controller;

import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.accessor.StudentLifeFeeAccessor;
import edu.wpi.sga.treasury.application.dto.StudentLifeFeeDto;
import lombok.RequiredArgsConstructor;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequiredArgsConstructor
public class SLFController {
    private final StudentLifeFeeAccessor studentLifeFeeAccessor;

    @GetMapping(value = "/financials/slf")
    public Response<List<StudentLifeFeeDto>> getStudentLifeFees() {
        List<StudentLifeFeeDto> data = studentLifeFeeAccessor.getStudentLifeFees();

        return Response.<List<StudentLifeFeeDto>>builder()
                .data(data)
                .message("Successfully retrieved all Student Life Fees")
                .build();
    }

    @GetMapping(value = "/financials/slf/{fy}", produces = MediaType.APPLICATION_JSON_VALUE)
    public Response<StudentLifeFeeDto> getStudentLifeFeeByFY(@PathVariable String fy) {
        StudentLifeFeeDto dto = studentLifeFeeAccessor.getStudentLifeFeeByFy(fy);

        return Response.<StudentLifeFeeDto>builder()
                .data(dto)
                .message("Successfully retrieved the Student Life Fee")
                .build();
    }
}
