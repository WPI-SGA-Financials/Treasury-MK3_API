using System;

#nullable disable

namespace Treasury.Domain.Models.Views
{
    public partial class CompleteFundingRequest
    {
        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public DateTime HearingDate { get; set; }
        public string FiscalYear { get; set; }
        public string AgendaNumber { get; set; }
        public string DotNumber { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfEvent { get; set; }
        public decimal Requested { get; set; }
        public string Decision { get; set; }
        public decimal Approved { get; set; }
        public string Notes { get; set; }
        public string MinutesLink { get; set; }
        public string Appealed { get; set; }
        public decimal? AppealAmount { get; set; }
        public string AppealDecision { get; set; }
        public decimal? ApprovedAppeal { get; set; }
        public string AppealMinutes { get; set; }
        public decimal? SpentAmount { get; set; }
        public string ReportFormStatus { get; set; }
        public decimal? ReportFormApprovedAmount { get; set; }
        public ulong? IdtSubmitted { get; set; }
        public string WorkdayApproved { get; set; }
        public DateTime? WorkdayApprovalDate { get; set; }
    }
}
