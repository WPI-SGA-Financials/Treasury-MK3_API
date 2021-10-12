namespace Treasury.Application.Errors
{
    public interface IError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string SearchQuery { get; set; }
        public string Type { get; set; }
    }
}