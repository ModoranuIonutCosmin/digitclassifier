using Application.Models.History;
using Application.Services;
using DataAcces.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Service
{
    public class FavoritesServiceTests
    {
        private readonly DITests diKernel;

        public FavoritesServiceTests()
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
        public async Task Given_FavoritesController_When_GetFavoritesIsCalled_Then_Should_ContainAllEntries()
        {
            //ARRANGE
            var favoritesService = diKernel.ResolveService<IFavoritesService>();
            var requestingUser = SetupRequestingUser();
            var favoritesRequest = new FavoritesRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };

            //ACT
            var favoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);

            //ASSERT
            Assert.Contains(favoritesResponse.FavoritesResponseList, e => e.Image == "yH5BAEAAAAALAAAAAABAAEAAAIBRAA8");
        }

        [Fact]
        public async Task Given_FavoritesController_When_GetFavoritesIsCalledNoEntriesOnAPage_Then_Should_ContainEmptyList()
        {
            //ARRANGE
            var favoritesService = diKernel.ResolveService<IFavoritesService>();
            var requestingUser = SetupRequestingUser();
            var favoritesRequest = new FavoritesRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 420,
                Filter = "dateTime",
                SortOrder = "desc"
            };

            //ACT
            var favoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);

            //ASSERT
            Assert.Empty(favoritesResponse.FavoritesResponseList);
        }

        [Fact]
        public async Task Given_FavoritesController_When_AddFavoritesIsCalledWithEmpty_Then_Should_RaiseArgumentNullException()
        {
            //ARRANGE
            var favoritesService = diKernel.ResolveService<IFavoritesService>();

            //ACT

            //ASSERT
            _ = await Assert.ThrowsAsync<ArgumentNullException>(async () => await favoritesService.AddFavoriteEntry(new Guid()));
        }

        [Fact]
        public async Task Given_FavoritesController_When_RemoveFavoritesIsCalledWithEmpty_Then_Should_RaiseArgumentNullException()
        {
            //ARRANGE
            var favoritesService = diKernel.ResolveService<IFavoritesService>();

            //ACT

            //ASSERT
            _ = await Assert.ThrowsAsync<ArgumentNullException>(async () => await favoritesService.DeleteFavoriteEntry(new Guid()));
        }

        [Fact]
        public async Task Given_FavoritesController_When_AddFavoritesIsCalledIfAlreadyFavorite_Then_Should_NotChange()
        {
            //ARRANGE
            var favoritesService = diKernel.ResolveService<IFavoritesService>();
            var historyService = diKernel.ResolveService<IHistoryService>();
            var requestingUser = SetupRequestingUser();
            var favoritesRequest = new FavoritesRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };
            var historyRequest = new HistoryRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };

            //ACT
            var historyResponse = await historyService.GetHistoryForUserAsync(requestingUser, historyRequest);
            var entry = historyResponse.HistoryResponseList.Find(el => el.IsFavorite);

            var firstFavoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);
            await favoritesService.AddFavoriteEntry(entry.Id);
            var secondFavoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);

            //ASSERT
            Assert.True(firstFavoritesResponse.FavoritesResponseList.Count == secondFavoritesResponse.FavoritesResponseList.Count);
            Assert.Contains(firstFavoritesResponse.FavoritesResponseList, e => e.Id == entry.Id);
            Assert.Contains(secondFavoritesResponse.FavoritesResponseList, e => e.Id == entry.Id);
        }

        [Fact]
        public async Task Given_FavoritesController_When_AddFavoritesIsCalledIfNotFavorite_Then_Should_ContainOneMoreEntity()
        {
            //ARRANGE
            var favoritesService = diKernel.ResolveService<IFavoritesService>();
            var historyService = diKernel.ResolveService<IHistoryService>();
            var requestingUser = SetupRequestingUser();
            var favoritesRequest = new FavoritesRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };
            var historyRequest = new HistoryRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };

            //ACT
            var historyResponse = await historyService.GetHistoryForUserAsync(requestingUser, historyRequest);
            var entry = historyResponse.HistoryResponseList.Find(el => !el.IsFavorite);

            var firstFavoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);
            await favoritesService.AddFavoriteEntry(entry.Id);
            var secondFavoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);

            //ASSERT
            Assert.True(firstFavoritesResponse.FavoritesResponseList.Count < secondFavoritesResponse.FavoritesResponseList.Count);
            Assert.DoesNotContain(firstFavoritesResponse.FavoritesResponseList, e => e.Id == entry.Id);
            Assert.Contains(secondFavoritesResponse.FavoritesResponseList, e => e.Id == entry.Id);
        }

        [Fact]
        public async Task Given_FavoritesController_When_DeleteFavoritesIsCalledIfNotFavorite_Then_Should_ContainOneLessEntity()
        {
            //ARRANGE
            var favoritesService = diKernel.ResolveService<IFavoritesService>();
            var historyService = diKernel.ResolveService<IHistoryService>();
            var requestingUser = SetupRequestingUser();
            var favoritesRequest = new FavoritesRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };
            var historyRequest = new HistoryRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };

            //ACT
            var historyResponse = await historyService.GetHistoryForUserAsync(requestingUser, historyRequest);
            var entry = historyResponse.HistoryResponseList.Find(el => !el.IsFavorite);

            var firstFavoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);
            await favoritesService.DeleteFavoriteEntry(entry.Id);
            var secondFavoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);

            //ASSERT
            Assert.True(firstFavoritesResponse.FavoritesResponseList.Count == secondFavoritesResponse.FavoritesResponseList.Count);
            Assert.DoesNotContain(firstFavoritesResponse.FavoritesResponseList, e => e.Id == entry.Id);
            Assert.DoesNotContain(secondFavoritesResponse.FavoritesResponseList, e => e.Id == entry.Id);
        }

        [Fact]
        public async Task Given_FavoritesController_When_DeleteFavoritesIsCalledIfFavorite_Then_Should_ContainOneLessEntity()
        {
            //ARRANGE
            var favoritesService = diKernel.ResolveService<IFavoritesService>();
            var historyService = diKernel.ResolveService<IHistoryService>();
            var requestingUser = SetupRequestingUser();
            var favoritesRequest = new FavoritesRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };

            //ACT
            var firstFavoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);
            var entry = firstFavoritesResponse.FavoritesResponseList[0];
            await favoritesService.DeleteFavoriteEntry(entry.Id);
            var secondFavoritesResponse = await favoritesService.GetFavoritesAsync(requestingUser, favoritesRequest);

            //ASSERT
            Assert.True(firstFavoritesResponse.FavoritesResponseList.Count > secondFavoritesResponse.FavoritesResponseList.Count);
            Assert.Contains(firstFavoritesResponse.FavoritesResponseList, e => e.Id == entry.Id);
            Assert.DoesNotContain(secondFavoritesResponse.FavoritesResponseList, e => e.Id == entry.Id);
        }
    }
}
