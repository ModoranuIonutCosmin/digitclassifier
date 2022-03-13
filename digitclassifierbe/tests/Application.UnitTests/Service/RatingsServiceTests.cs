using Application.Exceptions;
using Application.Models.Ratings;
using Application.Services;
using DataAcces.Entities;
using DataAcces.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Service
{
    public class RatingsServiceTests
    {
        private DITests diKernel;

        public RatingsServiceTests()
        {
            diKernel = new DITests();
        }

        public User SetupRequestingUser()
        {
            var requestingUser = new User()
            {
                Id = Guid.Parse("c74faa1b-3f57-41f3-806c-1d7710faa89c"),
            };

            return requestingUser;
        }

        [Fact]
        public async Task Given_RatingService_When_RatePredictionIsCalled_Then_RatingShouldBeSubmitted()
        {
            var ratingService = diKernel.ResolveService<IRatingService>();
            var user = SetupRequestingUser();
            var context = diKernel.ResolveService<DatabaseContext>();

            await ratingService.RatePrediction(user, new RateRequest
            {
                HistoryId = "ff98c9ba-fadd-42b7-bb88-c227e645d2e0",
                StarsAmount = 3
            });

            Assert.True(context.Ratings
                       .Include(r => r.Prediction)
                       .Any(r => r.Stars == 3 && r.Prediction.Id == Guid.Parse("ff98c9ba-fadd-42b7-bb88-c227e645d2e0")));
        }


        [Fact]
        public async Task Given_RatingService_When_RatePredictionIsCalledWithImproperRatingValue_Then_ExceptionShouldBeThrown()
        {
            diKernel.Dispose();
            diKernel = new DITests();

            var ratingService = diKernel.ResolveService<IRatingService>();
            var user = SetupRequestingUser();
            var context = diKernel.ResolveService<DatabaseContext>();

            var request = new RateRequest
            {
                HistoryId = "ff98c9ba-fadd-42b7-bb88-c227e645d2e0",
                StarsAmount = 7
            };

            await Assert.ThrowsAsync<InvalidRatingValueException>(async () => await ratingService.RatePrediction(user, request));
        }


        [Fact]
        public async Task Given_RatingService_When_RatePredictionIsAlreadySubmitted_Then_RatingMayNotBeResubmitted()
        {
            diKernel.Dispose();
            diKernel = new DITests();

            var ratingService = diKernel.ResolveService<IRatingService>();
            var user = SetupRequestingUser();
            var context = diKernel.ResolveService<DatabaseContext>();

            var request = new RateRequest
            {
                HistoryId = "ff98c9ba-fadd-42b7-bb88-c227e645d2e0",
                StarsAmount = 3
            };

            await Assert.ThrowsAsync<RatingAlreadyExistsException>(async () =>
            {
                await ratingService.RatePrediction(user, request);
                await ratingService.RatePrediction(user, request);
            });
        }


        [Fact]
        public async Task Given_RatingService_When_GetAllRatingsIsCalled_Then_ShouldGetAllRatingsForPrediction()
        {
            diKernel.Dispose();
            diKernel = new DITests();

            var ratingService = diKernel.ResolveService<IRatingService>();
            var context = diKernel.ResolveService<DatabaseContext>();

            var result = await ratingService.GetAllRatingStats(1, 10);


            var actualResult = context.Ratings.Skip(0).Take(10).ToList();

            Assert.Equal(actualResult.Count(), result.PredictionsStats.Sum(k => k.RatingVotesCount));
        }


        [Fact]
        public async Task Given_RatingService_When_GetAllRatingsIsCalledWithInexistedPage_Then_ShouldReturnNothing()
        {
            var ratingService = diKernel.ResolveService<IRatingService>();

            var result = await ratingService.GetAllRatingStats(99999999, 100);

            Assert.Empty(result.PredictionsStats);
        }


        [Fact]
        public async Task Given_RatingService_When_GetRatingIsCalledForParticularPrediction_Then_ShouldReturnProperInfo()
        {
            var ratingService = diKernel.ResolveService<IRatingService>();

            var result = await ratingService.GetRatingStats(Guid.Parse("e398c9ba-fadd-42b7-bb88-c227e645d2e0"));

            Assert.Equal(4, result.AverageRating);
            Assert.Equal(2, result.RatingVotesCount);
        }
    }
}
