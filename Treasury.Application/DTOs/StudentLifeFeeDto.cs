using System;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs;

public class StudentLifeFeeDto
{
    public int Id { get; set; }

    public string FiscalYear { get; set; }

    public decimal SlfAmount { get; set; }

    public int? FallStudentAmount { get; set; }

    public decimal? TotalStudentLifeFeeAmount => SlfAmount * FallStudentAmount;

    public DateTime Timestamp { get; set; }

    public static StudentLifeFeeDto CreateDtoFromSlf(StudentLifeFee slf)
    {
        var dto = new StudentLifeFeeDto
        {
            Id = slf.Id,
            FiscalYear = slf.FiscalYear,
            SlfAmount = slf.SlfAmount,
            FallStudentAmount = slf.FallStudentAmount,
            Timestamp = slf.Timestamp
        };

        return dto;
    }
}