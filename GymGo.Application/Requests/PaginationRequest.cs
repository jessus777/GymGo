namespace GymGo.Application.Requests
{
    public class PaginationRequest<TOrderBy>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public TOrderBy OrderBy { get; set; } = default;
        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}
