using System.Collections.Generic;
using Treasury.Application.DTOs;

namespace Treasury.Application.Accessor.Interface;

public interface IStudentLifeFeeAccessor
{
    List<StudentLifeFeeDto> GetSlfs();
    StudentLifeFeeDto GetSlfByFy(int fy);
}