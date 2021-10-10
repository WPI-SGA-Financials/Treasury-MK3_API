using System;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs
{
    public class FundingRequestDto
    {
        public static FundingRequestDto CreateDtoFromFr(FundingRequest fr)
        {
            FundingRequestDto dto = new FundingRequestDto
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
        
        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public DateTime HearingDate { get; set; }
        public string FiscalYear { get; set; }
        public string DotNumber { get; set; }
        public string Description { get; set; }
        public decimal AmountRequested { get; set; }
        public string Decision { get; set; }
        public decimal AmountApproved { get; set; }
    }

    public class FundingRequestDetailedDto : FundingRequestDto
    {
        public static FundingRequestDetailedDto CreateDtoFromFr(FundingRequest fr)
        {
            FundingRequestDetailedDto dto = new FundingRequestDetailedDto()
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
        
        public DateTime? DateOfEvent { get; set; }
        public FundingRequestAppealDto FrAppeal { get; set; }
    }
}