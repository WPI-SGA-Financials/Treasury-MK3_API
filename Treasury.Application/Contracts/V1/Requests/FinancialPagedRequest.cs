using System;
using System.ComponentModel.DataAnnotations;

namespace Treasury.Application.Contracts.V1.Requests;

public class FinancialPagedRequest : IPagedRequest
{
    public string[] Name { get; set; } = Array.Empty<string>();

    public string[] Acronym { get; set; } = Array.Empty<string>();

    public string[] Classification { get; set; } = Array.Empty<string>();

    public string[] Type { get; set; } = Array.Empty<string>();

    public bool IncludeInactive { get; set; } = false;

    public string[] Description { get; set; } = Array.Empty<string>();

    public string[] FiscalClass { get; set; } = Array.Empty<string>();

    public string[] FiscalYear { get; set; } = Array.Empty<string>();

    public string[] DotNumber { get; set; } = Array.Empty<string>();

    // Minimum Requested Amount
    public int MinimumRequestedAmount { get; set; } = 0;

    [Required]
    public int Page { get; set; } = 1;

    [Required]
    public int Rpp { get; set; } = 10;

    // TODO: Maximum Requested Amount

    // Date Range
}