using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trudoyomkost
{
    public partial class FormOkTariff : Form
    {
        private dobavlenieNorm _dobavlenieNorm;

        public FormOkTariff(dobavlenieNorm dobavlenieNorm)
        {
            InitializeComponent();
            _dobavlenieNorm = dobavlenieNorm;
        }

        private void buttonForNewTariff_Click(object sender, EventArgs e)
        {
           
            _dobavlenieNorm.addItemList(textBoxForTariff.Text);
            this.Close();
           
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       


    }
}
