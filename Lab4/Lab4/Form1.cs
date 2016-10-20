using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        Square[,] grid;
        int queens = 0;

        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(650, 650);
            grid = new Square[8,8];

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if((i + j) % 2 == 0)
                    {
                        grid[i, j] = new Square(new Point(100 + 50 * i, 100 + 50 * j), Brushes.White);
                    }
                    else
                    {
                        grid[i, j] = new Square(new Point(100 + 50 * i, 100 + 50 * j), Brushes.Black);
                    }
                }
            }

        }

        //Paint each square, update queen-affected squares on board.
        //Original intention was to display "You did it!" if the paint function counted 8 queens, 
        //but the exe provided shows the message right as you click, and then places the queen and paints the board.
        //For this reason, I decided to have a counter that would updated on each click event or on reset.
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("You have " + queens + " queens on the board.", Font, Brushes.Black, new Point(200, 25));

            foreach(Square q in grid)
            {
                //Always update and redraw, if a queen is removed we will get blank spaces otherwise.
                if (q.queen)
                {
                    updateQueens(reduce(q.tlCorner.X, q.tlCorner.Y), true);
                }
            }
            //Draw the grid - Squares
            foreach(Square sq in grid)
            {
                sq.paintSq(e.Graphics);
                
            }
        }

        //Left click - Place a queen if safe, Right click - Remove a queen if one is on the board. Update queens counter
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point click = reduce(e.X, e.Y);
            if (click.X >= 0 && click.X < 8 && click.Y < 8 && click.Y >= 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (grid[click.X, click.Y].queenHit == false)
                    {
                        //Let the paint function ensure all the queens that are hit are hit.
                        grid[click.X, click.Y].queen = true;
                        queens++;
                        if (queens == 8)
                        {
                            MessageBox.Show("You did it!");
                        }
                        Invalidate();
                    }
                    else
                    {
                        //Beep! this is not a safe cell!
                        System.Media.SystemSounds.Beep.Play();
                    }
                }
                else if(e.Button == MouseButtons.Right)
                {
                    if (grid[click.X, click.Y].queen)
                    {
                        //Remove this queen and uncolor all affected red spaces. If there are overlapping hits, the paint function will repaint them.
                        updateQueens(click,false);
                        queens--;
                        Invalidate();
                    }
                }
            }
        }

        //Reduce a mouseClick coordinate to a square in the grid.
        private Point reduce(int X, int Y)
        {
            return new Point((X-100)/50, (Y-100)/50);
        }

        //Updates all affected squares if a queen is placed/lifted
        private void updateQueens(Point click, bool left)
        {
            //Place queen on that space
            grid[click.X, click.Y].queen = left;

            //Hit all of the horizontal & vertical spaces.
            for (int i = 0; i < 8; i++)
            {
                grid[i, click.Y].queenHit = left;
                grid[click.X, i].queenHit = left;
            }

            //I GOT THE QUEEN DIAGONALS WORKING!
            //Starting from the (0, upper++) and (0,lower--)
            int upper = Math.Abs(click.X - click.Y);
            int lower = Math.Abs(click.X + click.Y);

            for(int i = 0; i < 8; i++)
            {
                if (upper >= 0 && upper < 8)
                {
                    //lower half of the grid
                    if (click.X <= click.Y)
                    {
                        grid[i, upper].queenHit = left;
                    }//mirror over y=x
                    else
                    {
                        grid[upper, i].queenHit = left;
                    }
                }
                if (lower >= 0 && lower < 8)
                {//lower half of the grid
                    if (click.X <= click.Y)
                    {
                        grid[i, lower].queenHit = left;
                    }//mirror over y=x
                    else
                    {
                        grid[lower, i].queenHit = left;
                    }
                }
                upper++;
                lower--;
            }
            
        }

        //Hints checkbox immediately updates the view
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                foreach(Square sq in grid)
                {
                    sq.hint = true;
                }
            }
            else
            {
                foreach (Square sq in grid)
                {
                    sq.hint = false;
                }
            }
            Invalidate();
        }

        //Reset button - Resets all squares, reinitializes queens
        private void button1_Click(object sender, EventArgs e)
        {
            foreach(Square sq in grid)
            {
                sq.reset();
                queens = 0;
            }
            Invalidate();
        }
    }
}
