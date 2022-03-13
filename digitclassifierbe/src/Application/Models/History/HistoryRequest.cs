
namespace Application.Models.History
{
    public class HistoryRequest
    {
        public int ElementsPerPage { get; set; }
        public int PageNumber { get; set; }
        public string Filter { get; set; }
        public string SortOrder { get; set; }
    }
}
