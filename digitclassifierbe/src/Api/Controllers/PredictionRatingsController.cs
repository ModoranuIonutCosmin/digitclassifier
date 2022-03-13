using Application.Models.Ratings;
using Application.Services;
using DataAcces.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class PredictionRatingsController : ControllerBase
    {
        private readonly IRatingService ratingService;

        public PredictionRatingsController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitPredictionRating([FromBody] RateRequest rateRequest)
        {
            User user = HttpContext.Items["User"] as User;

            return Created("rating", await ratingService.RatePrediction(user, rateRequest));
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllRatings([FromQuery] RatingsPaginatedRequest pagination)
        {
            //Vor fi mai sus elementele cu rating mai mare.
            var result = await ratingService.GetAllRatingStats(pagination.Page, pagination.Count);

            return Ok(result);
        }

        [HttpGet("myRating/{predictionId}")]
        public async Task<IActionResult> GetUserRating(Guid predictionId)
        {
            User user = HttpContext.Items["User"] as User;

            return Ok(await ratingService.GetUserRatingForPrediction(user, predictionId));
        }

        [HttpGet("{predictionId}")]
        public async Task<IActionResult> GetRating(Guid predictionId)
        {
            return Ok(await ratingService.GetRatingStats(predictionId));
        }
    }
}
