using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab2
{
    public partial class Form1 : Form
    {
        private ArrayList coord = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //Add point, paint
            if (e.Button == MouseButtons.Left)
            {
                Point p = new Point(e.X, e.Y);
                this.coord.Add(p);
                this.Invalidate();
            }
            //Clear points, paint
            if (e.Button == MouseButtons.Right)
            {
                this.coord.Clear();
                this.Invalidate();
            }

        }

        //The master painter himself
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int radius = 10;

            Graphics g = e.Graphics;
            //Paint all the points and write coordinates
            foreach(Point p in coord)
            {
                g.FillEllipse(Brushes.Black, p.X - radius, p.Y - radius, radius * 2, radius * 2);

                //Push the top left corner of the text over by radius amount.
                Point text = new Point(p.X + radius, p.Y - radius/2);
                g.DrawString("(" + p.X + ", " + p.Y + ")", Font, Brushes.Black, text);
            }
        }

        //If you can't right click, don't worry we have a button for that.
        private void button1_Click(object sender, EventArgs e)
        {
            coord.Clear();
            Invalidate();
        }

        //If you don't like buttons, no problem we have a menu item for that.
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coord.Clear();
            Invalidate();
        }
    }
}
