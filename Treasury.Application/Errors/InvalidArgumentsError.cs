using System;
using System.Collections.Generic;

namespace Treasury.Application.Errors
{
    [Serializable]
    public class InvalidArgumentsError
    {
        public InvalidArgumentsError(string message, Dictionary<string, object> searchQuery)
        {
            StatusCode = 400;
            Type = "Invalid arguments error";
            Message = message;
            SearchQuery = searchQuery;
        }
        
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> SearchQuery { get; set; }
        public string Type { get; set; }
    }
}