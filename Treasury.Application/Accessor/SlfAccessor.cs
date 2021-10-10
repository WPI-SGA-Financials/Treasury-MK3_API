using System.Collections.Generic;
using System.Linq;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor
{
    public class SlfAccessor
    {
        private sgadbContext _dbContext;

        public SlfAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public List<StudentLifeFeeDto> GetSlfs()
        {
            List<StudentLifeFeeDto> slf = _dbContext.StudentLifeFees
                .OrderByDescending(slf => slf.FiscalYear)
                .Select(slf => StudentLifeFeeDto.CreateDtoFromSlf(slf))
                .ToList();

            return slf;
        }

        public StudentLifeFeeDto GetSlfByFy(int fy)
        {
            StudentLifeFee slf = _dbContext.StudentLifeFees
                .FirstOrDefault(slf => slf.FiscalYear.Contains(""+ fy));

            return StudentLifeFeeDto.CreateDtoFromSlf(slf);
        }
    }
}