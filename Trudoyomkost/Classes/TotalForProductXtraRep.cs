using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Data;

namespace Trudoyomkost
{
    public partial class TotalForProductXtraRep : DevExpress.XtraReports.UI.XtraReport
    {
        public TotalForProductXtraRep()
        {
            InitializeComponent();
           
        }
        public TotalForProductXtraRep(DataTable dtinput, DataGridView dginput)
        {
            InitializeComponent();
            
         
            //this.dgTotalForProduct.DataSource = dtinput;
            // UserDataTables.SetDgColumns(dgTotalForProduct);
            // this.dgTotalForProduct.Width = dginput.Width;
            //this.dgTotalForProduct.Height = dginput.Rows.Count * 23;
        }

    }
}
