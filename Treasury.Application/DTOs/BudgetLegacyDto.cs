namespace Treasury.Application.DTOs;

public class BudgetLegacyDto
{
    public int Id { get; set; }

    public decimal AmountRequested { get; set; }

    public decimal AmountProposed { get; set; }

    public bool Appealed { get; set; }

    public decimal AppealAmount { get; set; }

    public string AppealDecision { get; set; }

    public decimal ApprovedAppeal { get; set; }

    public decimal AmountApproved => AmountProposed + ApprovedAppeal;
}