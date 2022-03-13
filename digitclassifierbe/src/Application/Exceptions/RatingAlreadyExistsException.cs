using System;

namespace Application.Exceptions
{
    public class RatingAlreadyExistsException : Exception
    {
        public RatingAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
