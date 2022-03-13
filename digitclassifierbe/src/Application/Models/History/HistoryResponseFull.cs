

using System.Collections.Generic;

namespace Application.Models.History
{
    public class HistoryResponseFull
    {
        public List<HistoryResponse> HistoryResponseList { get; set; }
        public int NumberOfPages { get; set; }
    }
}
