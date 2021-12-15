using System.Collections.Generic;
using System.Linq;
using Treasury.Application.Accessor.Interface;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;

namespace Treasury.Application.Accessor.Implementation;

public class StudentLifeFeeAccessorImpl : IStudentLifeFeeAccessor
{
    private readonly sgadbContext _dbContext;

    public StudentLifeFeeAccessorImpl(sgadbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<StudentLifeFeeDto> GetSlfs()
    {
        var slf = _dbContext.StudentLifeFees
            .OrderByDescending(slf => slf.FiscalYear)
            .Select(slf => StudentLifeFeeDto.CreateDtoFromSlf(slf))
            .ToList();

        return slf;
    }

    public StudentLifeFeeDto GetSlfByFy(int fy)
    {
        var fiscalYear = fy.ToString().PadLeft(2, '0');

        var slf = _dbContext.StudentLifeFees
            .FirstOrDefault(slf => slf.FiscalYear.Equals("FY " + fiscalYear));

        return slf != null ? StudentLifeFeeDto.CreateDtoFromSlf(slf) : null;
    }
}