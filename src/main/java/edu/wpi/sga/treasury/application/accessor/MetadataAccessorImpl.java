package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.AllMetadata;
import edu.wpi.sga.treasury.application.enums.ClubClassification;
import edu.wpi.sga.treasury.application.enums.ClubType;
import edu.wpi.sga.treasury.application.enums.FiscalClass;
import edu.wpi.sga.treasury.domain.model.StudentLifeFee;
import edu.wpi.sga.treasury.domain.repository.StudentLifeFeeRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;
import java.util.stream.Stream;

@Service
@RequiredArgsConstructor
public class MetadataAccessorImpl implements MetadataAccessor {

    // Repositories
    private final StudentLifeFeeRepository studentLifeFeeRepository;

    @Override
    public AllMetadata getAllMetadata() {
        return AllMetadata.builder()
                .clubClassifications(getClassifications())
                .clubTypes(getClubTypes())
                .fiscalClasses(getFiscalClasses())
                .fiscalYears(getFiscalYears())
                .build();
    }

    @Override
    public Map<Integer, String> getClassifications() {
        return Stream.of(ClubClassification.values()).collect(Collectors.toMap(Enum::ordinal, ClubClassification::getName));
    }

    @Override
    public Map<Integer, String> getClubTypes() {
        return Stream.of(ClubType.values()).collect(Collectors.toMap(Enum::ordinal, ClubType::getName));
    }

    @Override
    public Map<Integer, String> getFiscalYears() {
        return studentLifeFeeRepository.findAll().stream().collect(Collectors.toMap(StudentLifeFee::getId, StudentLifeFee::getFiscalYear));
    }

    @Override
    public List<String> getFiscalClasses() {
        return Stream.of(FiscalClass.values()).map(FiscalClass::getName).collect(Collectors.toList());
    }
}
