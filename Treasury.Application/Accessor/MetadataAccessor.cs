using System.Collections.Generic;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;

namespace Treasury.Application.Accessor
{
    public class MetadataAccessor
    {
        private readonly sgadbContext _dbContext;

        public MetadataAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ClassificationDto> GetClassifications()
        {
            // TODO: Refactor Database to make classifications an id name pairing
            return new List<ClassificationDto>
            {
                new()
                {
                    Id = 0,
                    Classification = "Class 1 - Special Interest"
                },
                new()
                {
                    Id = 1,
                    Classification = "Class 2 - Club Sports"
                },
                new()
                {
                    Id = 2,
                    Classification = "Class 3 - Campus Wide"
                },
                new()
                {
                    Id = 3,
                    Classification = "Class 4 - Selective Membership"
                },
                new()
                {
                    Id = 4,
                    Classification = "Class 5 - Greek Life"
                },
                new()
                {
                    Id = 5,
                    Classification = "Class 6 - Provisional"
                },
                new()
                {
                    Id = 6,
                    Classification = "Class 7 - Non-Funded"
                },
                new()
                {
                    Id = 7,
                    Classification = "Classless"
                },
                new()
                {
                    Id = 8,
                    Classification = "Department"
                },
                new()
                {
                    Id = 9,
                    Classification = "Graduate"
                },
                new()
                {
                    Id = 10,
                    Classification = "Mandatory Transfer"
                }
            };
        }

        public List<ClubTypeDto> GetClubTypes()
        {
            // TODO: Refactor Database to make club types an id name pairing
            return new List<ClubTypeDto>
            {
                new()
                {
                   Id = 0,
                   Type = "Campus Wide"
                },
                new()
                {
                   Id = 1,
                   Type = "Classless"
                },
                new()
                {
                   Id = 2,
                   Type = "Cultural"
                },
                new()
                {
                   Id = 3,
                   Type = "Greek Life"
                },
                new()
                {
                   Id = 4,
                   Type = "International"
                },
                new()
                {
                   Id = 5,
                   Type = "Misc Interest"
                },
                new()
                {
                   Id = 6,
                   Type = "Performing and Media"
                },
                new()
                {
                   Id = 7,
                   Type = "Professional"
                },
                new()
                {
                   Id = 8,
                   Type = "Selective Membership"
                },
                new()
                {
                   Id = 9,
                   Type = "Societal Awareness"
                },
                new()
                {
                   Id = 10,
                   Type = "Sport"
                },
                new()
                {
                   Id = 11,
                   Type = "STEM Focused"
                },
                
            };
        }

        public List<object> GetMqpTypes()
        {
            // TODO: Refactor Database to make mqp types an id name pairing or roll in to organization
            return new List<object>()
            {

            };
        }
    }
}