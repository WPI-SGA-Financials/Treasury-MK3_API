namespace Treasury.Application.DTOs;

public class BudgetSectionDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int NumOfItems { get; set; }

    public decimal AmountRequested { get; set; }

    public decimal AmountProposed { get; set; }

    public bool Appealed { get; set; }

    public decimal RequestedAppeal { get; set; }

    public decimal ApprovedAppeal { get; set; }

    public decimal AmountApproved => AmountProposed + ApprovedAppeal;
}