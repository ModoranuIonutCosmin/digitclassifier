using System;

namespace Application.Models.History
{
    public class AddFavoriteResponse
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public DateTime DateTime { get; set; }
        public int PredictedDigit { get; set; }
        public double PredictionProbability { get; set; }
    }
}