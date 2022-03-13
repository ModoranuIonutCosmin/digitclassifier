using Application.Models.Image;
using Application.Services;
using DataAcces.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Service
{
    public class ImageServiceTests
    {
        private readonly DITests diKernel;

        public ImageServiceTests()
        {
            diKernel = new DITests();
        }

        public User SetupRequestingUser()
        {
            var requestingUser = new User()
            {
                Id = Guid.Parse("05d173fa-e47e-4ab6-84e0-2cd577109d63"),
                UserName = "Giani199",
                FirstName = "Giani",
                LastName = "M.",
                Password = "parola",
                Email = "giani69@sefu.com",
            };

            return requestingUser;
        }

        [Fact]
        public async Task Given_ImageService_When_GetPredictionCalled_Then_Should_ShouldReturnProperPredictionFormat()
        {
            ImageResponse forcedPostRequestResponse = new ImageResponse()
            {
                DigitPredicted = 6,
                PredictionLikelihood = 0.9
            };

            diKernel.AddMockHttpClient(forcedPostRequestResponse);

            IImageService imageService = diKernel.ResolveService<IImageService>();

            var finalResponse = await imageService.Predict(SetupRequestingUser(),
                new ImageRequest()
                {
                    Base64Image = "yH5BAEAAAAALAAAAAABAAEAAAIBRAA8"
                });

            Assert.NotNull(finalResponse);
        }


    }
}
