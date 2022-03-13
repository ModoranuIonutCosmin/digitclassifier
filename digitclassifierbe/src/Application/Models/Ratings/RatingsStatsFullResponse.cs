using System.Collections.Generic;

namespace Application.Models.Ratings
{
    public class RatingsStatsFullResponse
    {
        public List<RatingStats> PredictionsStats { get; set; }
        public int TotalPredictionsCount { get; set; }
    }
}
