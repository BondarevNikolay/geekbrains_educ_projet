using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace geekbrains_educ_projet
{
    internal class BaseObject
    {
        protected Point Pos { get; set; }
        protected Point Dir { get; set; } //VX, VY
        protected Size Size { get; set; }

        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public void Update()
        {
            Pos = new Point(Pos.X+Dir.X, Pos.Y+Dir.Y);
            if (Pos.X > Game.Width) // Такого быть не должно никогда, но ...
            {
                Pos = new Point(Game.Width, Pos.Y);
                Dir = new Point(-Game.rand.Next(1, 10), Game.rand.Next(-10, 10)); 
            }
            else if (Pos.X < -50) // Перекидывает звёздочки и кружочки из конца в начачло и переопределяет вектор движения
            { 
                Pos = new Point(Game.Width, Game.rand.Next(0, Game.Height));
                Dir = new Point(-Game.rand.Next(1, 10), Game.rand.Next(-10, 10));
            }
            if (Pos.Y < -50) // Перекидывает звёздочки и кружочки сверху вниз при выходе за границу окна и переопределяет вектор движения. Значение -50 взято для плавности
            {
                Pos = new Point(Pos.X, Pos.Y + Game.Height);
                Dir = new Point(-Game.rand.Next(1, 10), Game.rand.Next(-10, 0));

            }
            else if (Pos.Y > Game.Height) // Перекидывает звёздочки и кружочки внизу вверх при выходе за границу окна и переопределяет вектор движения
            {
                Pos = new Point(Pos.X, Pos.Y - Game.Height);
                Dir = new Point(-Game.rand.Next(1, 10), Game.rand.Next(0, 10));
            }
        }

    }

    internal class Star : BaseObject
    {
        static Image img = Image.FromFile(@"Pictures/Star.png");

        public Star(Point pos, Point dir) : base(pos, dir, new Size(img.Width, img.Height))
        { 
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos);
        }
    }
}
