package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.ReallocationRequestDto;
import edu.wpi.sga.treasury.application.mapper.ReallocationRequestMapper;
import lombok.RequiredArgsConstructor;
import org.hibernate.cfg.NotYetImplementedException;
import org.mapstruct.factory.Mappers;
import org.springframework.data.domain.Page;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class ReallocationRequestAccessorImpl implements ReallocationRequestAccessor {

    // Repositories


    // Mappers
    private final ReallocationRequestMapper reallocationRequestMapper = Mappers.getMapper(ReallocationRequestMapper.class);

    @Override
    public List<ReallocationRequestDto> getReallocationRequestsForOrganization(String organization) {
        throw new NotYetImplementedException();
    }

    @Override
    public Page<ReallocationRequestDto> getReallocationRequests(PagedRequest request) {
        throw new NotYetImplementedException();
    }

    @Override
    public ReallocationRequestDetailedDto getReallocationRequestById(Integer id) {
        throw new NotYetImplementedException();
    }
}
