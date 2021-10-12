namespace Treasury.Application.Errors
{
    public class InvalidArgumentsError : IError
    {
        public InvalidArgumentsError(string message, string searchQuery)
        {
            StatusCode = 400;
            Type = "Invalid arguments error";
            Message = message;
            SearchQuery = searchQuery;
        }
        
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string SearchQuery { get; set; }
        public string Type { get; set; }
    }
}