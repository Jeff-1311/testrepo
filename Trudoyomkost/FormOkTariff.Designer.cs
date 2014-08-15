namespace Trudoyomkost
{
    partial class FormOkTariff
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
            this.buttonForNewTariff = new System.Windows.Forms.Button();
            this.textBoxForTariff = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonForNewTariff
            // 
            this.buttonForNewTariff.Location = new System.Drawing.Point(12, 38);
            this.buttonForNewTariff.Name = "buttonForNewTariff";
            this.buttonForNewTariff.Size = new System.Drawing.Size(96, 23);
            this.buttonForNewTariff.TabIndex = 0;
            this.buttonForNewTariff.Text = "Ок";
            this.buttonForNewTariff.UseVisualStyleBackColor = true;
            this.buttonForNewTariff.Click += new System.EventHandler(this.buttonForNewTariff_Click);
            // 
            // textBoxForTariff
            // 
            this.textBoxForTariff.Location = new System.Drawing.Point(12, 12);
            this.textBoxForTariff.Name = "textBoxForTariff";
            this.textBoxForTariff.Size = new System.Drawing.Size(198, 20);
            this.textBoxForTariff.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(114, 38);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "<= Сюда введите номер новой тарифной сетки";
            // 
            // FormOkTariff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 71);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxForTariff);
            this.Controls.Add(this.buttonForNewTariff);
            this.Name = "FormOkTariff";
            this.Text = "FormOkTariff";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonForNewTariff;
        private System.Windows.Forms.TextBox textBoxForTariff;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
    }
}