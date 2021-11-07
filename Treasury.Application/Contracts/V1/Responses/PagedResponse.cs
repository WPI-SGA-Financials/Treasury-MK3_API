using System.Collections.Generic;

namespace Treasury.Application.Contracts.V1.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse(){}

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }

        public string Message { get; set; }

        public int? PageNumber { get; set; }

        public int? ResultsPerPage { get; set; }

        public int? MaxResults { get; set; }
    }
}