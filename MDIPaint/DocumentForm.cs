﻿using System;
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
        public Bitmap bitmap;
        public static int Beams {  get; set; }
        public static double R {  get; set; }
        public static double r { get; set; }
        public static int CurrentWidth { get; set; }
        public static int CurrentHeight {  get; set; }

        public DocumentForm(Bitmap image)
        {
            InitializeComponent();
            lastSavedFilePath = null;
            CurrentHeight = image.Height;
            CurrentWidth = image.Width;
            bitmap = new Bitmap(image, CurrentWidth, CurrentHeight);
            Beams = 5;
            R = 50;
            r = 25;
            this.DoubleBuffered = true;
        }

        public DocumentForm()
        {
            InitializeComponent();
            lastSavedFilePath = null;
            CurrentWidth = 300;
            CurrentHeight = 300;
            bitmap = new Bitmap(CurrentWidth, CurrentHeight);
            Beams = 5;
            R = 50;
            r = 25;
            this.DoubleBuffered = true;
            UpdateBitmap();
        }

        public void ResizeBitmap(int width, int height)
        {
            CurrentWidth = width; CurrentHeight = height;  

            var prev = new Bitmap(bitmap);

            bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.DrawImage(prev, 0, 0);
            }
            prev.Dispose();
            Invalidate();

        }

        public void Zoom(int width, int height)
        {

            Bitmap originalImage = new Bitmap(bitmap); 
            Bitmap scaledImage = new Bitmap((int)width, (int)height);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(
                    originalImage,
                    new Rectangle(0, 0, (int)width, (int)height),
                    new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                    GraphicsUnit.Pixel);
            }
            bitmap = scaledImage;

            Invalidate();

            CurrentHeight = (int)height;
            CurrentWidth = (int)width;

        }

        public void UpdateBitmap()
        {
            for (int Xcount = 0; Xcount < bitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < bitmap.Height; Ycount++)
                {
                    bitmap.SetPixel(Xcount, Ycount, Color.White);
                }
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
                switch (MainForm.Tool)
                {
                    case 1:
                        Refresh();
                        Graphics g = CreateGraphics();
                        g.DrawEllipse(new Pen(MainForm.Color, MainForm.Width), e.X, e.Y, x - e.X, y - e.Y);
                        HasPaint = true;
                        break;
                    case 2:
                        Refresh();
                        g = CreateGraphics();
                        g.DrawLine(new Pen(MainForm.Color, MainForm.Width), x, y, e.X, e.Y);
                        HasPaint = true;
                        break;
                    case 4:
                        g = Graphics.FromImage(bitmap);
                        g.DrawLine(new Pen(Color.White, MainForm.Width+3), x, y, e.X, e.Y);
                        Invalidate();
                        x = e.X;
                        y = e.Y;
                        HasPaint = true;
                        break;
                    case 5:
                        g = Graphics.FromImage(bitmap);
                        Pen pen = new Pen(MainForm.Color, MainForm.Width*2);
                        g.DrawLine(pen, x, y, e.X, e.Y);
                        g.FillEllipse(pen.Brush, e.X - MainForm.Width, e.Y - MainForm.Width, MainForm.Width * 2, MainForm.Width*2);
                        Invalidate();
                        x = e.X;
                        y = e.Y;
                        HasPaint = true;
                        break;
                }

            }

        }

        private void DocumentForm_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(bitmap);

            switch(MainForm.Tool)
            {
                case 1:
                    g.DrawEllipse(new Pen(MainForm.Color, MainForm.Width), e.X, e.Y, x - e.X, y - e.Y);
                    x = e.X;
                    y = e.Y;
                    Invalidate();
                    HasPaint = true;
                    break;
                case 2:
                    g.DrawLine(new Pen(MainForm.Color, MainForm.Width), x, y, e.X, e.Y);
                    x = e.X;
                    y = e.Y;
                    Invalidate();
                    HasPaint = true;
                    break;
                case 3:               
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
                    g.DrawLines(new Pen(MainForm.Color, MainForm.Width), points);
                    Invalidate();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

    }
}
