
namespace Trudoyomkost
{
    partial class ValidatingComboBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.insertedCBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // insertedCBox
            // 
            this.insertedCBox.FormattingEnabled = true;
            this.insertedCBox.Location = new System.Drawing.Point(3, 3);
            this.insertedCBox.Name = "insertedCBox";
            this.insertedCBox.Size = new System.Drawing.Size(177, 21);
            this.insertedCBox.TabIndex = 0;
            this.insertedCBox.TextChanged += new System.EventHandler(this.insertedCBox_TextChanged);
            // 
            // ValidatingComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.insertedCBox);
            this.Name = "ValidatingComboBox";
            this.Size = new System.Drawing.Size(221, 30);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ValueComboBox_Paint);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.ValidatingComboBox_Validating);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox insertedCBox;


    }
}
