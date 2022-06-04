package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.AllMetadata;
import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.enums.ClubClassification;
import edu.wpi.sga.treasury.application.enums.ClubType;
import edu.wpi.sga.treasury.application.enums.FiscalClass;
import edu.wpi.sga.treasury.domain.model.StudentLifeFee;
import edu.wpi.sga.treasury.domain.repository.StudentLifeFeeRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

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
    public Response<AllMetadata> getAllMetadata() {
        AllMetadata allMetadata = AllMetadata.builder()
                .clubClassifications(getClassificationMap())
                .clubTypes(getClubTypeMap())
                .fiscalClasses(getFiscalClassList())
                .fiscalYears(getFiscalYearMap())
                .build();

        return new Response<>(allMetadata);
    }

    @Override
    public Response<Map<Integer, String>> getClassifications() {
        return new Response<>(getClassificationMap());
    }

    @Override
    public Response<Map<Integer, String>> getClubTypes() {

        return new Response<>(getClubTypeMap());
    }

    @Override
    public Response<Map<Integer, String>> getFiscalYears() {
        return new Response<>(getFiscalYearMap());
    }

    @Override
    public ListResponse<String> getFiscalClasses() {
        List<String> fiscalClasses = getFiscalClassList();

        return new ListResponse<>(fiscalClasses);
    }

    /* Private Helpers */
    private Map<Integer, String> getClassificationMap() {
        return Stream.of(ClubClassification.values()).collect(Collectors.toMap(Enum::ordinal, ClubClassification::getName));
    }

    private Map<Integer, String> getClubTypeMap() {
        return Stream.of(ClubType.values()).collect(Collectors.toMap(Enum::ordinal, ClubType::getName));
    }

    private Map<Integer, String> getFiscalYearMap() {
        return studentLifeFeeRepository.findAll().stream().collect(Collectors.toMap(StudentLifeFee::getId, StudentLifeFee::getFiscalYear));
    }

    private List<String> getFiscalClassList() {
        return Stream.of(FiscalClass.values()).map(FiscalClass::getName).collect(Collectors.toList());
    }
}
