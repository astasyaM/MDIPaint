﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace MDIPaint
{
    public partial class DocumentForm : Form
    {
        private int x, y;
        public static Bitmap bitmap;
        public static int Beams {  get; set; }
        public static double R {  get; set; }
        public static double r { get; set; }

        public DocumentForm()
        {
            InitializeComponent();
            bitmap = new Bitmap(500, 500);
            Beams = 5;
            R = 50;
            r = 25;
            UpdateBitmap();
        }

        public void ResizeBitmap(int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bitmap, 0, 0, width, height);
            }
            bitmap = result;
            UpdateBitmap();
            Invalidate();
            
        }

        public static void UpdateBitmap()
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
                        break;
                    case 2:
                        Refresh();
                        g = CreateGraphics();
                        g.DrawLine(new Pen(MainForm.Color, MainForm.Width), x, y, e.X, e.Y);
                        break;
                    case 4:
                        g = Graphics.FromImage(bitmap);
                        g.DrawLine(new Pen(Color.White, MainForm.Width+3), x, y, e.X, e.Y);
                        Invalidate();
                        x = e.X;
                        y = e.Y;
                        break;
                    case 5:
                        g = Graphics.FromImage(bitmap);
                        g.DrawLine(new Pen(MainForm.Color, MainForm.Width), x, y, e.X, e.Y);
                        Invalidate();
                        x = e.X;
                        y = e.Y;
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
                    break;
                case 2:
                    g.DrawLine(new Pen(MainForm.Color, MainForm.Width), x, y, e.X, e.Y);
                    x = e.X;
                    y = e.Y;
                    Invalidate();
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
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

    }
}
