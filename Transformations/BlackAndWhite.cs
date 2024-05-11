using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Drawing;

namespace Transformations
{
    [Version(1, 0)]
    public class BlackAndWhite : IPlugin
    {
        public string Name
        {
            get
            {
                return "Преобразование цветного изображения в оттенки серого";
            }
        }

        public string Author
        {
            get
            {
                return "Anastasiya Medvedeva";
            }
        }

        public void Transform(Bitmap bitmap)
        {
            // Перебираем каждый пиксель изображения
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    // Получаем цвет текущего пикселя
                    Color pixelColor = bitmap.GetPixel(x, y);

                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);

                    // Создаем новый цвет с оттенком серого
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    bitmap.SetPixel(x, y, grayColor);
                }
            }
        }

    }
}
