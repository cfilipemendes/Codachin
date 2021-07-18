namespace Codachin.Services.Dto
{
    public class PaginationFilter
    {
        public int Page { get; set; }
        public int PerPage { get; set; }

        public PaginationFilter()
        {
            this.Page = 1;
            this.PerPage = 30;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.Page = pageNumber < 1 ? 1 : pageNumber;
            this.PerPage = pageSize > 100 ? 100 : pageSize;
        }

    }
}
