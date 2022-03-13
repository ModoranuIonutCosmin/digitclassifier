using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PredictionHelpers.Models;
using PredictionHelpers.Services;

namespace Api.Controllers
{
    [Route("api/ml")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionService _predictionService;

        public PredictionController(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpPost]
        public IActionResult Predict([FromBody]PredictionRequest request)
        {
            PredictionResponse response = _predictionService.PredictImage(request);
            return Ok(response);
        }
    }
}
