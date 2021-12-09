using System.Collections.Generic;
using Treasury.Application.DTOs;

namespace Treasury.Application.Accessor.Interface
{
    public interface IMetadataAccessor
    {
        List<ClassificationDto> GetClassifications();
        List<ClubTypeDto> GetClubTypes();
        List<object> GetMqpTypes();
        List<FiscalYearDto> GetFiscalYears();
    }
}