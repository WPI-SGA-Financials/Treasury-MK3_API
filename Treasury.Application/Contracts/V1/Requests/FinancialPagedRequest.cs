namespace Treasury.Application.Contracts.V1.Requests
{
    public class FinancialPagedRequest : GeneralPagedRequest
    {
        public string FiscalClass { get; set; } = "";

        public int FiscalYear { get; set; } = 0;

        public int RequestedAmount { get; set; } = 0;

        // Date Range
    }
}