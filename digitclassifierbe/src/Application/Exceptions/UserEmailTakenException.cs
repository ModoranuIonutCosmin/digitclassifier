using System;

namespace Application.Exceptions
{
    public class UserEmailTakenException : Exception
    {
        public UserEmailTakenException(string message) : base(message)
        {
        }
    }
}
