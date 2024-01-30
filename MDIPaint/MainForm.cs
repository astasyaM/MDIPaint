using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIPaint
{
    public partial class MainForm : Form
    {
        public static Color Color { get; set; }
        public static int Width { get; set; }
        public static int Tool { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Color = Color.Black;
            Width = 3;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmAbout = new AboutForm();
            frmAbout.ShowDialog();
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new DocumentForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void размерХолстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new CanvasSizeForm();
            frm.MdiParent = this;
            frm.frm = ActiveMdiChild as DocumentForm;
            frm.Show();
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Red;
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Blue;
        }

        private void зелёныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Green;
        }

        private void другойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                Color = cd.Color;
        }

        private void рисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            размерХолстаToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (WidthText.Text != "")
                Width = Int32.Parse(WidthText.Text);
        }

        private void WidthText_Leave(object sender, EventArgs e)
        {
        }

        private void Ellips_Click(object sender, EventArgs e)
        {
            Tool = 1;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Tool = 2;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Tool = 3;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Tool = 4;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Tool = 5;
        }

        private void toolStripButton3_ButtonClick(object sender, EventArgs e)
        {

        }

        private void настроитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new StarSize();
            frm.MdiParent = this;
            frm.frm = ActiveMdiChild as DocumentForm;
            frm.Show();
        }

        private void стандартнаяЗвездаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentForm.Beams = 5;
            DocumentForm.R = 50;
            DocumentForm.r = 25;
        }
    }
}
