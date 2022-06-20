package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.application.dto.misc.ListResponse;
import edu.wpi.sga.treasury.application.dto.misc.Response;
import edu.wpi.sga.treasury.application.dto.pagination.PagedRequest;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDto;
import edu.wpi.sga.treasury.application.dto.pagination.PagedResponse;
import edu.wpi.sga.treasury.application.mapper.ReallocationRequestMapper;
import edu.wpi.sga.treasury.application.util.PagedHelperFunctions;
import edu.wpi.sga.treasury.domain.model.ReallocationRequest;
import edu.wpi.sga.treasury.domain.repository.ReallocationRequestRepository;
import edu.wpi.sga.treasury.domain.specification.ReallocationSpecification;
import lombok.RequiredArgsConstructor;
import org.mapstruct.factory.Mappers;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class ReallocationRequestAccessorImpl implements ReallocationRequestAccessor {

    // Repositories
    private final ReallocationRequestRepository reallocationRequestRepository;

    // Mappers
    private final ReallocationRequestMapper reallocationRequestMapper = Mappers.getMapper(ReallocationRequestMapper.class);

    // Utils
    private final PagedHelperFunctions pagedHelperFunctions;

    @Override
    public ListResponse<ReallocationRequestDto> getReallocationRequestsForOrganization(String organization) {
        List<ReallocationRequest> orgReallocs = reallocationRequestRepository.findAllByOrganizationNameOrderByHearingDateDesc(organization);

        return new ListResponse<ReallocationRequestDto>(orgReallocs, reallocationRequestMapper::toReallocDtos);
    }

    @Override
    public PagedResponse<ReallocationRequestDto> getReallocationRequests(PagedRequest request) {
        Pageable pageable = pagedHelperFunctions.generatePagedRequest(request);

        request = pagedHelperFunctions.cleanRequest(request);

        Specification<ReallocationRequest> specification = ReallocationSpecification.builder().request(request).build();

        Page<ReallocationRequest> reallocs = reallocationRequestRepository.findAll(specification, pageable);

        return new PagedResponse<>(reallocs, reallocationRequestMapper::toReallocDtos);
    }

    @Override
    public Response<ReallocationRequestDetailedDto> getReallocationRequestById(Integer id) {
        Optional<ReallocationRequest> optionalRealloc = reallocationRequestRepository.findById(id);

        return optionalRealloc.map(o -> new Response<>(o, reallocationRequestMapper::reallocationRequestToReallocationRequestDetailedDto))
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND));
    }
}
