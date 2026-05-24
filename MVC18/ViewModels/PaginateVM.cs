namespace MVC18.ViewModels
{
    public class PaginateVM
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string SortBy { get; set; } = string.Empty;
    }
}
