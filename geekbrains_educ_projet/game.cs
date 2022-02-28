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
        
        static int Frames = 0, xBackGround = 0;
        static public Image backgraund = Image.FromFile(@"Pictures/deep_space.jpg");
        static public Timer timer = new Timer();
        static BaseObject[] _obj;
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
            _obj = new BaseObject[20];
            for (int i = 0; i < _obj.Length/4*3; i++)
            {
                _obj[i] = new BaseObject(new Point(rand.Next(0, Game.Width), rand.Next(0, Game.Height)), new Point(rand.Next(-10, 10), rand.Next(-10, 10)), new Size(20, 20));
            }
            for (int i = _obj.Length / 4 * 3; i < _obj.Length; i++)
            {
                _obj[i] = new Star(new Point(0, rand.Next(0, Game.Height)), new Point(rand.Next(-10, 10), rand.Next(-10, 10)));
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
            foreach (BaseObject obj in _obj)
            {
                obj.Draw();
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
            foreach (BaseObject obj in _obj)
            {
                obj.Update();
                //star.Update();
            }
        }

    }
}
