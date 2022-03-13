using System;

namespace Application.Exceptions
{
    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException(string input) : base(input)
        {

        }
    }
}
