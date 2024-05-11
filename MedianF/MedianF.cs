using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace MedianF
{
    [Version(1, 0)]
    public class MedianF: IPlugin
    {
        public string Name
        {
            get
            {
                return "Матричный медианный фильтр";
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
            int[] matrix = new int[25];

            for (int x = 2; x < bitmap.Width - 2; x++)
            {
                for (int y = 2; y < bitmap.Height - 2; y++)
                {
                    int index = 0;
                    for (int i = -2; i <= 2; i++)
                    {
                        for (int j = -2; j <= 2; j++)
                        {
                            Color pixelColor = bitmap.GetPixel(x + i, y + j);
                            int colorValue = pixelColor.ToArgb();
                            matrix[index++] = colorValue;
                        }
                    }

                    Array.Sort(matrix, 0, matrix.Length);
                    int medianValue = matrix[matrix.Length / 2];
                    bitmap.SetPixel(x, y, Color.FromArgb(medianValue));
                }
            }
        }

    }
}
