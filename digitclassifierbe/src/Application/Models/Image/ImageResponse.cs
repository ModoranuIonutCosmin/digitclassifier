using System;

namespace Application.Models.Image
{
    public class ImageResponse
    {
        public int DigitPredicted { get; set; }
        public double PredictionLikelihood { get; set; }
        public Guid HistoryEntryId { get; set; }
    }
}
