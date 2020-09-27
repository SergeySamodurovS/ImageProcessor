using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            if (args.Length == 0)
            {
                Console.WriteLine("Enter the path to file");
                path = $@"{Console.ReadLine()}";
            }
            else
            {
                path = $@"{args[0]}";
            }
            if(!File.Exists(path))
            {
                Console.WriteLine("Файл по указанному пути не существует");
                return;
            }
            Bitmap image;
            using (var file = new FileStream(path, FileMode.Open))
            {
                image = new Bitmap(file);
            }
            ConvertToGrayscale(image);
            
            string name = $@"{path.Substring(0, path.ToCharArray().Length - 4)}-result.{path.Substring(path.ToCharArray().Length - 3, 3)}";
            try
            {
                image.Save(name);
                Console.WriteLine($"Изображение преобразовано в чёрно-белое, результат записан в файл {name}");
            }
            catch (ExternalException)
            {
                Console.WriteLine("Недостаточно прав для записи файла, пожалуйста, выполните команду от имени администратора");
            }
            Console.ReadKey();
        }

        private static void ConvertToGrayscale(Bitmap bitmap)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    int grey = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    bitmap.SetPixel(x, y, Color.FromArgb(pixelColor.A, grey, grey, grey));
                }
            }
        }
    }
}
