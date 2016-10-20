using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics name = e.Graphics;

            //name.FillRectangle(Brushes.Azure, 0, 0, 200, 200);

            name.DrawString("David Tran", Font, Brushes.Black, 100, 100);
        }
    }
}
