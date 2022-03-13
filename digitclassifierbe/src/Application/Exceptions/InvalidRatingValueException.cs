using System;

namespace Application.Exceptions
{
    public class InvalidRatingValueException : Exception
    {
        public InvalidRatingValueException(string message) : base(message)
        {
        }
    }
}
