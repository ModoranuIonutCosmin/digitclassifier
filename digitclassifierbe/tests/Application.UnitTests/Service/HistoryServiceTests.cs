using Application.Models.History;
using Application.Services;
using DataAcces.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Service
{
    public class HistoryServiceTests : DatabaseTest
    {
        private readonly DITests diKernel;

        public HistoryServiceTests()
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
        public async Task Given_HistoryController_When_GetHistoryIsCalled_Then_Should_ContainAllEntries()
        {
            //ARRANGE
            var historyService = diKernel.ResolveService<IHistoryService>();
            var requestingUser = SetupRequestingUser();
            var historyRequest = new HistoryRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };

            //ACT
            var historyResponse = await historyService.GetHistoryForUserAsync(requestingUser, historyRequest);

            //ASSERT
            Assert.Contains(historyResponse.HistoryResponseList, e => e.Image == "yH5BAEAAAAALAAAAAABAAEAAAIBRAA8");
        }


        [Fact]
        public async Task Given_HistoryController_When_GetHistoryIsCalledWithNoEntriesOnAPage_Then_Should_ContainEmptyList()
        {
            //ARRANGE
            var historyService = diKernel.ResolveService<IHistoryService>();
            var requestingUser = SetupRequestingUser();
            var historyRequest = new HistoryRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 1990,
                Filter = "dateTime",
                SortOrder = "desc"
            };

            //ACT
            var historyResponse = await historyService.GetHistoryForUserAsync(requestingUser, historyRequest);

            //ASSERT
            Assert.Empty(historyResponse.HistoryResponseList);
        }

        [Fact]
        public async Task Given_HistoryController_When_DeleteHistoryIsCalledWithEmpty_Then_Should_RaiseArgumentNullException()
        {
            //ARRANGE
            var historyService = diKernel.ResolveService<IHistoryService>();

            //ACT

            //ASSERT
            _ = await Assert.ThrowsAsync<ArgumentNullException>(async () => await historyService.DeleteHistoryEntry(new Guid()));
        }

        [Fact]
        public async Task Given_HistoryController_When_DeleteHistoryIsCalled_Then_Should_HaveOneLessEntry()
        {
            //ARRANGE
            var historyService = diKernel.ResolveService<IHistoryService>();
            var requestingUser = SetupRequestingUser();
            var historyRequest = new HistoryRequest()
            {
                ElementsPerPage = 10,
                PageNumber = 0,
                Filter = "dateTime",
                SortOrder = "desc"
            };

            //ACT
            var firstHistoryResponse = await historyService.GetHistoryForUserAsync(requestingUser, historyRequest);
            var toDelete = firstHistoryResponse.HistoryResponseList[0].Id;
            await historyService.DeleteHistoryEntry(toDelete);
            var secondHistoryResponse = await historyService.GetHistoryForUserAsync(requestingUser, historyRequest);

            //ASSERT
            Assert.True(secondHistoryResponse.HistoryResponseList.Count < firstHistoryResponse.HistoryResponseList.Count);
            Assert.DoesNotContain(secondHistoryResponse.HistoryResponseList, e => e.Id == toDelete);
        }
    }
}
