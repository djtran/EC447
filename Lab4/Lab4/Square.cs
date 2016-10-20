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

namespace Lab4
{
    public class Square
    {
        //initialize top left corner coordinate & white or black square
        public Point tlCorner;
        Brush dColor;
        Brush qColor;

        //if hint on, color if hit by queen
        public bool hint = false;
        //if queen on
        public bool queen = false;
        //if queen hits
        public bool queenHit = false;

        public Square(Point p, Brush b)
        {
            tlCorner = p;
            dColor = b;
        }

        public void reset()
        {
            queen = false;
            queenHit = false;
        }

        //Paint square based on current state
        public void paintSq(Graphics g)
        {
            if(queenHit)
            {
                if(hint)
                {
                    g.FillRectangle(Brushes.Red, tlCorner.X, tlCorner.Y, 50, 50);
                    qColor = Brushes.Black;
                }
                else
                {
                    g.FillRectangle(dColor, tlCorner.X, tlCorner.Y, 50, 50);
                    if (dColor == Brushes.Black)
                    {
                        qColor = Brushes.White;
                    }
                    else
                    {
                        qColor = Brushes.Black;
                    }
                }

                if(queen)
                {
                    g.DrawString("Q", new Font("Arial", 30, FontStyle.Bold), qColor, tlCorner.X, tlCorner.Y);
                }
            }
            else
            {
                g.FillRectangle(dColor, tlCorner.X, tlCorner.Y, 50, 50);
            }

            //Outline it
            g.DrawRectangle(Pens.Black, tlCorner.X, tlCorner.Y, 50, 50);

        }
    }
}