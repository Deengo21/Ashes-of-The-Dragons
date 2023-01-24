using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Ashes
{
    public class Displayable
    {
        protected Image _image = null;

        public virtual string ImagePath => $"Images/{GetType().BaseType.Name}/{GetType().Name}.png";

        public Grid Grid { get; set; }

        public Displayable()
        {
            Grid = MainWindow.DynamicContent;
            
        }

        public virtual int X
        {
            get { return Grid.GetColumn(Image); }
            set { Grid.SetColumn(Image, value); }
        }

        public virtual int Y
        {
            get { return Grid.GetRow(Image); }
            set { Grid.SetRow(Image, value); }
        }
        
        public virtual Image Image
        {
            get
            {
                if (_image == null)
                {
                    _image = new Image();
                    _image.Source = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
                    Grid.Children.Add(_image);
                }
                return _image;
            }
        }
        

    }
}
