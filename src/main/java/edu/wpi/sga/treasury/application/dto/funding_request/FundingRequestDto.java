package edu.wpi.sga.treasury.application.dto.funding_request;

import lombok.Data;

import java.math.BigDecimal;
import java.time.LocalDate;

@Data
public class FundingRequestDto {
    private final Integer id;
    private final String nameOfClub;
    private final String description;
    private final LocalDate hearingDate;
    private final String fiscalYear;
    private final String dotNumber;
    private final BigDecimal amountRequested;
    private final String decision;
    private final BigDecimal amountApproved;
}
