using Api.Controllers;
using Application.Models.History;
using Application.Services;
using FakeItEasy;
using System.Threading.Tasks;
using Application.UnitTests.Helpers;
using Xunit;
using Application.Models.Ratings;
using System;

namespace Application.UnitTests.API
{
    public class RatingControllerTests
    {
        public readonly PredictionRatingsController ratingsController;
        public readonly IRatingService ratingService;
        public RatingControllerTests()
        {
            ratingService = A.Fake<IRatingService>();

            ratingsController = new PredictionRatingsController(ratingService);
        }

        [Fact]
        public async void Given_RatingsController_When_GetRatingIsCalled_Then_ShouldCallRatingsServiceGet()
        {
            var authorizedUser = ratingsController.MockUserAuthorizationForController();

            var request = new RatingsPaginatedRequest
            {
                Count = 10,
                Page = 1
            };

            await ratingsController.GetAllRatings(request);

            A.CallTo(() => ratingService.GetAllRatingStats(request.Page, request.Count))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Given_RatingsController_When_GetSingleRatingIsCalled_Then_ShouldCallRatingsServiceGet()
        {
            var authorizedUser = ratingsController.MockUserAuthorizationForController();

            _ = await ratingsController.GetRating(Guid.Parse("e398c9ba-fadd-42b7-bb88-c227e645d2e0"));

            A.CallTo(() => ratingService.GetRatingStats(Guid.Parse("e398c9ba-fadd-42b7-bb88-c227e645d2e0")))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Given_RatingsController_When_SubmitRatingIsCalled_Then_ShouldCallRatingsServiceSubmit()
        {
            var authorizedUser = ratingsController.MockUserAuthorizationForController();

            var submitRequest = new RateRequest
            {
                HistoryId = "e398c9ba-fadd-42b7-bb88-c227e645d2e0",
                StarsAmount = 3
            };
            await ratingsController.SubmitPredictionRating(submitRequest);

            A.CallTo(() => ratingService.RatePrediction(authorizedUser, submitRequest))
                .MustHaveHappenedOnceExactly();
        }

    }
}
