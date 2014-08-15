namespace Trudoyomkost
{
    partial class fmDoubleNorms
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
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDetNum = new System.Windows.Forms.ComboBox();
            this.btDoubleExec = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(450, 18);
            this.label3.TabIndex = 22;
            this.label3.Text = "Дублировать нормы на другую деталь , зборку , нормаль, и тп.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbDetNum);
            this.groupBox1.Controls.Add(this.btDoubleExec);
            this.groupBox1.Location = new System.Drawing.Point(3, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 47);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // cbDetNum
            // 
            this.cbDetNum.FormattingEnabled = true;
            this.cbDetNum.Location = new System.Drawing.Point(9, 19);
            this.cbDetNum.Name = "cbDetNum";
            this.cbDetNum.Size = new System.Drawing.Size(273, 21);
            this.cbDetNum.TabIndex = 1;
            this.cbDetNum.SelectedIndexChanged += new System.EventHandler(this.cbDetNum_SelectedIndexChanged);
            // 
            // btDoubleExec
            // 
            this.btDoubleExec.Location = new System.Drawing.Point(315, 19);
            this.btDoubleExec.Name = "btDoubleExec";
            this.btDoubleExec.Size = new System.Drawing.Size(114, 23);
            this.btDoubleExec.TabIndex = 0;
            this.btDoubleExec.Text = "Применить";
            this.btDoubleExec.UseVisualStyleBackColor = true;
            this.btDoubleExec.Click += new System.EventHandler(this.btDoubleExec_Click);
            // 
            // fmDoubleNorms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 91);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fmDoubleNorms";
            this.Text = "fmDoubleNorms";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbDetNum;
        private System.Windows.Forms.Button btDoubleExec;
    }
}