using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace DateGeo
{
    public class DateGeo: IPlugin
    {
        public string Name
        {
            get
            {
                return "Добавление текущей даты и данных геолокации";
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
            string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
            string strAdd = $"{currentDate}\nРоссия, Пермский край, г. Пермь, НИУ ВШЭ-Пермь";

            Font font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            SolidBrush brush = new SolidBrush(Color.Yellow); 
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Far
            };

            GraphicsPath path = new GraphicsPath();
            path.AddString(strAdd, font.FontFamily, (int)font.Style, 
                font.Size, new PointF(bitmap.Width - 10, bitmap.Height - 20), stringFormat);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                float penWidth = 4; 
                Pen pen = new Pen(Color.Black, penWidth)
                {
                    LineJoin = LineJoin.Round
                };
                graphics.DrawPath(pen, path);
                graphics.FillPath(brush, path);
            }

        }
    }
}
