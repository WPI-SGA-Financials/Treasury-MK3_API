namespace Treasury.Application.Errors
{
    public class NotFoundError : IError
    {
        public NotFoundError(string message, string searchQuery)
        {
            StatusCode = 404;
            Type = "Not found error";
            Message = message;
            SearchQuery = searchQuery;
        }
        
        
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string SearchQuery { get; set; }
    }
}