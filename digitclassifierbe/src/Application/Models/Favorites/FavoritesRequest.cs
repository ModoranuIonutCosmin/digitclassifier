namespace Application.Models.History
{
    public class FavoritesRequest
    {
        public int ElementsPerPage { get; set; }
        public int PageNumber { get; set; }
        public string Filter { get; set; }
        public string SortOrder { get; set; }
    }
}