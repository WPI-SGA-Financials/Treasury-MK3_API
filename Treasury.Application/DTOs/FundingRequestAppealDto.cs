using System;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs
{
    public class FundingRequestAppealDto
    {
        public static FundingRequestAppealDto CreateDtoFromAppeal(Frappeal appeal)
        {
            FundingRequestAppealDto dto = new FundingRequestAppealDto
            {
                Id = appeal.Id,
                NewDotNumber = appeal.NewDotNumber,
                AppealDate = appeal.AppealDate,
                Description = appeal.Description,
                AppealAmount = appeal.AppealAmount,
                Decision = appeal.Decision,
                ApprovedAppeal = appeal.ApprovedAppeal,
                MinutesLink = appeal.MinutesLink
            };

            return dto;
        }
        
        public int Id { get; set; }
        public string NewDotNumber { get; set; }
        public DateTime AppealDate { get; set; }
        public string Description { get; set; }
        public decimal AppealAmount { get; set; }
        public string Decision { get; set; }
        public decimal ApprovedAppeal { get; set; }
        public string MinutesLink { get; set; }
    }
}