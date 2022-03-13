using System;
using Microsoft.ML;
using MLPredict.Models;
using Trainer.Models;

namespace PredictionHelpers.Services
{
    public class DigitClassifier : IDigitClassifier
    {
        private readonly MLContext Context;
        private readonly PredictionEngine<DigitsInfoInput, DigitClassifierPrediction> Engine;
        private  string _MODEL_PATH = @"../../src/Trainer/Model/model.zip";
        public DigitClassifier()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Production")
            {
                _MODEL_PATH = @"/src/Trainer/Model/model.zip";
            }
            Context = new MLContext();

            // Load trained model
            ITransformer trainedModel = Context.Model.Load(_MODEL_PATH, out _);

            Engine = Context.Model.CreatePredictionEngine<DigitsInfoInput, DigitClassifierPrediction>(trainedModel);
        }

        public DigitClassifierPrediction Predict(DigitsInfoInput input)
        {
            return Engine.Predict(input);
        }
    }
}
