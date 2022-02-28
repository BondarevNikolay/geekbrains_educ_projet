using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace geekbrains_educ_projet
{
    public partial class startForm : Form
    {
        public Button startButton = new Button();
        public Button recordsButton = new Button();
        public Button optionButton = new Button();
        public Button exitButton = new Button();
        public startForm()
        {
            InitializeComponent();

            startButton.Text = "Start";
            startButton.Size = new Size(240, 30);
            startButton.Location = new Point(5, 5);
            this.Controls.Add(startButton);
            startButton.Click += new EventHandler(startButton_Click);

            recordsButton.Text = "Records";
            recordsButton.Size = new Size(240, 30);
            recordsButton.Location = new Point(5, 40);
            this.Controls.Add(recordsButton);
            recordsButton.Click += new EventHandler(recordsButton_Click);

            optionButton.Text = "Option";
            optionButton.Size = new Size(240, 30);
            optionButton.Location = new Point(5, 75);
            this.Controls.Add(optionButton);
            optionButton.Click += new EventHandler(optionButton_Click);

            exitButton.Text = "Exit";
            exitButton.Size = new Size(240, 30);
            exitButton.Location = new Point(5, 110);
            this.Controls.Add(exitButton);
            exitButton.Click += new EventHandler(exitButton_Click);
        }
        private void startButton_Click(object sender, System.EventArgs e)
        {
            Form1 form = new Form1
            {
                Width = 1200,
                Height = 600,
                MaximumSize = new Size(1200, 600),
                MinimumSize = new Size(1200, 600)
            };

            form.Show();
            Game.Init(form);
            Game.Draw();
        }
        private void recordsButton_Click(object sender, System.EventArgs e)
        { }
        private void optionButton_Click(object sender, System.EventArgs e)
        { }
        private void exitButton_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
