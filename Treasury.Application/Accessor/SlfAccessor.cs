using System.Collections.Generic;
using System.Linq;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Application.Errors;
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

        public object GetSlfByFy(int fy)
        {
            Dictionary<string, object> errorDict = new Dictionary<string, object>();
            
            if (fy is < 1 or > 99)
            {
                errorDict.Add("fy", "Fiscal Year is out of bounds");
                return new InvalidArgumentsError("One or more parameters is invalid", errorDict);
            }

            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            StudentLifeFee slf = _dbContext.StudentLifeFees
                .FirstOrDefault(slf => slf.FiscalYear.Equals("FY " + fiscalYear));
            
            if (slf != null)
            {
                return StudentLifeFeeDto.CreateDtoFromSlf(slf);
            }
            
            errorDict = new Dictionary<string, object>
            {
                { "fy", fy }
            };

            return new NotFoundError("The requested student life was not found", errorDict);
        }
    }
}