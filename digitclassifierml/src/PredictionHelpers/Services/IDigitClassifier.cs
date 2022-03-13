using MLPredict.Models;
using Trainer.Models;

namespace PredictionHelpers.Services
{
    public interface IDigitClassifier
    {
        DigitClassifierPrediction Predict(DigitsInfoInput input);
    }
}