using MLPredict.Services;
using MLPredict.Utils;
using System;
using System.IO;

namespace Labeler
{
    public static class Program
    {
        static void Main()
        {
            var lastImageString = "";
            var imagePath = "edited.bmp";
            var imgConverter = new ImageToVectorConverter();

            while (true)
            {
                Console.WriteLine("Label next!");

                var label = Console.ReadLine();

                var result = int.TryParse(label, out var number);

                if (!result || number < 0 || number > 9)
                    continue;

                var imageBase64 = Base64Helpers.ConvertImageToBase64(imagePath);

                if (lastImageString == imageBase64)
                {
                    Console.WriteLine("N-ai salvat!");
                    continue;
                }

                var bytes = imgConverter.GetPixelValuesFromImage(imageBase64);

                string entry = string.Join(",", bytes) + "," + number + "\n";

                File.AppendAllText("out.txt", entry);

                lastImageString = imageBase64;
            }
        }
    }
}
