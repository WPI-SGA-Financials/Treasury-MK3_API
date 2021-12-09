using System.Collections.Generic;

namespace Treasury.Application.DTOs
{
    public class AllMetadataDto
    {
        public List<ClassificationDto> ClubClassifications { get; set; }
        public List<ClubTypeDto> ClubTypes { get; set; }
        
        public List<FiscalYearDto> FiscalYears { get; set; }
    }
}