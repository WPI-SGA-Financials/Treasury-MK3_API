using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Accessor.Interface;
using Treasury.Application.Contexts;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.DTOs;
using Treasury.Application.Util;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor.Implementation
{
    public class OrganizationAccessorImpl : IOrganizationAccessor
    {
        private readonly sgadbContext _dbContext;

        public OrganizationAccessorImpl(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Organizations Data
        public List<OrganizationDto> GetOrganizations(GeneralPagedRequest generalPagedRequest, out int maxResults)
        {
            int skip = GeneralHelperFunctions.GetPage(generalPagedRequest);

            DbSet<Organization> baseQuery = _dbContext.Organizations;

            var filteredQuery = ApplyFilters(generalPagedRequest, baseQuery);

            maxResults = filteredQuery.Count();

            return filteredQuery
                    .OrderBy(org => org.NameOfClub)
                    .Skip(skip)
                    .Take(generalPagedRequest.Rpp)
                    .Select(org => OrganizationDto.CreateDtoFromOrg(org))
                    .ToList();
        }

        // Organization Data
        public OrganizationDetailDto GetOrganization(string name)
        {
            Organization org = _dbContext.Organizations
                .Include(org => org.ClubCategory)
                .Include(org => org.TechsyncInfo)
                .FirstOrDefault(org => org.NameOfClub.Equals(name.Trim()));

            return org != null ? OrganizationDetailDto.CreateDtoFromOrg(org) : null;
        }

        private static IQueryable<Organization> ApplyFilters(GeneralPagedRequest request, DbSet<Organization> orderedQueryable)
        {
            IQueryable<Organization> filtered = orderedQueryable;
            
            if (request.Name.Length > 0)
            {
                var predicate = PredicateBuilder.False<Organization>();

                predicate = request.Name.Aggregate(predicate, (current, name) => current.Or(p => p.NameOfClub.Contains(name)));

                filtered = filtered.Where(predicate);
            }
            
            if (request.Acronym.Length > 0)
            {
                var predicate = PredicateBuilder.False<Organization>();

                predicate = request.Acronym.Aggregate(predicate, (current, acronym) => current.Or(p => p.Acronym1.Contains(acronym)));

                filtered = filtered.Where(predicate);
            }
            
            if (request.Classification.Length > 0)
            {
                var predicate = PredicateBuilder.False<Organization>();
                
                predicate = request.Classification.Aggregate(predicate, (current, classification) => current.Or(p => p.Classification.Contains(classification)));

                filtered = filtered.Where(predicate);
            }
            
            if (request.Type.Length > 0)
            {
                var predicate = PredicateBuilder.False<Organization>();

                predicate = request.Type.Aggregate(predicate, (current, type) => current.Or(p => p.TypeOfClub.Contains(type)));

                filtered = filtered.Where(predicate);
            }
            
            if (!request.IncludeInactive)
            {
                filtered = filtered.Where(query => !query.Inactive);
            }

            return filtered;
        }
    }
}