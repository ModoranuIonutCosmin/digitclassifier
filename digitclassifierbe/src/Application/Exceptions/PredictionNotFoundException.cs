using System;

namespace Application.Exceptions
{
    public class PredictionNotFoundException : Exception
    {
        public PredictionNotFoundException(string message) : base(message)
        {
        }
    }
}
