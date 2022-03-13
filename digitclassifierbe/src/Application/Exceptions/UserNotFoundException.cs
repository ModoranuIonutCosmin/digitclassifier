using System;

namespace Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string input) : base(input)
        {

        }
    }
}
