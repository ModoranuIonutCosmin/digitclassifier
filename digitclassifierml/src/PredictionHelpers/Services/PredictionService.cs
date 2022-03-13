using System.Collections.Generic;
using MLPredict.Models;
using MLPredict.Services;
using PredictionHelpers.Models;
using Trainer.Models;

namespace PredictionHelpers.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IDigitClassifier _classifierEngine;
        private readonly ImageToVectorConverter _imageConverter;

        public PredictionService(IDigitClassifier classifierEngine)
        {
            _classifierEngine = classifierEngine;
            _imageConverter = new ImageToVectorConverter();
        }
        public PredictionResponse PredictImage(PredictionRequest request)
        {
            List<float> imageBytes = _imageConverter.GetPixelValuesFromImage(request.Base64Image);
            var digitInfo = new DigitsInfoInput()
            {
                PixelAlphas = imageBytes.ToArray()
            };
            DigitClassifierPrediction prediction = _classifierEngine.Predict(digitInfo);
            var predictionResponse = new PredictionResponse()
            {
                DigitPredicted = prediction.MostLikely,
                PredictionLikelihood = prediction.Score[prediction.MostLikely]
            };

            return predictionResponse;
        }
    }
}
