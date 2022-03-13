using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MLPredict.Services
{
    public class ImageToVectorConverter
    {
        public const int TargetWidth = 32;
        public const int TargetHeight = 32;
        public const int BlockSquareSize = 4;

        public List<float> GetPixelValuesFromImage(string base64Image)
        {
            var imageBytes = Convert.FromBase64String(base64Image).ToArray();

            // resize the original image and save it as bitmap
            var bitmap = new Bitmap(TargetWidth, TargetHeight);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                using var stream = new MemoryStream(imageBytes);
                var png = Image.FromStream(stream);
                g.DrawImage(png, 0, 0, TargetWidth, TargetHeight);
            }

            // aggregate pixels in 4X4 area --> 'result' is a list of 64 integers
            var result = new List<float>();
            for (var i = 0; i < TargetWidth; i += BlockSquareSize)
            {
                for (var j = 0; j < TargetHeight; j += BlockSquareSize)
                {
                    var sum = 0;
                    for (var k = i; k < i + BlockSquareSize; k++)
                    {
                        for (var l = j; l < j + BlockSquareSize; l++)
                        {
                            if (bitmap.GetPixel(l, k).Name != "ffffffff") sum++;
                        }
                    }
                    result.Add(sum);
                }
            }

            return result;
        }
    }
}
