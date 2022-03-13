using Api.Controllers;
using Application.Models.History;
using Application.Services;
using FakeItEasy;
using System.Threading.Tasks;
using Application.UnitTests.Helpers;
using Xunit;
using Application.Models.Image;

namespace Application.UnitTests.API
{
    public class ImageControllerTests
    {
        public readonly ImageController imageController;
        public readonly IImageService imageService;
        public ImageControllerTests()
        {
            imageService = A.Fake<IImageService>();

            imageController = new ImageController(imageService);
        }

        [Fact]
        public async Task Given_HistoryController_When_PredictIsCalled_Then_CallServiceOnce()
        {
            var authorizedUser = imageController.MockUserAuthorizationForController();

            var request = new ImageRequest
            {
                Base64Image = "yH5BAEAAAAALAAAAAABAAEAAAIBRAA7"
            };

            await imageController.Predict(request);

            A.CallTo(() => imageService.Predict(authorizedUser, request))
                .MustHaveHappenedOnceExactly();
        }
    }
}
