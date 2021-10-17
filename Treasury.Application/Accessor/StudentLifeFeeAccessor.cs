using System.Collections.Generic;
using System.Linq;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor
{
    public class StudentLifeFeeAccessor
    {
        private readonly sgadbContext _dbContext;

        public StudentLifeFeeAccessor(sgadbContext dbContext)
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
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            StudentLifeFee slf = _dbContext.StudentLifeFees
                .FirstOrDefault(slf => slf.FiscalYear.Equals("FY " + fiscalYear));
            
            return slf != null ? StudentLifeFeeDto.CreateDtoFromSlf(slf) : null;
        }
    }
}