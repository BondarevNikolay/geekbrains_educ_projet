using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace geekbrains_educ_projet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Form1 form = new Form1();
            form.Width = 1200;
            form.Height = 600;
            form.MaximumSize = new Size(1200, 600);
            form.MinimumSize = new Size(1200, 600);
                      
            
            form.Show();
                        
            Game.Init(form);
            
            Game.Draw();

            Application.Run(form);         

            //Console.ReadKey();
        }
    }
}
