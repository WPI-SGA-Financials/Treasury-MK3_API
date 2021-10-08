using System;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs
{
    public class OrganizationDto
    {
        public static OrganizationDto CreateDtoFromOrg(Organization org)
        {
            OrganizationDto dto = new OrganizationDto
            {
                NameOfClub = org.NameOfClub,
                Classification = org.Classification,
                TypeOfClub = org.TypeOfClub,
                AccountNumber = org.AccountNumber,
                Acronym1 = org.Acronym1,
                Inactive = org.Inactive == 1,
                Timestamp = org.Timestamp
            };

            return dto;
        }

        public string NameOfClub { get; set; }
        public string Classification { get; set; }
        public string TypeOfClub { get; set; }
        public string AccountNumber { get; set; }
        public string Acronym1 { get; set; }
        public bool Inactive { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class OrganizationDetailDto : OrganizationDto
    {
        public static OrganizationDetailDto CreateDtoFromOrg(Organization org)
        {
            return new OrganizationDetailDto
            {
                NameOfClub = org.NameOfClub,
                Classification = org.Classification,
                TypeOfClub = org.TypeOfClub,
                AccountNumber = org.AccountNumber,
                Acronym1 = org.Acronym1,
                Inactive = org.Inactive == 1,
                Timestamp = org.Timestamp,
                ClubClassification = org.ClubClassification.Category,
                TechsyncName = org.TechsyncName.TechsyncName1
            };
        }
        
        public string ClubClassification { get; set; }
        public string TechsyncName { get; set; }
    }
}