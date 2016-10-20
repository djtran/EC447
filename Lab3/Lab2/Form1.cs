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

namespace Lab3
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
                PaintPoint p = new PaintPoint(e.X, e.Y, 10);
                this.coord.Add(p);
                this.Invalidate();
            }
            //Modify point to paint, maybe clear
            if (e.Button == MouseButtons.Right)
            {
                bool check = false;
                for(int i = coord.Count-1; i >= 0; i--)
                {
                    PaintPoint p = (PaintPoint) coord[i];
                    if (p.CheckCollision(e.X, e.Y))
                    {
                        check = true;
                        if(p.state == 2)
                        {
                            coord.RemoveAt(i);
                        }
                    }
                }
                if (!check)
                {
                    this.coord.Clear();
                }
                this.Invalidate();
            }

        }

        //The master painter himself
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            //Paint all the points and write coordinates
            foreach(PaintPoint p in coord)
            {

                p.Paint(g);
                //g.FillEllipse(Brushes.Black, p.X - radius, p.Y - radius, radius * 2, radius * 2);

                //Push the top left corner of the text over by radius amount.
                //Point text = new Point(p.X + radius, p.Y - radius/2);
                //g.DrawString("(" + p.X + ", " + p.Y + ")", Font, Brushes.Black, text);
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
