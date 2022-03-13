using System;

namespace DataAcces.Entities
{
    public class PredictionRating : Common.BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public int Stars { get; set; }
        public History Prediction { get; set; }
    }
}
