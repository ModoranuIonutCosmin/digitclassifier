using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System;
using Trainer.Models;

namespace Trainer
{
    internal class Program
    {
        static void Main()
        {
            var pathData = "optics.csv";

            var context = new MLContext();
            var dataView = context.Data.LoadFromTextFile(
                path: pathData,
                columns: new[]
                {
              new TextLoader.Column(nameof(DigitsInfoInput.TargetValue), DataKind.Single, 64),
              new TextLoader.Column(nameof(DigitsInfoInput.PixelAlphas), DataKind.Single, 0, 63),
                },
                hasHeader: false,
                separatorChar: ',');

            // split data into a training and test set
            var partitions = context.Data.TrainTestSplit(dataView, 0.0001);

            var pipeline = context.Transforms.Conversion
                .MapValueToKey("Label", nameof(DigitsInfoInput.TargetValue), keyOrdinality: ValueToKeyMappingEstimator.KeyOrdinality.ByValue)
                .Append(
                context.Transforms.Concatenate(
                "Features",
                nameof(DigitsInfoInput.PixelAlphas)))
                .Append(context.Transforms.Conversion.MapKeyToValue(nameof(DigitsInfoInput.TargetValue), "Label"))
                // step 2: cache data to speed up training                
                .AppendCacheCheckpoint(context)

                // step 3: train the model with SDCA
                .Append(context.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                    labelColumnName: "Label",
                    featureColumnName: "Features"));

            // train the model
            Console.WriteLine("Training model....");
            var model = pipeline.Fit(partitions.TrainSet);

            // Save Trained Model
            context.Model.Save(model, dataView.Schema, "../../../Model/model.zip");

            // use the model to make predictions on the test data
            Console.WriteLine("Evaluating model....");
            var predictions = model.Transform(partitions.TestSet);

            // evaluate the predictions
            var metrics = context.MulticlassClassification.Evaluate(
                data: predictions,
                labelColumnName: nameof(DigitsInfoInput.TargetValue),
                scoreColumnName: "Score");

            // show evaluation metrics
            Console.WriteLine($"Evaluation metrics");
            Console.WriteLine($"    MicroAccuracy:    {metrics.MacroAccuracy:0.###}");
            Console.WriteLine($"    MacroAccuracy:    {metrics.MicroAccuracy:0.###}");
            Console.WriteLine($"    LogLoss:          {metrics.LogLoss:#.###}");
            Console.WriteLine($"    LogLossReduction: {metrics.LogLossReduction:#.###}");
            Console.WriteLine();
        }
    }
}
