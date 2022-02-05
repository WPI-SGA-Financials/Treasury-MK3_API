package edu.wpi.sga.treasury.application.mapper;

import edu.wpi.sga.treasury.application.dto.StudentLifeFeeDto;
import edu.wpi.sga.treasury.domain.model.StudentLifeFee;
import org.mapstruct.*;

@Mapper(unmappedTargetPolicy = ReportingPolicy.IGNORE, componentModel = "spring")
public interface StudentLifeFeeMapper {
    StudentLifeFee studentLifeFeeDtoToStudentLifeFee(StudentLifeFeeDto studentLifeFeeDto);

    StudentLifeFeeDto studentLifeFeeToStudentLifeFeeDto(StudentLifeFee studentLifeFee);

    @BeanMapping(nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE)
    void updateStudentLifeFeeFromStudentLifeFeeDto(StudentLifeFeeDto studentLifeFeeDto, @MappingTarget StudentLifeFee studentLifeFee);
}
