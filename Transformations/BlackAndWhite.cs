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
            
        }

    }
}
