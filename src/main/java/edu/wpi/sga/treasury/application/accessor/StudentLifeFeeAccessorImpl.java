package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.StudentLifeFeeDto;
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
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class StudentLifeFeeAccessorImpl implements StudentLifeFeeAccessor {

    // Repositories
    private final StudentLifeFeeRepository studentLifeFeeRepository;

    // Mappers
    private final StudentLifeFeeMapper studentLifeFeeMapper = Mappers.getMapper(StudentLifeFeeMapper.class);

    @Override
    public List<StudentLifeFeeDto> getStudentLifeFees() {
        return studentLifeFeeRepository.findAll().stream().map(studentLifeFeeMapper::studentLifeFeeToStudentLifeFeeDto).collect(Collectors.toList());
    }

    @Override
    public StudentLifeFeeDto getStudentLifeFeeByFy(String fy) {
        Optional<StudentLifeFee> studentLifeFee = studentLifeFeeRepository.findByFiscalYear(fy);

        if (studentLifeFee.isPresent()) {
            return studentLifeFeeMapper.studentLifeFeeToStudentLifeFeeDto(studentLifeFee.get());
        }

        throw new ResponseStatusException(HttpStatus.NOT_FOUND);
    }
}
