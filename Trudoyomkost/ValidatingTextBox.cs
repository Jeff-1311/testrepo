using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trudoyomkost
{
    public delegate bool ValidatingMethod(string input);
    public delegate string StringCorrectionMethod(string input);

    public partial class ValidatingTextBox : UserControl
    {
        private Bitmap bmpOkBad;
        private ValidatingMethod validateValue;
        public int  MaxLength=50;
        public ValidatingMethod ValidateValue
        {
            get
            {
                return validateValue;
            }
            set
            {
                validateValue = value;
            }
        }

        private StringCorrectionMethod stringAutoCorrectionMethod;
        public StringCorrectionMethod StringAutoCorrectionMethod
        {
            get
            {
                return stringAutoCorrectionMethod;
            }
            set
            {
                stringAutoCorrectionMethod = value;
            }
        }

        private bool isValid = false;

        public bool IsValid
        {
            get
            {
                return isValid;
            }
            private set
            {
                isValid = value;
                Invalidate();
            }
        }

        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {
                return tbValue.AutoCompleteCustomSource;
            }
            set
            {
                tbValue.AutoCompleteCustomSource = value;
            }
        }

        public string ValueTxt
        {
            get
            {
                return tbValue.Text;
            }
            set
            {
                tbValue.Text = value;
            }
        }

        public ValidatingTextBox()
        {
            bmpOkBad = Trudoyomkost.Properties.Resources.OK_BAD;
            InitializeComponent();
        }
        public ValidatingTextBox(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            bmpOkBad = Trudoyomkost.Properties.Resources.OK_BAD;
            InitializeComponent();
        }

        private void ValidatingTextBox_Paint(object sender, PaintEventArgs e)
        {
            Rectangle sourceRectangle;
            if (isValid || this.Enabled == false)
            {
                sourceRectangle = new Rectangle(0, 0, 20, 20);//рисуем галочку              
            }
            else
            {
                sourceRectangle = new Rectangle(20, 0, 20, 20);//рисуем крестик
            }
            Rectangle targetRectangle = new Rectangle(this.Size.Width - 20, 0, 20, 20);
            e.Graphics.DrawImage(bmpOkBad, targetRectangle, sourceRectangle, GraphicsUnit.Pixel);

            //e.Graphics.FillRectangle(Brushes.Black, 0, 0, 20, 20);            
        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {
            if (validateValue != null)
            {
                
                isValid = validateValue(tbValue.Text);
                if (tbValue.Text.Length > this.MaxLength)
                    isValid = false;
                Rectangle targetRectangle = new Rectangle(this.Size.Width - 20, 0, 20, 20);
                Invalidate(targetRectangle);
            }

            if (stringAutoCorrectionMethod != null)
            {
                string lastValue = tbValue.Text;
                tbValue.Text = stringAutoCorrectionMethod(tbValue.Text);
                tbValue.SelectionStart = tbValue.Text.Length;
            }
        }
    }
}
