using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geekbrains_educ_projet
{
    public partial class Form1 : Form
    {
        public Button ppButton = new Button();
        public Button recordsButton = new Button();
        public Button optionButton = new Button();
        public Button exitButton = new Button();

        bool paused = false;
        public Form1()
        {
            InitializeComponent();

            ppButton.Text = "Pause";
            ppButton.Size = new Size(75, 25);
            ppButton.Location = new Point(0, 0);
            this.Controls.Add(ppButton);
            ppButton.Click += new EventHandler(startButton_Click);
            
            exitButton.Text = "Exit";
            exitButton.Size = new Size(75, 25);
            exitButton.Location = new Point(1100, 0);
            this.Controls.Add(exitButton);
            exitButton.Click += new EventHandler(exitButton_Click);
        }
        private void startButton_Click(object sender, System.EventArgs e)
        {
            if (paused == false)
            {
                Game.timer.Stop();
                paused = true;
                ppButton.Text = "Play";
            }
            else
            {
                Game.timer.Start();
                paused = false;
                ppButton.Text = "Pause";
            }
        }
        private void recordsButton_Click(object sender, System.EventArgs e)
        { }
        private void optionButton_Click(object sender, System.EventArgs e)
        { }
        private void exitButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Game.timer.Stop();
        }
    }
}
