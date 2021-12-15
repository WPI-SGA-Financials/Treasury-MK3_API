using System;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs;

public class OrganizationDto
{
    public string NameOfClub { get; set; }

    public string Classification { get; set; }

    public string TypeOfClub { get; set; }

    public string AccountNumber { get; set; }

    public string Acronym { get; set; }

    public bool Inactive { get; set; }

    public DateTime Timestamp { get; set; }

    public static OrganizationDto CreateDtoFromOrg(Organization org)
    {
        var dto = new OrganizationDto
        {
            NameOfClub = org.NameOfClub,
            Classification = org.Classification,
            TypeOfClub = org.TypeOfClub,
            AccountNumber = org.AccountNumber,
            Acronym = org.Acronym1,
            Inactive = org.Inactive,
            Timestamp = org.Timestamp
        };

        return dto;
    }
}

public class OrganizationDetailDto : OrganizationDto
{
    public string FiVizClubClassification { get; set; }

    public string TechsyncName { get; set; }

    public static OrganizationDetailDto CreateDtoFromOrg(Organization org)
    {
        return new OrganizationDetailDto
        {
            NameOfClub = org.NameOfClub,
            Classification = org.Classification,
            TypeOfClub = org.TypeOfClub,
            AccountNumber = org.AccountNumber,
            Acronym = org.Acronym1,
            Inactive = org.Inactive,
            Timestamp = org.Timestamp,
            FiVizClubClassification = org.ClubCategory?.Category,
            TechsyncName = org.TechsyncInfo?.TechsyncName1
        };
    }
}