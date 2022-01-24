package edu.wpi.sga.treasury.application.accessor;

import edu.wpi.sga.treasury.api.contract.request.PagedRequest;
import edu.wpi.sga.treasury.application.dto.FundingRequestDetailedDto;
import edu.wpi.sga.treasury.application.dto.FundingRequestDto;
import edu.wpi.sga.treasury.application.mapper.FundingRequestMapper;
import edu.wpi.sga.treasury.domain.model.FundingRequest;
import edu.wpi.sga.treasury.domain.repository.FundingRequestRepository;
import lombok.RequiredArgsConstructor;
import org.hibernate.cfg.NotYetImplementedException;
import org.mapstruct.factory.Mappers;
import org.springframework.data.domain.Page;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class FundingRequestAccessorImpl implements FundingRequestAccessor {
    // Repositories
    private final FundingRequestRepository fundingRequestRepository;

    // Mappers
    private final FundingRequestMapper fundingRequestMapper = Mappers.getMapper(FundingRequestMapper.class);

    @Override
    public List<FundingRequestDto> getFundingRequestsForOrganization(String organization) {
        throw new NotYetImplementedException();
    }

    @Override
    public Page<FundingRequestDto> getFundingRequests(PagedRequest request) {
        throw new NotYetImplementedException();
    }

    @Override
    public FundingRequestDetailedDto getFundingRequestById(Integer id) {
        Optional<FundingRequest> request = fundingRequestRepository.findById(id);

        if (request.isPresent()) {
            return fundingRequestMapper.fundingRequestToFundingRequestDetailedDto(request.get());
        } else {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND);
        }
    }
}
