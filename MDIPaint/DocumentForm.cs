using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace MDIPaint
{
    public partial class DocumentForm : Form
    {
        public static string lastSavedFilePath;
        private int x, y;
        public bool HasPaint = false;
        public static int Beams {  get; set; }
        public static double R {  get; set; }
        public static double r { get; set; }
        public static int CurrentWidth { get; set; }
        public static int CurrentHeight {  get; set; }
        private float CurrentScaleX {  get; set; }
        private float CurrentScaleY { get; set; }
        private float BaseScaleW { get; set; }
        private float BaseScaleH { get; set; }

        public DocumentForm(Bitmap image)
        {
            InitializeComponent();
            lastSavedFilePath = null;
            CurrentHeight = image.Height;
            CurrentWidth = image.Width;
            Beams = 5;
            R = 50;
            r = 25;
            pictureBox1.Size = new Size(CurrentWidth, CurrentHeight);
            pictureBox1.Image = image;
            pictureBox1.BackColor = System.Drawing.Color.White;
            CurrentScaleX = 1;
            CurrentScaleY = 1;
            BaseScaleH = image.Height;
            BaseScaleW = image.Width;
        }

        public DocumentForm()
        {
            InitializeComponent();
            lastSavedFilePath = null;
            CurrentWidth = 300;
            CurrentHeight = 300;
            Beams = 5;
            R = 50;
            r = 25;
            pictureBox1.Size = new Size(CurrentWidth, CurrentHeight);
            pictureBox1.Image = new Bitmap(CurrentWidth, CurrentHeight);
            pictureBox1.BackColor = System.Drawing.Color.White;
            UpdateBitmap();
            CurrentScaleX = 1;
            CurrentScaleY = 1;
            BaseScaleW = 300;
            BaseScaleH = 300;
        }

        public void UpdateBitmap()
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            for (int Xcount = 0; Xcount < pictureBox1.Image.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < pictureBox1.Image.Height; Ycount++)
                {
                    bitmap.SetPixel(Xcount, Ycount, System.Drawing.Color.White);
                }
            }
        }

        public System.Drawing.Image GetPicture()
        {
            return pictureBox1.Image;
        }

        public void ResizeBitmap(int width, int height)
        {
            CurrentWidth = width; CurrentHeight = height;  

            var prev = new Bitmap(pictureBox1.Image);

            pictureBox1.Image = new Bitmap(width, height);
            pictureBox1.Size = new Size(width, height);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);
                g.DrawImage(prev, 0, 0);
            }
            prev.Dispose();
            pictureBox1.Invalidate();
            Invalidate();

        }

        public void Zoom(int width, int height)
        {
            if (width>50 && height>50 && width<1200 && height<1200)
            {
                pictureBox1.Size = new Size(width, height);
                CurrentScaleX = BaseScaleW / width; CurrentScaleY = BaseScaleH / height;
                CurrentHeight = height;
                CurrentWidth = width;
                pictureBox1.Refresh();
            }
        }

        private void DocumentForm_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void DocumentForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int baseX = (Int32)(e.X * CurrentScaleX);
                int baseY = (Int32)(e.Y * CurrentScaleY);

                int endX = (Int32)(x * CurrentScaleX);
                int endY = (Int32)(y * CurrentScaleY);

                int w = (Int32)(MainForm.Width / CurrentScaleX);

                switch (MainForm.Tool)
                {
                    case 1:
                        Refresh();
                        Graphics g = pictureBox1.CreateGraphics();
                        
                        g.DrawEllipse(new Pen(MainForm.Color, w*2), e.X, e.Y, x - e.X, y - e.Y);
                        HasPaint = true;
                        break;
                    case 2:
                        Refresh();
                        g = pictureBox1.CreateGraphics();
                        g.DrawLine(new Pen(MainForm.Color, w*2), x, y, e.X, e.Y);
                        Invalidate();
                        HasPaint = true;
                        break;
                    case 3:
                        Refresh();
                        g = pictureBox1.CreateGraphics();
                        double beta = 0;
                        double x0 = e.X, y0 = e.Y;
                        PointF[] points = new PointF[2 * Beams + 1];
                        double a = beta, da = Math.PI / Beams, l;
                        for (int k = 0; k < 2 * Beams + 1; k++)
                        {
                            l = k % 2 == 0 ? r : R;
                            points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                            a += da;
                        }
                        g.DrawLines(new Pen(MainForm.Color, w * 2), points);
                        Invalidate();
                        pictureBox1.Invalidate();
                        HasPaint = true;
                        break;
                    case 4:
                        g = Graphics.FromImage(pictureBox1.Image);
                        Pen pen = new Pen(Color.White, MainForm.Width * 2 + 3);
                        g.DrawLine(pen, endX, endY, baseX, baseY);
                        g.FillEllipse(pen.Brush, (e.X - MainForm.Width) * CurrentScaleX, (e.Y - MainForm.Width) * CurrentScaleY, MainForm.Width * 2, MainForm.Width * 2);
                        Invalidate();
                        pictureBox1.Invalidate();
                        x = e.X;
                        y = e.Y;
                        HasPaint = true;
                        break;
                    case 5:
                        g = Graphics.FromImage(pictureBox1.Image);
                        pen = new Pen(MainForm.Color, MainForm.Width*2);
                        g.DrawLine(pen, endX, endY, baseX, baseY);
                        g.FillEllipse(pen.Brush, (e.X - MainForm.Width)*CurrentScaleX, (e.Y - MainForm.Width)*CurrentScaleY, MainForm.Width * 2, MainForm.Width*2);
                        Invalidate();
                        pictureBox1.Invalidate();
                        x = e.X;
                        y = e.Y;
                        HasPaint = true;
                        break;
                }

            }

        }

        private void DocumentForm_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);

            int baseX = (Int32)(e.X * CurrentScaleX);
            int baseY = (Int32)(e.Y * CurrentScaleY);

            int endX = (Int32)(x * CurrentScaleX);
            int endY = (Int32)(y * CurrentScaleY);

            switch (MainForm.Tool)
            {
                case 1:
                    g.DrawEllipse(new Pen(MainForm.Color, MainForm.Width*2), baseX, baseY, endX - baseX, endY - baseY);
                    x = baseX;
                    y = baseY;
                    Invalidate();
                    pictureBox1.Invalidate();
                    HasPaint = true;
                    break;
                case 2:
                    g.DrawLine(new Pen(MainForm.Color, MainForm.Width * 2), endX, endY, baseX, baseY);
                    x = baseX;
                    y = baseY;
                    Invalidate();
                    pictureBox1.Invalidate();
                    HasPaint = true;
                    break;
                case 3:               
                    double beta = 0;       
                    double x0 = baseX, y0 = baseY; 
                    PointF[] points = new PointF[2 * Beams + 1];
                    double a = beta, da = Math.PI / Beams, l;
                    for (int k = 0; k < 2 * Beams + 1; k++)
                    {
                        l = k % 2 == 0 ? r : R;
                        points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                        a += da;
                    }
                    g.DrawLines(new Pen(MainForm.Color, MainForm.Width*2), points);
                    Invalidate();
                    pictureBox1.Invalidate();
                    HasPaint = true;
                    break;
                case 4:
                    g = Graphics.FromImage(pictureBox1.Image);
                    Pen pen = new Pen(Color.White, MainForm.Width * 2 + 3);
                    g.DrawLine(pen, endX, endY, baseX, baseY);
                    g.FillEllipse(pen.Brush, baseX - MainForm.Width, baseY - MainForm.Width, MainForm.Width * 2, MainForm.Width * 2);
                    Invalidate();
                    pictureBox1.Invalidate();
                    x = baseX;
                    y = baseY;
                    HasPaint = true;
                    break;
                case 5:
                    g = Graphics.FromImage(pictureBox1.Image);
                    pen = new Pen(MainForm.Color, MainForm.Width * 2);
                    g.DrawLine(pen, endX, endY, baseX, baseY);
                    g.FillEllipse(pen.Brush, baseX - MainForm.Width, baseY - MainForm.Width, MainForm.Width * 2, MainForm.Width * 2);
                    Invalidate();
                    pictureBox1.Invalidate();
                    x = baseX;
                    y = baseY;
                    HasPaint = true;
                    break;
            }
        }

        private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HasPaint == false)
            {
                MainForm.Forms--;
                Program.MainForm.CheckButtons();
                return;
            }
            else
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    DialogResult dr = MessageBox.Show("Сохранить изменения?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        Program.MainForm.сохранитьКакToolStripMenuItem_Click(sender, e);
                        MainForm.Forms--;
                        Program.MainForm.CheckButtons();
                    }
                    else if (dr == DialogResult.No)
                    {
                        MainForm.Forms--;
                        Program.MainForm.CheckButtons();
                        return;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            
        }

        private void DocumentForm_FormC(object sender, FormClosingEventArgs e)
        {

        }


    }
}
