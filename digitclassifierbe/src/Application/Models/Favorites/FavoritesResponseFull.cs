using System.Collections.Generic;

namespace Application.Models.History
{
    public class FavoritesResponseFull
    {
        public List<FavoritesResponse> FavoritesResponseList { get; set; }
        public int NumberOfPages { get; set; }
    }
}