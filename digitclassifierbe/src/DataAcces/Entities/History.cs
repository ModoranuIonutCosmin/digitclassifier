using System;
using System.Collections.Generic;

namespace DataAcces.Entities
{
    public class History : Common.BaseEntity
    {
        public string Image { get; set; }
        public DateTime DateTime { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public int PredictedDigit { get; set; }
        public double PredictionProbability { get; set; }
        public bool IsFavorite { get; set; }
        public List<PredictionRating> Ratings { get; set; }
    }
}
