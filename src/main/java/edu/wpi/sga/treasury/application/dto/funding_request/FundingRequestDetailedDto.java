package edu.wpi.sga.treasury.application.dto.funding_request;

import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;
import java.time.LocalDate;

@Getter
@Setter
public class FundingRequestDetailedDto {
    private Integer id;
    private String nameOfClub;
    private String description;
    private LocalDate hearingDate;
    private String fiscalYear;
    private LocalDate dateOfEvent;
    private String dotNumber;
    private BigDecimal amountRequested;
    private String decision;
    private BigDecimal amountApproved;
    private FRAppealDto frAppeal;
}
