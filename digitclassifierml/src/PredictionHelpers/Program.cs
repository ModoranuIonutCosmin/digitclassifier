using MLPredict.Services;
using MLPredict.Utils;
using PredictionHelpers.Services;
using System;
using Trainer.Models;

namespace PredictionHelpers
{
    public static class Program
    { 
        public static void Main()
        {
            var imageWithNumberPath = "image.bmp";
            var imageToVector = new ImageToVectorConverter();

            var imageAsBase64 = Base64Helpers.ConvertImageToBase64(imageWithNumberPath);
            //Console.WriteLine("BASE64: " + imageAsBase64);
            var imageBytes = imageToVector.GetPixelValuesFromImage(imageAsBase64);

            IDigitClassifier digitClassifier = new DigitClassifier();
            var digitInfo = new DigitsInfoInput()
            {
                PixelAlphas = imageBytes.ToArray()
            };

            var prediction = digitClassifier.Predict(digitInfo);

            Console.WriteLine(prediction);

            Console.WriteLine(prediction.MostLikely);
        }
    }

}

