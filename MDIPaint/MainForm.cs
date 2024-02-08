using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIPaint
{
    public partial class MainForm : Form
    {

        public DocumentForm frm { get; set; }
        public static Color Color { get; set; }
        public static int Width { get; set; }
        public static int Tool { get; set; }
        public static int Forms {  get; set; }

        public MainForm()
        {
            InitializeComponent();
            сохранитьToolStripMenuItem.Enabled = false;
            сохранитьКакToolStripMenuItem.Enabled = false;
            размерХолстаToolStripMenuItem.Enabled = false;
            Color = Color.Black;
            Width = 3;
            Tool = 5;
            Forms = 0;
            WidthText.Text = Width.ToString();
        }

        public void CheckButtons()
        {
            if (Forms==0)
            {
                сохранитьToolStripMenuItem.Enabled = false;
                сохранитьКакToolStripMenuItem.Enabled = false;
                размерХолстаToolStripMenuItem.Enabled = false;
            }
            else
            {
                сохранитьToolStripMenuItem.Enabled = true;
                сохранитьКакToolStripMenuItem.Enabled = true;
                размерХолстаToolStripMenuItem.Enabled = true;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr1 == DialogResult.Yes)
            {
                var frm = this.MdiChildren.OfType<DocumentForm>().FirstOrDefault();
                if (frm == null || frm.HasPaint == false)
                {
                    Environment.Exit(0);
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Сохранить изменения?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        Program.MainForm.сохранитьКакToolStripMenuItem_Click(sender, e);
                        Environment.Exit(0);
                    }
                    else if (dr == DialogResult.No)
                        Environment.Exit(0);
                    else
                        return;
                }
            }
            else
                return;
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
            Forms += 1;
            CheckButtons();
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

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            var frm = this.MdiChildren.OfType<DocumentForm>().FirstOrDefault();
            frm.Zoom(DocumentForm.CurrentWidth+50, DocumentForm.CurrentHeight + 50);
            
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            var frm = this.MdiChildren.OfType<DocumentForm>().FirstOrDefault();
            frm.Zoom(DocumentForm.CurrentWidth - 50, DocumentForm.CurrentHeight - 50);
        }

        public void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = this.MdiChildren.OfType<DocumentForm>().FirstOrDefault();
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Bitmap Images|*.bmp|JPEG Images|*.jpg";
            saveDialog.Title = "Save Drawing";

            // Если есть предыдущий путь, устанавливаем его в диалог сохранения
            if (!string.IsNullOrEmpty(DocumentForm.lastSavedFilePath))
            {
                saveDialog.InitialDirectory = System.IO.Path.GetDirectoryName(DocumentForm.lastSavedFilePath);
                saveDialog.FileName = System.IO.Path.GetFileName(DocumentForm.lastSavedFilePath);
            }

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                // Получение пути и названия файла
                string filePath = saveDialog.FileName;

                // Определение формата изображения в зависимости от выбора пользователя
                ImageFormat imageFormat = ImageFormat.Bmp;

                if (saveDialog.FilterIndex == 2)
                {
                    imageFormat = ImageFormat.Jpeg;
                }

                // Сохранение Bitmap в выбранный файл
                Image img = frm.GetPicture();
                img.Save(filePath, imageFormat);

                // Обновляем последний сохраненный путь
                DocumentForm.lastSavedFilePath = filePath;
            }
            CheckButtons();

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Простое сохранение без диалога выбора файла
            if (!string.IsNullOrEmpty(DocumentForm.lastSavedFilePath))
            {
                // Определение формата изображения (в данном случае BMP)
                ImageFormat imageFormat = ImageFormat.Bmp;

                // Перезаписываем файл
                Image img = frm.GetPicture();
                img.Save(DocumentForm.lastSavedFilePath, imageFormat);
            }
            else
            {
                // Если нет предыдущего пути, вызываем диалог сохранения
                сохранитьКакToolStripMenuItem_Click(sender, e);
            }
            CheckButtons();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms += 1;
            Bitmap openImage = null;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Bitmap Images|*.bmp|JPEG Images|*.jpg";
            bool flag = false;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openImage = new Bitmap(openDialog.FileName);
                    flag = true;
                }
                catch
                {
                    DialogResult res = MessageBox.Show("Невозможно открыть выбранный файл!", "Внимание!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (flag)
                {
                    DocumentForm newChildForm = new DocumentForm(openImage);
                    newChildForm.MdiParent = this;
                    newChildForm.Text = openDialog.FileName;
                    newChildForm.Show();
                }
            }
            CheckButtons();
        }

        private void каскадомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void слеваНаправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void сверхуВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void упорядочитьЗначкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr1 == DialogResult.Yes)
            {
                var frm = this.MdiChildren.OfType<DocumentForm>().FirstOrDefault();
                if (frm == null || frm.HasPaint == false)
                {
                    return;
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Сохранить изменения?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        Program.MainForm.сохранитьКакToolStripMenuItem_Click(sender, e);
                        return;
                    }
                    else if (dr == DialogResult.No)
                        return;
                    else
                        e.Cancel = true;
                }
            }
            else
                e.Cancel = true;
        }
    }
}
