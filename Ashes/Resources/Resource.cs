using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ashes.Resources
{
    public class Resource : Displayable
    {
       /* string[] availableImages = { 
            "Images/Resource/Gold.png", 
            "Images/Resource/grain.png", 
            "Images/Resource/LifeFountain.png",
            "Images/Resource/oil-barrel.png",
            "Images/Resource/Steel.png"
        };

        public void SetRandomImage()
        {
            Random rnd = new Random();
            int index = rnd.Next(availableImages.Length);
            Image.Source = new BitmapImage(new Uri(availableImages[index], UriKind.Relative));
        }
       */
        public override Image Image
        {
            get
            {
                if (_image == null)
                {
                    var image = base.Image;
                    image.Width = 40;
                    var translate = new TranslateTransform(0, 0);

                    image.RenderTransform = translate;
                    //SetRandomImage();
                }
                return _image;
            }
        }
        



    }

        }

    
