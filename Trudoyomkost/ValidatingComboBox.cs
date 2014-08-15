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
    public partial class ValidatingComboBox : UserControl
    {
        private bool isValid = false;
        private Bitmap bmpOkBad;
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
        public int SelectedIndex
        {
            get
            {
                return insertedCBox.SelectedIndex;
            }
            set
            {
                insertedCBox.SelectedIndex = value;
            }
        }
        public Object SelectedItem
        {
            get
            {
                return insertedCBox.SelectedItem;
            }
            set
            {
                insertedCBox.SelectedItem = value;
            }
        }
        

        public ComboBox.ObjectCollection Items
        {
            get
            {
                return insertedCBox.Items;
            }
        }
        public string Text
        {
            get
            {
                return insertedCBox.Text;
            }
        }
        public AutoCompleteMode AutoCompleteMode
        {
            get
            {
                return insertedCBox.AutoCompleteMode;
            }
            set
            {
                insertedCBox.AutoCompleteMode = value;
            }
        }
        public AutoCompleteSource AutoCompleteSource
        {
            get
            {
                return insertedCBox.AutoCompleteSource;
            }
            set
            {
                insertedCBox.AutoCompleteSource = value;
            }
        }

       public ValidatingComboBox()
        {


            bmpOkBad = Trudoyomkost.Properties.Resources.OK_BAD;
            InitializeComponent();
         
        }
       public ValidatingComboBox(System.ComponentModel.IContainer container)
        {
            
            container.Add(this);
            bmpOkBad = Trudoyomkost.Properties.Resources.OK_BAD;
            InitializeComponent();
            
        }

        private void ValueComboBox_Paint(object sender, PaintEventArgs e)
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

        }

        private void insertedCBox_TextChanged(object sender, EventArgs e)
        {
            if (Items.Contains(Text))
            {
                IsValid = true;
                return;
            }
            IsValid = false;
        }

        private void ValidatingComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (Items.Contains(Text))
            {
                IsValid = true;
                return;
            }
            IsValid = false;
        }

    }
}
