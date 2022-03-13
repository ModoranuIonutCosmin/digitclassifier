using System;
using System.Net.Mail;

namespace Application.Services.Implementation
{
    public class EmailSyntaxValidator : IEmailSyntaxValidator
    {
        public bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException($"email cannot be null.");
            }

            try
            {
               _ = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
