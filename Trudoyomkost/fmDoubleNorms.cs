using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ErikEJ.SqlCe;
namespace Trudoyomkost
{
    public partial class fmDoubleNorms : Form
    {
        
        private DataGridView _dgOperViewer;
        private int _destinationDetID;
        private int _sourceDetID;
        public fmDoubleNorms(fmNormsViewer fm)
        {
         _sourceDetID = fm._infDetID;
         _dgOperViewer = fm.DgOperViewer;
            InitializeComponent();
            InitilizeFormElemets();
        }

        private void InitilizeFormElemets()
        {
            cbDetNum.DataSource = FillTrudoyomkostDB.DicDetNumAndId.Keys.ToList();
                cbDetNum.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbDetNum.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cbDetNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            _destinationDetID = FillTrudoyomkostDB.DicDetNumAndId[cbDetNum.SelectedItem.ToString()];
        }

        


        private void btDoubleExec_Click(object sender, EventArgs e)
        {
            AddDoubleItems();
        }


        public void AddDoubleItems()
        {
            this.Enabled = false;



            var labourNormlist = LinqQueryForTrudoyomkost.FillLabournNormForDet(_destinationDetID);

            foreach (var item in labourNormlist)
            {
                 var whereOperUselstdel = LinqQueryForTrudoyomkost.FillWhereOperationUseList(item.ID);

                 if (whereOperUselstdel.Count() > 0)
                 {
                     foreach (var applydel in whereOperUselstdel)
                     {
                         FillTrudoyomkostDB.WhereOperationUseList.Remove(applydel);
                     }
                 }

                 FillTrudoyomkostDB.LabourNormTableAdapter.DeleteQuery(item.ID);
                 FillTrudoyomkostDB.WhereOperationUseTableAdapter.DeleteQuery(item.ID);
                 FillTrudoyomkostDB.LabourNormList.Remove(item);

            }

            labourNormlist = LinqQueryForTrudoyomkost.FillLabournNormForDet(_sourceDetID);


                    using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
                    {



                        int Id = 0;
                        int whereOperUseId = FillTrudoyomkostDB.WhereOperationUseList.Last().Id;
                        Id = currentContext.LabourNorm.ToList().Max().ID;
                        foreach (var item in labourNormlist)
                        {
                           
                            var whereOperUselst = LinqQueryForTrudoyomkost.FillWhereOperationUseList(item.ID);
                            if (whereOperUselst.Count() > 0)
                            {
                                Id++;
                                //FillTrudoyomkostDB.LabourNormTableAdapter.InsertQuery(_destinationDetID, item.OperNum, item.DepRegion, item.ProfCode, item.NameKindWork,
                                // item.TariffNetNum, item.KindPay, item.WorkerRate, item.ItemCTN, item.PreparTimeCTN,
                                //item.ItemPayNorm, item.PreparTimePayNorm, item.Valuation, item.ValPreparTime, item.CoeffCTN, item.DocNum, item.Date, item.TaskNumber);

                                FillTrudoyomkostDB.FilltmpLabourNormRow(new LabourNorm(Id, _destinationDetID, item.OperNum, item.DepRegion, item.ProfCode,
                                 item.NameKindWork, item.TariffNetNum, item.KindPay, item.WorkerRate, item.ItemCTN, item.PreparTimeCTN, item.ItemPayNorm, item.PreparTimePayNorm,
                                  item.Valuation, item.ValPreparTime, item.CoeffCTN, item.DocNum, DateTime.Now, item.TaskNumber));

                                FillTrudoyomkostDB.LabourNormList.Add(new LabourNorm(Id, _destinationDetID, item.OperNum, item.DepRegion, item.ProfCode,
                                item.NameKindWork, item.TariffNetNum, item.KindPay, item.WorkerRate, item.ItemCTN, item.PreparTimeCTN, item.ItemPayNorm, item.PreparTimePayNorm,
                                item.Valuation, item.ValPreparTime, item.CoeffCTN, item.DocNum, DateTime.Now, item.TaskNumber));


                                //currentContext.LabourNorm.InsertOnSubmit(new LabourNorm(Id, _destinationDetID, item.OperNum, item.DepRegion, item.ProfCode,
                                //  item.NameKindWork, item.TariffNetNum, item.KindPay, item.WorkerRate, item.ItemCTN, item.PreparTimeCTN, item.ItemPayNorm, item.PreparTimePayNorm,
                                //  item.Valuation, item.ValPreparTime, item.CoeffCTN, item.DocNum, DateTime.Now, item.TaskNumber));

                                
                                
                                foreach (var applyItem in whereOperUselst)
                                {
                                    
                                    whereOperUseId++;
                                  
                                    FillTrudoyomkostDB.FilltmpWhereOperUseRow(new WhereOperationUse(Id, applyItem.SeriaFrom, applyItem.SeriaTo, applyItem.InfProductsChipher));
                                    FillTrudoyomkostDB.WhereOperationUseList.Add(new WhereOperationUse(Id, applyItem.SeriaFrom, applyItem.SeriaTo, applyItem.InfProductsChipher, whereOperUseId));
                                    //currentContext.WhereOperationUse.InsertOnSubmit(new WhereOperationUse(Id, applyItem.SeriaFrom, applyItem.SeriaTo, applyItem.InfProductsChipher,whereOperUseId));

                                    //FillTrudoyomkostDB.WhereOperationUseTableAdapter.InsertQuery(Id, applyItem.SeriaFrom, applyItem.SeriaTo, applyItem.InfProductsChipher);
                                }
                                
                            }
                            //currentContext.SubmitChanges();

                        }
                        DataTable tmpWhereOperUse = FillTrudoyomkostDB.WhereOperationUseDataTable;
                        DataTable tmpLabourNorm = FillTrudoyomkostDB.LabourNormDataTable;

                        SqlCeBulkCopy bulkInsert = new SqlCeBulkCopy(Properties.Settings.Default.TrudoyomkostDBConnectionString);
                        if (tmpWhereOperUse.Rows.Count > 0)
                        {
                            bulkInsert.DestinationTableName = "whereOperationUse";
                            bulkInsert.WriteToServer(tmpWhereOperUse);
                        }
                        if (tmpLabourNorm.Rows.Count > 0)
                        {
                            bulkInsert.DestinationTableName = "LabourNorm";
                            bulkInsert.WriteToServer(tmpLabourNorm);
                        }
                        FillTrudoyomkostDB.LabourNormDataTable.Clear();
                        FillTrudoyomkostDB.WhereOperationUseDataTable.Clear();
                        bulkInsert.Close();
                    }
           
          this.Enabled = true;
        }
    }
}
