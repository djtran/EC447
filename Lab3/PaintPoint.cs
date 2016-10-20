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
    public class PaintPoint
    {

        public Point p;
        public int state;
        public int radius;
        public Brush b;

        public PaintPoint(int x, int y, int radius)
        {
            p = new Point(x, y);
            state = 0;
            this.radius = radius;
            b = Brushes.Black;
        }

        public bool CheckCollision(int x, int y)
        {
            //if within x bounds
            if (x <= (p.X + radius) && x >= (p.X - radius))
            {
                if (y <= (p.Y + radius) && y >= (p.Y - radius))
                {
                    state++;
                    if(b == Brushes.Black)
                    {
                        b = Brushes.Red;
                    }
                    return true;
                }
            }
            return false;
        }

        public void Paint(Graphics g)
        {
            g.FillEllipse(b, p.X - radius, p.Y - radius, radius * 2, radius * 2);
        }
    }
}