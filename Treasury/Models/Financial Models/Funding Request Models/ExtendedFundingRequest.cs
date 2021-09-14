using System;
using System.Collections.Generic;
using Treasury.Models.Financial_Models.Funding_Request_Models;

namespace Treasury.Models.Financial_Models
{
    public class ExtendedFundingRequest
    {
        private ExtendedFundingRequest(FundingRequest fr)
        {
            ID = fr.ID;
            NameOfClub = fr.NameOfClub;
            HearingDate = fr.HearingDate;
            FiscalYear = fr.FiscalYear;
            DotNumber = fr.DotNumber;
            Description = fr.Description;
            AmountRequested = fr.AmountRequested;
            Decision = fr.Decision;
            AmountApproved = fr.AmountApproved;
        }
        
        public int ID { get; set; }
        
        public string NameOfClub { get; set; }
        
        public string FiscalYear { get; set; }
        
        public string DotNumber { get; set; }
        
        public string Description { get; set; }
        
        public DateTime HearingDate { get; set; }
        
        public decimal AmountRequested { get; set; }

        public string Decision { get; set; }

        public decimal AmountApproved { get; set; }

        // TODO: When meeting system is implemented, Meeting Details

        public static ExtendedFundingRequest createFromFR(FundingRequest fr)
        {
            return new ExtendedFundingRequest(fr);
        }
    }
}