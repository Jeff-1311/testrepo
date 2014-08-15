namespace Trudoyomkost
{
    partial class fmGroupUpdate
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
            this.components = new System.ComponentModel.Container();
            this.gpAddOperApply = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.vtbSeriaTo = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbSeriaFrom = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbProdName = new Trudoyomkost.ValidatingTextBox(this.components);
            this.btAddOper = new System.Windows.Forms.Button();
            this.dgOperApply = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.vtbLowerCoff = new Trudoyomkost.ValidatingTextBox(this.components);
            this.chBoxWithoutNew = new System.Windows.Forms.CheckBox();
            this.gpAddOperApply.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperApply)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpAddOperApply
            // 
            this.gpAddOperApply.Controls.Add(this.label16);
            this.gpAddOperApply.Controls.Add(this.label15);
            this.gpAddOperApply.Controls.Add(this.label10);
            this.gpAddOperApply.Controls.Add(this.vtbSeriaTo);
            this.gpAddOperApply.Controls.Add(this.vtbSeriaFrom);
            this.gpAddOperApply.Controls.Add(this.vtbProdName);
            this.gpAddOperApply.Controls.Add(this.btAddOper);
            this.gpAddOperApply.Location = new System.Drawing.Point(18, 173);
            this.gpAddOperApply.Name = "gpAddOperApply";
            this.gpAddOperApply.Size = new System.Drawing.Size(419, 53);
            this.gpAddOperApply.TabIndex = 2;
            this.gpAddOperApply.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(224, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 13);
            this.label16.TabIndex = 25;
            this.label16.Text = "Серия По";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(131, 5);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Серия С ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Изделие";
            // 
            // vtbSeriaTo
            // 
            this.vtbSeriaTo.Location = new System.Drawing.Point(221, 21);
            this.vtbSeriaTo.Name = "vtbSeriaTo";
            this.vtbSeriaTo.Size = new System.Drawing.Size(98, 20);
            this.vtbSeriaTo.StringAutoCorrectionMethod = null;
            this.vtbSeriaTo.TabIndex = 2;
            this.vtbSeriaTo.ValidateValue = null;
            this.vtbSeriaTo.ValueTxt = "";
            // 
            // vtbSeriaFrom
            // 
            this.vtbSeriaFrom.Location = new System.Drawing.Point(117, 21);
            this.vtbSeriaFrom.Name = "vtbSeriaFrom";
            this.vtbSeriaFrom.Size = new System.Drawing.Size(98, 20);
            this.vtbSeriaFrom.StringAutoCorrectionMethod = null;
            this.vtbSeriaFrom.TabIndex = 1;
            this.vtbSeriaFrom.ValidateValue = null;
            this.vtbSeriaFrom.ValueTxt = "";
            // 
            // vtbProdName
            // 
            this.vtbProdName.Location = new System.Drawing.Point(6, 21);
            this.vtbProdName.Name = "vtbProdName";
            this.vtbProdName.Size = new System.Drawing.Size(105, 20);
            this.vtbProdName.StringAutoCorrectionMethod = null;
            this.vtbProdName.TabIndex = 0;
            this.vtbProdName.ValidateValue = null;
            this.vtbProdName.ValueTxt = "";
            // 
            // btAddOper
            // 
            this.btAddOper.Location = new System.Drawing.Point(325, 5);
            this.btAddOper.Name = "btAddOper";
            this.btAddOper.Size = new System.Drawing.Size(85, 36);
            this.btAddOper.TabIndex = 23;
            this.btAddOper.Text = "Добавить";
            this.btAddOper.UseVisualStyleBackColor = true;
            this.btAddOper.Click += new System.EventHandler(this.btAddOper_Click);
            // 
            // dgOperApply
            // 
            this.dgOperApply.AllowUserToAddRows = false;
            this.dgOperApply.AllowUserToDeleteRows = false;
            this.dgOperApply.AllowUserToResizeColumns = false;
            this.dgOperApply.AllowUserToResizeRows = false;
            this.dgOperApply.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOperApply.Location = new System.Drawing.Point(18, 19);
            this.dgOperApply.MultiSelect = false;
            this.dgOperApply.Name = "dgOperApply";
            this.dgOperApply.ReadOnly = true;
            this.dgOperApply.RowHeadersVisible = false;
            this.dgOperApply.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgOperApply.RowTemplate.Height = 19;
            this.dgOperApply.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOperApply.Size = new System.Drawing.Size(419, 138);
            this.dgOperApply.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgOperApply);
            this.groupBox1.Controls.Add(this.gpAddOperApply);
            this.groupBox1.Location = new System.Drawing.Point(28, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(457, 253);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.vtbLowerCoff);
            this.groupBox2.Location = new System.Drawing.Point(504, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 93);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Применить  коэффициент";
            // 
            // vtbLowerCoff
            // 
            this.vtbLowerCoff.Location = new System.Drawing.Point(19, 44);
            this.vtbLowerCoff.Name = "vtbLowerCoff";
            this.vtbLowerCoff.Size = new System.Drawing.Size(96, 20);
            this.vtbLowerCoff.StringAutoCorrectionMethod = null;
            this.vtbLowerCoff.TabIndex = 26;
            this.vtbLowerCoff.ValidateValue = null;
            this.vtbLowerCoff.ValueTxt = "";
            // 
            // chBoxWithoutNew
            // 
            this.chBoxWithoutNew.AutoSize = true;
            this.chBoxWithoutNew.Location = new System.Drawing.Point(504, 152);
            this.chBoxWithoutNew.Name = "chBoxWithoutNew";
            this.chBoxWithoutNew.Size = new System.Drawing.Size(180, 17);
            this.chBoxWithoutNew.TabIndex = 29;
            this.chBoxWithoutNew.Text = "Только ограничить номокарту";
            this.chBoxWithoutNew.UseVisualStyleBackColor = true;
            this.chBoxWithoutNew.CheckedChanged += new System.EventHandler(this.chBoxWithoutNew_CheckedChanged);
            // 
            // fmGroupUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 303);
            this.Controls.Add(this.chBoxWithoutNew);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "fmGroupUpdate";
            this.Text = "Ограничение нормокарты";
            this.gpAddOperApply.ResumeLayout(false);
            this.gpAddOperApply.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperApply)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpAddOperApply;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private ValidatingTextBox vtbSeriaTo;
        private ValidatingTextBox vtbSeriaFrom;
        private ValidatingTextBox vtbProdName;
        private System.Windows.Forms.Button btAddOper;
        private System.Windows.Forms.DataGridView dgOperApply;
        private ValidatingTextBox vtbLowerCoff;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chBoxWithoutNew;
    }
}