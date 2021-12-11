using System.Linq;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Domain.Models;

namespace Treasury.Application.Util
{
    public static class GeneralHelperFunctions
    {
        public static int GetPage(IPagedRequest generalPagedRequest)
        {
            return generalPagedRequest.Page * generalPagedRequest.Rpp - generalPagedRequest.Rpp;
        }

        public static IQueryable<T> ApplyOrgBasedFilters<T>(FinancialPagedRequest request, IQueryable<T> queryable) where T : IOrgBasedEntity
        {
            IQueryable<T> filtered = queryable; 
            
            if (request.Acronym.Length > 0)
            {
                var predicate = PredicateBuilder.False<T>();

                predicate = request.Acronym.Aggregate(predicate,
                    (current, acronym) => current.Or(p => p.Organization.Acronym1.Contains(acronym)));

                filtered = filtered.Where(predicate);
            }

            if (request.Classification.Length > 0)
            {
                var predicate = PredicateBuilder.False<T>();

                predicate = request.Classification.Aggregate(predicate,
                    (current, classification) =>
                        current.Or(p => p.Organization.Classification.Contains(classification)));

                filtered = filtered.Where(predicate);
            }

            if (request.Type.Length > 0)
            {
                var predicate = PredicateBuilder.False<T>();

                predicate = request.Type.Aggregate(predicate,
                    (current, type) => current.Or(p => p.Organization.TypeOfClub.Contains(type)));

                filtered = filtered.Where(predicate);
            }

            if (!request.IncludeInactive)
            {
                filtered = filtered.Where(query => !query.Organization.Inactive);
            }

            return filtered;
        }
    }
}