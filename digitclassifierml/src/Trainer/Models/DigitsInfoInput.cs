using Microsoft.ML.Data;

namespace Trainer.Models
{
    public class DigitsInfoInput
    {

        [VectorType(64), LoadColumn(0, 63)]
        public float[] PixelAlphas;

        [LoadColumn(64)]
        public float TargetValue;
    }
}
