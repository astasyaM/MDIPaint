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
    public partial class CanvasSizeForm : Form
    {
        public DocumentForm frm {  get; set; }

        public CanvasSizeForm()
        {
            InitializeComponent();
            WidthSize.Text = DocumentForm.CurrentWidth.ToString();
            HeightSize.Text = DocumentForm.CurrentHeight.ToString();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            int width = Int32.Parse(WidthSize.Text);
            int height = Int32.Parse(HeightSize.Text);
            frm.ResizeBitmap(width, height);
            Close();
            
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WidthSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 59 || e.KeyChar <= 47) && e.KeyChar != 8)
            {
                e.Handled = true;
                DialogResult dr = MessageBox.Show("Введите число", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void HeightSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 59 || e.KeyChar <= 47) && e.KeyChar != 8)
            {
                e.Handled = true;
                DialogResult dr = MessageBox.Show("Введите число", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
