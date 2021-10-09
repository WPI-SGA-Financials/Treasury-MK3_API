using System;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs
{
    public class ReallocationRequestDto
    {
        public static ReallocationRequestDto CreateDtoFromRealloc(Reallocation realloc)
        {
            ReallocationRequestDto dto = new ReallocationRequestDto
            {
                Id = realloc.Id,
                NameOfClub = realloc.NameOfClub,
                HearingDate = realloc.HearingDate,
                FiscalYear = realloc.FiscalYear,
                DotNumber = realloc.DotNumber,
                Description = realloc.Description,
                AllocationAmount = realloc.AllocationAmount,
                Decision = realloc.Decision,
                AmountApproved = realloc.AmountApproved
            };

            return dto;
        }
        
        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public DateTime HearingDate { get; set; }
        public string FiscalYear { get; set; }
        public string DotNumber { get; set; }
        public string Description { get; set; }
        public decimal AllocationAmount { get; set; }
        public string Decision { get; set; }
        public decimal AmountApproved { get; set; }
    }

    public class ReallocationRequestDetailedDto : ReallocationRequestDto
    {
        public static ReallocationRequestDetailedDto CreateDtoFromRealloc(Reallocation realloc)
        {
            ReallocationRequestDetailedDto dto = new ReallocationRequestDetailedDto
            {
                Id = realloc.Id,
                NameOfClub = realloc.NameOfClub,
                HearingDate = realloc.HearingDate,
                FiscalYear = realloc.FiscalYear,
                DotNumber = realloc.DotNumber,
                AllocationAmount = realloc.AllocationAmount,
                Decision = realloc.Decision,
                AmountApproved = realloc.AmountApproved,
                Description = realloc.Description,
                AllocatedFrom = realloc.AllocatedFrom,
                AllocatedTo = realloc.AllocatedTo,
                Timestamp = realloc.Timestamp
            };

            return dto;
        }
        
        public string AllocatedFrom { get; set; }
        public string AllocatedTo { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
}