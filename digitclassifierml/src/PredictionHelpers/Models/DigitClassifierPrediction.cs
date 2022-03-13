using System;
using System.Linq;

namespace MLPredict.Models
{
    public class DigitClassifierPrediction
    {
        public float[] Score;

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return Score.Select((e, index) => $"Digit {index}, Confidence={e}")
                .Aggregate((e1, e2) => e1 + Environment.NewLine + e2);
        }

        public int MostLikely => Score.ToList().IndexOf(Score.Max());
    }
}
