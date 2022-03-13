using PredictionHelpers.Models;

namespace PredictionHelpers.Services
{
    public interface IPredictionService
    {
        public PredictionResponse PredictImage(PredictionRequest request);
    }
}
