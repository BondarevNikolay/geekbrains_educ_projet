using System;
using System.Windows.Forms;
using System.Drawing;

namespace geekbrains_educ_projet
{
    static class Game
    {
        static public Random rand = new Random();
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        static int Score = 0,  Frames = 0, xBackGround = 0;
        static public Image backgraund = Image.FromFile(@"Pictures/deep_space.jpg");
        static public Timer timer = new Timer();
        static BaseObject[] _obj;
        static public Bullet[] _bullets = new Bullet[4];
        static public Ship _ship;

        //static Star star;
        static Game()
        {
        }
        public static void Init(Form1 form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
           
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            timer.Start();  
        }
        public static void Load()
        {
            _ship = new Ship(new Point(0, Game.Height / 2), new Point(0, 0));
            //_bullets = new Bullet[4];
            for (int i = 0; i < _bullets.Length; i++)
            {
                _bullets[i] = new Bullet(new Point(0, Game.Height / 2), new Point(10 - rand.Next(1, 9), 0));
            }
                _obj = new BaseObject[20];
            for (int i = 0; i < _obj.Length/4*3; i++)
            {
                _obj[i] = new BaseObject(new Point(rand.Next(0, Game.Width), rand.Next(0, Game.Height)), new Point(rand.Next(-10, 10), rand.Next(-10, 10)), new Size(20, 20));
            }
            for (int i = _obj.Length / 4 * 3; i < _obj.Length; i++)
            {
                _obj[i] = new Star(new Point(Game.Width, rand.Next(0, Game.Height)), new Point(rand.Next(-10, 10), rand.Next(-10, 10)));
            }
            

            //star = new Star(new Point(100, 100), new Point(rand.Next(-10, 10), rand.Next(-10, 10)));
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            // Проверяем вывод графики
            Frames++;
            Buffer.Graphics.Clear(Color.Black);

            // Фон
            Buffer.Graphics.DrawImage(backgraund, xBackGround, 0);
            Buffer.Graphics.DrawImage(backgraund, xBackGround+1200, 0);
            
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200)); // Рисуем прямоугольник
            //Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200)); // Рисуем круг
            Buffer.Graphics.DrawString($"Frames:{Frames}", SystemFonts.DefaultFont, Brushes.AntiqueWhite, 1000, 0);
            Buffer.Graphics.DrawString($"Score:{Score}", SystemFonts.DefaultFont, Brushes.AntiqueWhite, 900, 0);
            _ship.Draw();
            foreach (BaseObject obj in _obj)
            {
                obj.Draw();
            }
            foreach (Bullet bullet in _bullets)
            {
                bullet.Draw();
            }
            //star.Draw();
            Buffer.Render();
        }   

        public static void Update()
        {
            xBackGround-=10;
            if (xBackGround < -1200)
            {
                xBackGround = 0;
            }
            for (int j = 0; j < _obj.Length; j++)
            {
                for (int i = 0; i < _bullets.Length; i++)
                {
                    if (_bullets[i].Collision(_obj[j]))
                    {
                        if (_obj[j] is Star)
                        {
                            Score++;
                            _bullets[i] = new Bullet(new Point(0, Game.Height / 2), new Point(10 - rand.Next(1, 9), rand.Next(-3, 3)));
                            _obj[j] = new Star(new Point(Game.Width, rand.Next(0, Game.Height)), new Point(rand.Next(-10, 10), rand.Next(-10, 10)));
                        }
                    }
                }
                _obj[j].Update();
                
                //star.Update();
            }
            foreach (Bullet bullet in _bullets)
            {
                bullet.Update();
            }
            _ship.Update();
        }

    }
}
