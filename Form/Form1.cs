
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
        static int Default_Scr_x = 750;
        static int Default_Scr_y = 700;
        static double new_Scr_x;
        static double new_Scr_y;
        public static double factor_x;
        public static double factor_y;
        private Graphics g;
        int xs, ys;
        Pen p_A = new Pen(Color.Blue, 5);
        Pen p_UCS = new Pen(Color.Green, 5);
        Pen p_point = new Pen(Color.Red, 5);
        readonly DB dB = new DB();
        Queue<POINT> Result = new Queue<POINT>();
        InfoForm A_star = new InfoForm();
        public Form1()
        {
            InitializeComponent();
            /*Rectangle rec = new Rectangle(0, 0, 30, 60);
            Pen p = new Pen(Color.Blue, 3);
            rec.*/
            g = this.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            AddItemToCombox();
            comboBox_from.SelectedIndex = 0;
            comboBox_to.SelectedIndex = 0;
            Control.CheckForIllegalCrossThreadCalls = false;
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
            //label4.Text = "Fac X : " + Math.Round(factor_x, 4) + " \nFac Y : " + Math.Round(factor_y, 4);
            
            //MessageBox.Show("x = " + x.ToString() +", y = " + y.ToString() );
            //g.DrawLine(p, x, y, x+ad, y+ad);
            //DrawPoint(xs, ys);
            //MessageBox.Show("X " + xs.ToString() + " - Y" + ys.ToString());
            for (int i = 0; i< dB.LOCATION.Rows.Count; i++)
            {
                double tmp_x = Convert.ToDouble(dB.LOCATION.Rows[i][1]) * Form1.factor_x;
                double tmp_y = Convert.ToDouble(dB.LOCATION.Rows[i][2]) * Form1.factor_y;
                DrawPoint(Convert.ToInt32(Math.Round(tmp_x,0).ToString()), Convert.ToInt32(Math.Round(tmp_y, 0).ToString()));
            }
        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(openFileDialog.FileName.ToString());
            //dataGridView1.DataSource = dB.CONNECTION;
                    
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
            g.Clear(Color.White);
            this.BackgroundImage = global::AI.Properties.Resources.Map3;
            new_Scr_x = this.Width;
            new_Scr_y = this.Height;
            factor_x = (double)new_Scr_x / (double)Default_Scr_x;
            factor_y = (double)new_Scr_y / (double)Default_Scr_y;
            label4.Text = "Fac X : " + Math.Round(factor_x, 4) + " \nFac Y : " + Math.Round(factor_y, 4);
            g.Clear(Color.White);
            this.BackgroundImage = global::AI.Properties.Resources.Map3;
            g.SetClip(new Rectangle(0, 0, (int)new_Scr_x, (int)new_Scr_x));
        }

        private void DrawPoint(int x, int y)
        {
            
            Rectangle rec = new Rectangle(new Point(x, y), new Size(new Point(5, 5)));
            g.DrawEllipse(p_point, rec); //Drawpoint
        }

        private void Draw(Queue<POINT> Result, int type)
        {
            new Thread(
                () =>
                   {
                       for (int i = 0; i < Result.Count - 1; i++)
                       {
                           Thread.Sleep(trackBar1.Value * 10);
                           double x1 = (double)Result.ElementAt(i).X * Form1.factor_x;
                           double y1 = (double)Result.ElementAt(i).Y * Form1.factor_y;
                           double x2 = (double)Result.ElementAt(i + 1).X * Form1.factor_x;
                           double y2 = (double)Result.ElementAt(i + 1).Y * Form1.factor_y;
                           g.DrawLine((type == 0)? p_A : p_UCS, (int)x1, (int)y1, (int)x2, (int)y2);
                       }
                   } ) { IsBackground = true }.Start();
            
        }

        private void Astar_exec(string StartPoint, string EndPoint)
        {
            InfoForm A_star = new InfoForm();
            Result.Clear();
            Result = A.Run_A(StartPoint, EndPoint, A_star);
            Draw(Result,0);
            A_star.Show();
        }

        private void UCS_exec(string StartPoint, string EndPoint)
        {
            InfoForm UCS_ = new InfoForm();
            Result.Clear();
            Result = UCS.Run_UCS(StartPoint, EndPoint, UCS_);
            Draw(Result,1);
            UCS_.Show();
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            ComboBoxItem itm_from = (ComboBoxItem)comboBox_from.SelectedItem;
            ComboBoxItem itm_to = (ComboBoxItem)comboBox_to.SelectedItem;
            if (itm_from.Value == itm_to.Value)
            {
                MessageBox.Show("Trùng điểm !! ");
            }
            else
            {
                if (rabtn_ucs.Checked)
                {
                    //UCS
                    UCS_exec(itm_from.Value, itm_to.Value);
                }
                else
                {
                    //A*
                    Astar_exec(itm_from.Value, itm_to.Value);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*Graphics tmp;
            tmp = this.CreateGraphics();
            g.Clip = tmp.Clip.Clone();*/
            new_Scr_x = this.Width;
            new_Scr_y = this.Height;
            factor_x = (double)new_Scr_x / (double)Default_Scr_x;
            factor_y = (double)new_Scr_y / (double)Default_Scr_y;
            label4.Text = "Fac X : " + Math.Round(factor_x, 4) + " \nFac Y : " + Math.Round(factor_y, 4);
            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

            //g.ScaleTransform((float)new_Scr_x, (float)new_Scr_y);

            //MessageBox.Show(g.Clip.ToString()); 
            new_Scr_x = this.Width;
            new_Scr_y = this.Height;
            factor_x = (double)new_Scr_x / (double)Default_Scr_x;
            factor_y = (double)new_Scr_y / (double)Default_Scr_y;
            label4.Text = "Fac X : " + Math.Round(factor_x, 4) + " \nFac Y : " + Math.Round(factor_y, 4);
            g.Clear(Color.White);
            this.BackgroundImage = global::AI.Properties.Resources.Map3;
            
            g.SetClip(new Rectangle(0, 0, (int)new_Scr_x, (int)new_Scr_x), System.Drawing.Drawing2D.CombineMode.Complement);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            new_Scr_x = this.Width;
            new_Scr_y = this.Height;
            factor_x = (double)new_Scr_x / (double)Default_Scr_x;
            factor_y = (double)new_Scr_y / (double)Default_Scr_y;
            label4.Text = "Fac X : " + Math.Round(factor_x, 4) + " \nFac Y : " + Math.Round(factor_y, 4);
            g.Clear(Color.White);
            this.BackgroundImage = global::AI.Properties.Resources.Map3;

            g.SetClip(new Rectangle(0, 0, (int)new_Scr_x, (int)new_Scr_x), System.Drawing.Drawing2D.CombineMode.Complement);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            new_Scr_x = this.Width;
            new_Scr_y = this.Height;
            factor_x = (double)new_Scr_x / (double)Default_Scr_x;
            factor_y = (double)new_Scr_y / (double)Default_Scr_y;
            label4.Text = "Fac X : " + Math.Round(factor_x, 4) + " \nFac Y : " + Math.Round(factor_y, 4);
            g.Clear(Color.White);
            this.BackgroundImage = global::AI.Properties.Resources.Map3;

            
            g.SetClip(new Rectangle(0, 0, (int)new_Scr_x, (int)new_Scr_x), System.Drawing.Drawing2D.CombineMode.Complement);
        }

        private void AddItemToCombox()
        {
            DataTable Name = dB.NAME;
            for (int i = 0; i < Name.Rows.Count; i++)
            {
                comboBox_from.Items.Add(new ComboBoxItem(Name.Rows[i].Field<string>("Name"), Name.Rows[i].Field<string>("ID")));
            }
            for (int i = 0; i < Name.Rows.Count; i++)
            {
                comboBox_to.Items.Add(new ComboBoxItem(Name.Rows[i].Field<string>("Name"), Name.Rows[i].Field<string>("ID")));
            }
        }
    }
}
