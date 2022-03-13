using Application.Services.Implementation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Service
{
    public class EmailSyntaxValidatorServiceTests
    {
        private readonly EmailSyntaxValidator validator;

        public EmailSyntaxValidatorServiceTests()
        {
            validator = new EmailSyntaxValidator();
        }

        [Fact]
        public void Given_NullMailAddress_Then_Should_ThrowArgumentNullException()
        {
            //ARRANGE
            string emailAddress = null;
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => validator.IsEmailValid(emailAddress));
        }

        [Fact]
        public void Given_EmptyMailAddress_Then_Should_ThrowArgumentNullException()
        {
            //ARRANGE
            string emailAddress = "";
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => validator.IsEmailValid(emailAddress));
        }

        [Fact]
        public void Given_MailAddressNoDomain_Then_Should_ReturnFalse()
        {
            //ARRANGE
            string emailAddress = "giani69";
            //ACT
            //ASSERT
            Assert.False(validator.IsEmailValid(emailAddress));
        }

        [Fact]
        public void Given_MailAddressEmptyDomain_Then_Should_ReturnFalse()
        {
            //ARRANGE
            string emailAddress = "giani69@";
            //ACT
            //ASSERT
            Assert.False(validator.IsEmailValid(emailAddress));
        }

        [Fact]
        public void Given_MailAddressInvalidDomain_Then_Should_ReturnFalse()
        {
            //ARRANGE
            string emailAddress = "giani69@.com";
            //ACT
            //ASSERT
            Assert.False(validator.IsEmailValid(emailAddress));
        }

        [Fact]
        public void Given_CompleteMailAddress_Then_Should_ReturnTrue()
        {
            //ARRANGE
            string emailAddress = "giani69@sefu.com";
            //ACT
            //ASSERT
            Assert.True(validator.IsEmailValid(emailAddress));
        }
    }
}
