using System;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs;

public class FundingRequestDto
{
    public int Id { get; set; }

    public string NameOfClub { get; set; }

    public DateTime HearingDate { get; set; }

    public string FiscalYear { get; set; }

    public string DotNumber { get; set; }

    public string Description { get; set; }

    public decimal AmountRequested { get; set; }

    public string Decision { get; set; }

    public decimal AmountApproved { get; set; }

    public static FundingRequestDto CreateDtoFromFr(FundingRequest fr)
    {
        var dto = new FundingRequestDto
        {
            Id = fr.Id,
            NameOfClub = fr.NameOfClub,
            HearingDate = fr.FundingDate,
            FiscalYear = fr.FiscalYear,
            DotNumber = fr.DotNumber,
            Description = fr.Description,
            AmountRequested = fr.AmountRequested,
            Decision = fr.Decision,
            AmountApproved = fr.AmountApproved
        };

        return dto;
    }
}

public class FundingRequestDetailedDto : FundingRequestDto
{
    public DateTime? DateOfEvent { get; set; }

    public FundingRequestAppealDto FrAppeal { get; set; }

    public static FundingRequestDetailedDto CreateDtoFromFr(FundingRequest fr)
    {
        var dto = new FundingRequestDetailedDto
        {
            Id = fr.Id,
            NameOfClub = fr.NameOfClub,
            HearingDate = fr.FundingDate,
            FiscalYear = fr.FiscalYear,
            DotNumber = fr.DotNumber,
            AmountRequested = fr.AmountRequested,
            Decision = fr.Decision,
            AmountApproved = fr.AmountApproved,
            Description = fr.Description,
            DateOfEvent = fr.DateOfEvent,
            FrAppeal = fr.Frappeal != null ? FundingRequestAppealDto.CreateDtoFromAppeal(fr.Frappeal) : null
        };

        return dto;
    }
}