using System;
using System.Collections.Generic;

namespace Treasury.Application.Errors
{
    [Serializable]
    public class NotFoundError
    {
        public NotFoundError(string message, Dictionary<string, object> searchQuery)
        {
            StatusCode = 404;
            Type = "Not found error";
            Message = message;
            SearchQuery = searchQuery;
        }
        
        
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> SearchQuery { get; set; }
    }
}