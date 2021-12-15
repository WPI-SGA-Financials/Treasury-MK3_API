using System.Collections.Generic;

namespace Treasury.Application.DTOs;

public class BudgetDto
{
    public int Id { get; set; }

    public string NameOfClub { get; set; }

    public string FiscalYear { get; set; }

    public int NumOfItems { get; set; }

    public decimal AmountRequested { get; set; }

    public decimal AmountProposed { get; set; }

    public decimal AmountApproved { get; set; }
}

public class BudgetDetailedDto : BudgetDto
{
#nullable enable
    public List<BudgetSectionDto>? Sections { get; set; }
#nullable disable
    public bool Appealed { get; set; }

    public decimal AppealAmount { get; set; }

    public string AppealDecision { get; set; }

    public decimal ApprovedAppeal { get; set; }
}