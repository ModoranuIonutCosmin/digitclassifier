namespace Application.Services.Implementation
{
    public interface IEmailSyntaxValidator
    {
        bool IsEmailValid(string email);
    }
}