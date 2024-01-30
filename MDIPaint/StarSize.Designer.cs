namespace MDIPaint
{
    partial class StarSize
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BeamsTB = new System.Windows.Forms.TextBox();
            this.rTBsmall = new System.Windows.Forms.TextBox();
            this.RTB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество вершин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Внешний радиус";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Внутренний радиус";
            // 
            // BeamsTB
            // 
            this.BeamsTB.Location = new System.Drawing.Point(193, 51);
            this.BeamsTB.Name = "BeamsTB";
            this.BeamsTB.Size = new System.Drawing.Size(89, 20);
            this.BeamsTB.TabIndex = 3;
            // 
            // rTBsmall
            // 
            this.rTBsmall.Location = new System.Drawing.Point(193, 103);
            this.rTBsmall.Name = "rTBsmall";
            this.rTBsmall.Size = new System.Drawing.Size(89, 20);
            this.rTBsmall.TabIndex = 4;
            // 
            // RTB
            // 
            this.RTB.Location = new System.Drawing.Point(193, 77);
            this.RTB.Name = "RTB";
            this.RTB.Size = new System.Drawing.Size(89, 20);
            this.RTB.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "Ок";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(170, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 30);
            this.button2.TabIndex = 7;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // StarSize
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(334, 219);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.RTB);
            this.Controls.Add(this.rTBsmall);
            this.Controls.Add(this.BeamsTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StarSize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StarSize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox BeamsTB;
        private System.Windows.Forms.TextBox rTBsmall;
        private System.Windows.Forms.TextBox RTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}