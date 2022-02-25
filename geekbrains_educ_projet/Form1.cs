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
        public Button startButton = new Button();
        public Button recordsButton = new Button();
        public Button optionButton = new Button();
        public Button exitButton = new Button();
        public Form1()
        {
            InitializeComponent();

            startButton.Text = "Start";
            startButton.Size = new Size(75, 25);
            startButton.Location = new Point(0, 0);
            this.Controls.Add(startButton);
            startButton.Click += new EventHandler(startButton_Click);

            recordsButton.Text = "Records";
            recordsButton.Size = new Size(75, 25);
            recordsButton.Location = new Point(80, 0);
            this.Controls.Add(recordsButton);
            recordsButton.Click += new EventHandler(recordsButton_Click);

            optionButton.Text = "Option";
            optionButton.Size = new Size(75, 25);
            optionButton.Location = new Point(160, 0);
            this.Controls.Add(optionButton);
            optionButton.Click += new EventHandler(optionButton_Click);

            exitButton.Text = "Exit";
            exitButton.Size = new Size(75, 25);
            exitButton.Location = new Point(1100, 0);
            this.Controls.Add(exitButton);
            exitButton.Click += new EventHandler(exitButton_Click);
        }
        private void startButton_Click(object sender, System.EventArgs e)
        {
            
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
