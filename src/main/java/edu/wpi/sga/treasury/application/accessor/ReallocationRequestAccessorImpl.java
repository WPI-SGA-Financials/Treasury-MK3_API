package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDto;
import edu.wpi.sga.treasury.application.mapper.ReallocationRequestMapper;
import edu.wpi.sga.treasury.application.util.GeneralHelperFunctions;
import edu.wpi.sga.treasury.domain.model.ReallocationRequest;
import edu.wpi.sga.treasury.domain.repository.ReallocationRequestRepository;
import lombok.RequiredArgsConstructor;
import org.mapstruct.factory.Mappers;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageImpl;
import org.springframework.data.domain.Pageable;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class ReallocationRequestAccessorImpl implements ReallocationRequestAccessor {

    // Repositories
    private final ReallocationRequestRepository reallocationRequestRepository;

    // Mappers
    private final ReallocationRequestMapper reallocationRequestMapper = Mappers.getMapper(ReallocationRequestMapper.class);

    // Utils
    private final GeneralHelperFunctions generalHelperFunctions;

    @Override
    public List<ReallocationRequestDto> getReallocationRequestsForOrganization(String organization) {
        Optional<List<ReallocationRequest>> orgReallocs = reallocationRequestRepository.findAllByOrganizationNameOrderByHearingDateDesc(organization);

        if(orgReallocs.isPresent() && orgReallocs.get().size() > 0) {
            return orgReallocs.get().stream().map(reallocationRequestMapper::reallocationRequestToReallocationRequestDto).collect(Collectors.toList());
        }

        throw new ResponseStatusException(HttpStatus.NOT_FOUND);
    }

    @Override
    public Page<ReallocationRequestDto> getReallocationRequests(PagedRequest request) {
        Pageable pageable = generalHelperFunctions.generatePagedRequest(request);

        Page<ReallocationRequest> reallocs;

        request = generalHelperFunctions.cleanRequest(request);

        if(generalHelperFunctions.determineFilterable(request)) {
            reallocs = reallocationRequestRepository.findReallocsByFilters(request);
        } else {
            reallocs = reallocationRequestRepository.findAllByOrderByHearingDateDescDotNumberDesc(pageable);
        }

        List<ReallocationRequestDto> dtos = reallocs.getContent()
                .stream()
                .map(reallocationRequestMapper::reallocationRequestToReallocationRequestDto)
                .collect(Collectors.toList());

        return new PageImpl<>(dtos, pageable, reallocs.getTotalElements());
    }

    @Override
    public ReallocationRequestDetailedDto getReallocationRequestById(Integer id) {
        Optional<ReallocationRequest> realloc = reallocationRequestRepository.findById(id);

        if(realloc.isPresent()) {
            return reallocationRequestMapper.reallocationRequestToReallocationRequestDetailedDto(realloc.get());
        }

        throw new ResponseStatusException(HttpStatus.NOT_FOUND);
    }
}
