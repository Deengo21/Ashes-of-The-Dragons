using Ashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ashes.Characters
{
    public class Character : Displayable
    {
        public virtual List<(int x, int y)> MoveVectors { get; } = new List<(int x, int y)>();

        protected Thread _thread;

        public Character()
        {
            _thread = new Thread(Run);
            _thread.Start();
        }

        public override Image Image
        {
            get
            {
                if (_image != null)
                    return _image;
                _image = base.Image;
                _image.MouseDown += _image_MouseDown;
                return _image;
            }
        }

        private void _image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var movable = this as IMovable;
            if (movable == null)
                return;

            Random random = new Random();
            int randomVectorIdx = random.Next(this.MoveVectors.Count);

            movable.Move(MoveVectors[randomVectorIdx]);
        }

        protected virtual void Run()
        {
            var movable = this as IMovable;
            if (movable == null)
                return;
            if (this.MoveVectors.Count == 0)
                return;
            Random random = new Random();
            while (true)
            {
                Thread.Sleep(1100);
                int randomVectorIdx = random.Next(this.MoveVectors.Count);
                bool? _in = Application.Current?.Dispatcher?.Invoke(() =>
           this.X + MoveVectors[randomVectorIdx].Item1 < 0 || this.X + MoveVectors[randomVectorIdx].Item1 >= 16
           || this.Y + MoveVectors[randomVectorIdx].Item2 < 0 || this.Y + MoveVectors[randomVectorIdx].Item2 >= 16);
                if (_in == true)
                    continue;
                Application.Current?.Dispatcher?.Invoke(() => movable.Move(MoveVectors[randomVectorIdx]));
            }
        }
    }
}
