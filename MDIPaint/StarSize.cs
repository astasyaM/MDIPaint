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
    public partial class StarSize : Form
    {
        public DocumentForm frm { get; set; }

        public StarSize()
        {
            InitializeComponent();
            BeamsTB.Text = DocumentForm.Beams.ToString();
            RTB.Text = DocumentForm.R.ToString();
            rTBsmall.Text = DocumentForm.r.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DocumentForm.Beams = Int32.Parse(BeamsTB.Text);
            DocumentForm.R = Double.Parse(RTB.Text);
            DocumentForm.r = Double.Parse(rTBsmall.Text);

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BeamsTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 59 || e.KeyChar <= 47) && e.KeyChar != 8)
            {
                e.Handled = true;
                DialogResult dr = MessageBox.Show("Введите число", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 59 || e.KeyChar <= 47) && e.KeyChar != 8)
            {
                e.Handled = true;
                DialogResult dr = MessageBox.Show("Введите число", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rTBsmall_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 59 || e.KeyChar <= 47) && e.KeyChar != 8)
            {
                e.Handled = true;
                DialogResult dr = MessageBox.Show("Введите число", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
