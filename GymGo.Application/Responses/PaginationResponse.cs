namespace GymGo.Application.Responses
{
    public class PaginationResponse<T>(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        public IEnumerable<T> Items { get; set; } = items;
        public int TotalCount { get; set; } = totalCount;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
    }
}
