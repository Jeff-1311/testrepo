using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;

namespace Trudoyomkost
{
    public partial class LabourCardXtraRep : DevExpress.XtraReports.UI.XtraReport
    {
        public LabourCardXtraRep()
        {
            InitializeComponent();
            xrLbDate.Text = DateTime.Now.ToString();
        }
        public LabourCardXtraRep(bool RepType)
        {
            InitializeComponent();
             
            xrLbDate.Text = DateTime.Now.ToString();
            
            dgOperViewer.Columns[0].Width = 184;
            dgOperViewer.Columns[1].Width = 50;
            dgOperViewer.Columns[2].Width = 120;
            dgOperViewer.Columns[3].Width = 40;
            dgOperViewer.Columns[4].Width = 40;
            dgOperViewer.Columns[5].Width = 35;

            dgOperViewer.Columns[6].Visible = false;
            dgOperViewer.Columns[7].Visible = false;
            dgOperViewer.Columns[8].Visible = false;
                lbItCTNSumC.Visible = false;
                lbPrTimeCTNSumC.Visible = false;
                lbRaiseCoffC.Visible = false;
                lbItCTNSumP.Visible = false;
                lbPrTimeCTNSumP.Visible = false;
                lbRaiseCoffP.Visible = false;
            xrLbDepNum.Visible = false;
            xrLabel9.Visible = false;
            xrLabel13.Text = "Нарядчик";xrLabel14.Text = "Проверил\n Работник ПЭБ";
            xrLabel18.Visible = true;
            if (RepType)
                xrLabel1.Text = "Карта возврата";
            else
            {
                xrLabel1.Text = "Маршрутный лист";
            }
        }

        public void InitializeNormTotals(NormTotal normTotalbyTheJob, NormTotal normTotalbyTheTime)
        {
            lbItCTNSumP.Text = normTotalbyTheTime.ItemCTNSum.ToString();
            lbPrTimeCTNSumP.Text =normTotalbyTheTime.PrTimeCTNSum.ToString();
            lbRaiseCoffP.Text = normTotalbyTheTime.Coeff.ToString();
            lbPrPayNormP.Text = normTotalbyTheTime.PrTimePaySum.ToString();
            lbValP.Text = normTotalbyTheTime.ValuateSum.ToString();
            lbItPayNormP.Text = normTotalbyTheTime.ItemPaySum.ToString();
            lbValPrTimeP.Text = normTotalbyTheTime.ValuatePrTimeSum.ToString();

            lbItCTNSumC.Text  = normTotalbyTheJob.ItemCTNSum.ToString();
            lbPrTimeCTNSumC.Text = normTotalbyTheJob.PrTimeCTNSum.ToString();
            lbRaiseCoffC.Text = normTotalbyTheJob.Coeff.ToString();
            lbPrPayNormC.Text = normTotalbyTheJob.PrTimePaySum.ToString();
            lbValC.Text = normTotalbyTheJob.ValuateSum.ToString();
            lbItPayNormC.Text = normTotalbyTheJob.ItemPaySum.ToString();
            lbValPrTimeC.Text = normTotalbyTheJob.ValuatePrTimeSum.ToString();
 

        }

        public void InitializeHeader(string depNum, string draftNum, string cardNum, string docNum, string execNum)
        {
            xrLbDepNum.Text = depNum;
            xrLbDraftNum.Text = draftNum;
            xrLbCardNum.Text = cardNum;
            xrLbDocNum.Text = docNum;
            xrLbExecNum.Text = execNum;
        }

        private void LabourCardXtraRep_PrintProgress(object sender, DevExpress.XtraPrinting.PrintProgressEventArgs e)
        {
            this.ClosePreview();
        }

       

    
    }
}
