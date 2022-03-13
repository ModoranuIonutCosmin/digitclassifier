using Application.Models.History;

namespace Application.Models.Ratings
{
    public class RatingStats
    {
        public HistoryResponse Prediction { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingVotesCount { get; set; }
    }
}
