using System;

namespace Application.Exceptions
{
    public class RatingDoesNotExistsException : Exception
    {
        public RatingDoesNotExistsException(string message) : base(message)
        {
        }
    }
}
