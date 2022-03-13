using Api.Controllers;
using Application.Models.History;
using Application.Services;
using Application.UnitTests.Helpers;
using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.API
{
    public class FavoritesServiceTests
    {
        public readonly FavoritesController favoritesController;
        public readonly IFavoritesService favoritesService;
        public FavoritesServiceTests()
        {
            favoritesService = A.Fake<IFavoritesService>();

            favoritesController = new FavoritesController(favoritesService);
        }

        [Fact]
        public async Task Given_FavoritesController_When_GetFavoritesIsCalled_Then_ShouldCallServiceOnce()
        {
            var authorizedUser = favoritesController.MockUserAuthorizationForController();

            var request = new FavoritesRequest
            {
                ElementsPerPage = 10,
                PageNumber = 0
            };
            await favoritesController.GetFavorites(request);

            A.CallTo(() => favoritesService.GetFavoritesAsync(authorizedUser, request))
                .MustHaveHappenedOnceExactly();
        }
    }
}
