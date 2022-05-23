
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI
{
    public partial class Form1 : Form
    {
        Graphics g;
        int xs, ys;
        Pen p = new Pen(Color.Blue, 5);
        DB dB = new DB();
        Stack<string> Result = new Stack<string>();
        public Form1()
        {
            InitializeComponent();
            /*Rectangle rec = new Rectangle(0, 0, 30, 60);
            Pen p = new Pen(Color.Blue, 3);
            rec.*/
            g = this.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Point a = new Point(0, 0);
            Point b = new Point(500, 500);
            Pen p = new Pen(Color.Blue, 10/10);
            g = this.CreateGraphics();
            g.DrawLine(p, a, b);*/
            MessageBox.Show("X " + xs.ToString() + " - Y " + ys.ToString());
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("x = " + x.ToString() +", y = " + y.ToString() );
            //g.DrawLine(p, x, y, x+ad, y+ad);
            //DrawPoint(xs, ys);
            //MessageBox.Show("X " + xs.ToString() + " - Y" + ys.ToString());
            for(int i = 0; i< dB.LOCATION.Rows.Count; i++)
            {
                DrawPoint(Convert.ToInt32(dB.LOCATION.Rows[i][1].ToString()), Convert.ToInt32(dB.LOCATION.Rows[i][2].ToString()));
            }
        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(openFileDialog.FileName.ToString());
            dataGridView1.DataSource = dB.CONNECTION;
                    
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            xs = e.X;
            ys = e.Y;
            //Thread t = new Thread();
            //t.
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            g.Clear(Color.Transparent);
        }

        private void DrawPoint(int x, int y)
        {
            Rectangle rec = new Rectangle(new Point(x, y), new Size(new Point(5, 5)));
            g.DrawEllipse(p, rec); //Drawpoint
        }

        private void A()
        {
            Result.Clear();
        }

        private void UCS()
        {
            Result.Clear();
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            if (rabtn_ucs.Checked)
            {
                //UCS
            }
            else
            {
                //A*
            }
        }

        public static Stack<int> sortstack(Stack<int> input)
        {
            Stack<int> tmpStack = new Stack<int>();
            while (input.Count > 0)
            {
                // pop out the first element
                int tmp = input.Pop();

                // while temporary stack is not empty and
                // top of stack is greater than temp
                while (tmpStack.Count > 0 && tmpStack.Peek() > tmp)
                {
                    // pop from temporary stack and
                    // push it to the input stack
                    input.Push(tmpStack.Pop());
                }

                // push temp in temporary of stack
                tmpStack.Push(tmp);
            }
            return tmpStack;
        }
    }
}
