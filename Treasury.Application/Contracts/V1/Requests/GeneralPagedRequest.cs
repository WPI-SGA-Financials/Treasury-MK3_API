using System;
using System.ComponentModel.DataAnnotations;

namespace Treasury.Application.Contracts.V1.Requests;

public class GeneralPagedRequest : IPagedRequest
{
    public string[] Name { get; set; } = Array.Empty<string>();

    public string[] Acronym { get; set; } = Array.Empty<string>();

    public string[] Classification { get; set; } = Array.Empty<string>();

    public string[] Type { get; set; } = Array.Empty<string>();

    public bool IncludeInactive { get; set; } = false;

    [Required]
    public int Page { get; set; } = 1;

    [Required]
    public int Rpp { get; set; } = 10;
}