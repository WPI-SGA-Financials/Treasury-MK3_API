namespace Treasury.Application.Contracts.V1.Requests
{
    public interface IPagedRequest
    {
        public int Page { get; set; }
        public int Rpp { get; set; }
    }
}