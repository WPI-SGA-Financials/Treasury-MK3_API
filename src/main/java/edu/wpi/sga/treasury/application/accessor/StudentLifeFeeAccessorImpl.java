package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.StudentLifeFeeDto;
import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.mapper.StudentLifeFeeMapper;
import edu.wpi.sga.treasury.domain.model.StudentLifeFee;
import edu.wpi.sga.treasury.domain.repository.StudentLifeFeeRepository;
import lombok.RequiredArgsConstructor;
import org.mapstruct.factory.Mappers;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class StudentLifeFeeAccessorImpl implements StudentLifeFeeAccessor {

    // Repositories
    private final StudentLifeFeeRepository studentLifeFeeRepository;

    // Mappers
    private final StudentLifeFeeMapper studentLifeFeeMapper = Mappers.getMapper(StudentLifeFeeMapper.class);

    @Override
    public ListResponse<StudentLifeFeeDto> getStudentLifeFees() {
        List<StudentLifeFee> lifeFees = studentLifeFeeRepository.findAll();

        return new ListResponse<>(lifeFees, studentLifeFeeMapper::toSlfDtos);
    }

    @Override
    public Response<StudentLifeFeeDto> getStudentLifeFeeByFy(String fy) {
        Optional<StudentLifeFee> optionalSlf = studentLifeFeeRepository.findByFiscalYear(fy);

        return optionalSlf.map(o -> new Response<>(o, studentLifeFeeMapper::studentLifeFeeToStudentLifeFeeDto))
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND));
    }
}
