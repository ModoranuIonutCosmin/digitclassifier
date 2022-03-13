using Api.Controllers;
using Application.Models.History;
using Application.Services;
using FakeItEasy;
using System.Threading.Tasks;
using Application.UnitTests.Helpers;
using Xunit;

namespace Application.UnitTests.API
{
    public class HistoryControllerTests
    {
        public readonly HistoryController historyController;
        public readonly IHistoryService historyService;
        public HistoryControllerTests()
        {
            historyService = A.Fake<IHistoryService>();

            historyController = new HistoryController(historyService);
        }

        [Fact]
        public async Task Given_HistoryController_When_GetHistoryIsCalled_Then_ShouldCallServiceOnce()
        {
            var authorizedUser = historyController.MockUserAuthorizationForController();

            var request = new HistoryRequest
            {
                ElementsPerPage = 10,
                PageNumber = 1
            };
            await historyController.GetHistory(request);

            A.CallTo(() => historyService.GetHistoryForUserAsync(authorizedUser, request))
                .MustHaveHappenedOnceExactly();
        }
    }
}
