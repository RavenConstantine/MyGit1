using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkrinGet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bmp;
        Graphics gr;
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            button1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //bmp = new Bitmap(this.Width, this.Height);
            bmp = new Bitmap((int)(this.Width*1.5), (int)(this.Height*1.5));
            gr = Graphics.FromImage(bmp);
            //gr.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, new Size(bmp.Width, bmp.Height));
            gr.CopyFromScreen((int)(this.Location.X * 1.5), (int)(this.Location.Y * 1.5), 0, 0, new Size(bmp.Width, bmp.Height));
            pictureBox1.Image = bmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
