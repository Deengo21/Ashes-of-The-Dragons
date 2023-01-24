using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace Ashes
{
    public static class Extensions
    {
        public static void Move(this IMovable movable, (int x, int y) v)
        {
            var displ = movable as Displayable;
            if (displ == null)
                return;

            if (displ.X + v.x >= MainWindow.BoardSize || displ.X + v.x < 0 ||
                displ.Y + v.y >= MainWindow.BoardSize || displ.Y + v.y < 0)
                return;

            var transform = new TranslateTransform();
            var xAnimation = new DoubleAnimation(MainWindow.BoardColumnSize * v.x, TimeSpan.FromSeconds(1));
            var yAnimation = new DoubleAnimation(MainWindow.BoardRowSize * v.y, TimeSpan.FromSeconds(1));

            displ.Image.RenderTransform = transform;

            xAnimation.Completed += (s, e) =>
            {
                displ.Image.RenderTransform = null;
                displ.X += v.x;
                displ.Y += v.y;
            };

            transform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, yAnimation);
        }
    }
}
