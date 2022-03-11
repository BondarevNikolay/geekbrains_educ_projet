using System;
using System.Windows.Forms;
using System.Collections.Generic;
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
        //public static System.Drawing.Point CursorPosition { get; }
        static uint starNum = Properties.Settings.Default.starNum, 
                    sAsterNum = Properties.Settings.Default.sAsterNum, 
                    bAsterNum = Properties.Settings.Default.bAsterNum;

        static int Score = 0,
                   Frames = 0,
                   xBackGround = 0;

        static public Image backgraund = Image.FromFile(@"Pictures/deep_space.jpg");
        static public Timer timer = new Timer();
        static BaseObject[] _obj;
        //static public Bullet[] _bullets = new Bullet[4];
        static public List<Bullet> _bullets2 = new List<Bullet>();
        static public List<Star> _stars = new List<Star>();
        static public List<SmallAsteroid> _sAster = new List<SmallAsteroid>();
        static public List<BigAsteroid> _bAster = new List<BigAsteroid>();
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
            form.MouseMove += Form_MouseMove;
            form.MouseClick += Form_MouseClick;
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            timer.Start();  
        }

        private static void Form_MouseClick(object sender, MouseEventArgs e)
        {
            _bullets2.Add(new Bullet(new Point(Game._ship.Rect.Location.X, Game._ship.Rect.Location.Y), new Point(10 - rand.Next(1, 9), 0)));
        }
        private static void Form_MouseMove(object sender, MouseEventArgs e)
        {
            //_ship.ShipUpdate(Control.MousePosition.X - Form1.ActiveForm.Location.X, Control.MousePosition.Y - Form1.ActiveForm.Location.Y);
            _ship.ShipUpdate(Form1.MousePosition.X - Form1.ActiveForm.Location.X - 8, Form1.MousePosition.Y - Form1.ActiveForm.Location.Y - 30);
            //_ship.ShipUpdate(Cursor.Position.X - Form1.MousePosition.X /*form.Location.X*/, Cursor.Position.Y - Form1.MousePosition.Y);
            /*if(Form1.MousePosition.X > Form1.ActiveForm.Location.X + 8 && Form1.MousePosition.Y > Form1.ActiveForm.Location.Y + 30 && Form1.MousePosition.X < Form1.ActiveForm.Location.X + Game.Width - 8 && Form1.MousePosition.Y < Form1.ActiveForm.Location.Y + Game.Height - 8)
            {    
                Cursor.Hide();
            }
            else Cursor.Show();*/
        }

        public static void Load()
        {
            // Последовательно загружаем корабль, пульку(снежок), звёздочки, малелькте и большие астероиды 
            _ship = new Ship(new Point(0, Game.Height / 2), new Point(0, 0));
            _bullets2.Add(new Bullet(new Point(Game._ship.Rect.Location.X, Game._ship.Rect.Location.Y), new Point(10 - rand.Next(1, 9), rand.Next(-3, 3))));
            for (uint i = 0; i < starNum; i++)
            {
                _stars.Add(new Star(new Point(Game.Width, rand.Next(0, Game.Height)), new Point(rand.Next(-10, 10), rand.Next(-10, 10))));
            }
            for (uint i = 0; i < sAsterNum; i++)
            {
                _sAster.Add(new SmallAsteroid(new Point(rand.Next(100, Game.Width), rand.Next(0, Game.Height)), new Point(rand.Next(-10, 10), rand.Next(-10, 10))));
            }
            for (uint i = 0; i < bAsterNum; i++)
            {
                _bAster.Add(new BigAsteroid(new Point(Game.Width, rand.Next(0, Game.Height)), new Point(rand.Next(-3, 3), rand.Next(-3, 3))));
            }

            /*_obj = new BaseObject[20];
            for (int i = 0; i < _obj.Length/4*3; i++)
            {
                _obj[i] = new BaseObject(new Point(rand.Next(0, Game.Width), rand.Next(0, Game.Height)), new Point(rand.Next(-10, 10), rand.Next(-10, 10)), new Size(20, 20));
            }
            for (int i = _obj.Length / 4 * 3; i < _obj.Length; i++)
            {
                _obj[i] = new Star(new Point(Game.Width, rand.Next(0, Game.Height)), new Point(rand.Next(-10, 10), rand.Next(-10, 10)));
            }*/
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
            /*foreach (BaseObject obj in _obj)
            {
                obj.Draw();
            }*/
            foreach (Bullet bullet in _bullets2)
            {
                bullet.Draw();
            }
            foreach (Star star in _stars)
            {
                star.Draw();
            }
            foreach (SmallAsteroid sAster in _sAster)
            {
                sAster.Draw();
            }
            foreach (BigAsteroid bAster in _bAster)
            {
                bAster.Draw();
            }
            Buffer.Render();
        }   

        public static void Update()
        {            
            xBackGround-=10;
            if (xBackGround < -1200)
            {
                xBackGround = 0;
            }
            // Собираем звёзды - получаем очки 
            for (int j = 0; j < _stars.Count; j++)
            {
                if (_ship.Collision(_stars[j]))
                { 
                    Score++;
                    _stars[j] = new Star(new Point(Game.Width, rand.Next(0, Game.Height)), new Point(rand.Next(-4, 4), rand.Next(-4, 4)));
                }
            }
            // Сталкиваемся с астероидами - быстро теряем очки
            for (int j = 0; j < _bAster.Count; j++)
            {
                for (int i = 0; i < _bullets2.Count; i++)
                {
                    if (_bullets2[i].Collision(_bAster[j]))
                    {
                        _bAster[j] = new BigAsteroid(new Point(Game.Width, rand.Next(0, Game.Height)), new Point(rand.Next(-3, 3), rand.Next(-3, 3)));
                    }
                }
                if (_ship.Collision(_bAster[j]))
                {
                    Score = Score - 10;
                    _bAster[j] = new BigAsteroid(new Point(Game.Width, rand.Next(0, Game.Height)), new Point(rand.Next(-3, 3), rand.Next(-3, 3)));
                }
            }
            for (int i = 0; i < _stars.Count; i++)
            { _stars[i].Update(); }
            for (int i = 0; i < _sAster.Count; i++)
            { _sAster[i].Update(); }
            for (int i = 0; i < _bAster.Count; i++)
            { _bAster[i].Update(); }
            for (int i = 0; i < _bullets2.Count; i++)
            { _bullets2[i].Update(); }
        }

    }
}
