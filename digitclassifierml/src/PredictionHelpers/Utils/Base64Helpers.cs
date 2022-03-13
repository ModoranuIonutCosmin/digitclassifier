using System;
using System.Drawing;
using System.IO;

namespace MLPredict.Utils
{
    public static class Base64Helpers
    {

        public static string ConvertImageToBase64(string path)
        {
            using Image image = Image.FromFile(path);
            using MemoryStream memoryStream = new();

            image.Save(memoryStream, image.RawFormat);
            byte[] imageBytes = memoryStream.ToArray();

            // Convert byte[] to Base64 String
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
