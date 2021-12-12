using System.ComponentModel.DataAnnotations;

namespace Treasury.Application.Contracts.V1.Requests
{
    public class FinancialPagedRequest : IPagedRequest
    {
        [Required] public int Page { get; set; } = 1;

        [Required] public int Rpp { get; set; } = 10;

        public string[] Name { get; set; } = System.Array.Empty<string>();
        
        public string[] Acronym { get; set; } = System.Array.Empty<string>();
        
        public string[] Classification { get; set; } = System.Array.Empty<string>();

        public string[] Type { get; set; } = System.Array.Empty<string>();
        
        public bool IncludeInactive { get; set; } = false;
        
        public string[] Description { get; set; } = System.Array.Empty<string>();
        
        public string[] FiscalClass { get; set; } = System.Array.Empty<string>();

        public string[] FiscalYear { get; set; } = System.Array.Empty<string>();

        // Minimum Requested Amount
        public int MinimumRequestedAmount { get; set; } = 0;

        // TODO: Maximum Requested Amount
        
        // Date Range
    }
}