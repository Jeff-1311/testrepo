using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Trudoyomkost
{
    public struct DataGridColumnSetting {
        public DataGridColumnSetting(int columnWidth, string columnName)
        {
            ColumnWidth = columnWidth;
            ColumnName = columnName;
        }

        public int ColumnWidth;
        public string ColumnName;

    }

    public static class UserDataTables
    {
        static Dictionary<string, DataGridColumnSetting> DgColumnSettings = new Dictionary<string, DataGridColumnSetting>()
        {
            {"DetNum", new DataGridColumnSetting(150,"Изделие")},
            {"OperNum", new DataGridColumnSetting(43,"Номер операции")},
            {"ProfCode", new DataGridColumnSetting(43,"Код професи")},
            {"KindPay", new DataGridColumnSetting(35,"В/О")},
            {"NameKindWork", new DataGridColumnSetting(120,"Наименование вида робот")},
            {"WorkerRate", new DataGridColumnSetting(35,"Роз- ряд")},
            {"ItemCTN", new DataGridColumnSetting(60,"РТН шт.")},
            {"PreparTimeCTN", new DataGridColumnSetting(60,"РТН ПЗВ")},
            {"ItemPayNorm", new DataGridColumnSetting(60,"Плат. шт.")},
            {"PreparTimePayNorm", new DataGridColumnSetting(60,"Плат. ПЗВ")},
            {"Valuation", new DataGridColumnSetting(60,"Расценок")},
            {"ValPreparTime", new DataGridColumnSetting(60,"Расц. ПЗВ")},
            {"TaskNumber", new DataGridColumnSetting(70,"№ ЗАДАНИЯ")},
        };

  

        public static DataTable CreateOperAppDt()
        {
            DataTable operAppdt =  new DataTable("OperationApplyTable");
            DataColumn dcProduct = new DataColumn("Изделие", typeof (string));
            DataColumn dcSeriaFrom = new DataColumn("Серия действия норм С", typeof (string));
            DataColumn dcSeriaTo = new DataColumn("Серия действия норм  По", typeof (string));
            operAppdt.Columns.AddRange(new DataColumn[] { dcProduct, dcSeriaFrom, dcSeriaTo });
            return operAppdt;
        }


        public static void CreatetbWhereUse(ref DataTable dtInput)
        {
            dtInput = new DataTable("dtWhereUseInfo");
            DataColumn dcSeriaFrom = new DataColumn("SeriaFrom", typeof(string));
            DataColumn dcSeriaTo = new DataColumn("SeriaTo", typeof(string));
            DataColumn dcProduct = new DataColumn("Product", typeof(string));
            DataColumn dcCountPerProd = new DataColumn("CountPerProd", typeof(int));
            DataColumn dcDepConsumer = new DataColumn("DepConsumer", typeof(int));
            DataColumn dcSeriaFrom_intNum = new DataColumn("SeriaFrom_intNum", typeof(int));
            DataColumn dcSeriaTo_intNum = new DataColumn("SeriaTo_intNum", typeof(int));
            dtInput.Columns.AddRange(new DataColumn[] { dcSeriaFrom, dcSeriaTo, dcProduct, dcCountPerProd, dcDepConsumer, dcSeriaFrom_intNum, dcSeriaTo_intNum });
        

        }

        public static void AddRowToLabourDt(LabourNorm item, DataTable dttemp)
        {
            DataRow rowdttemp = dttemp.NewRow();
            rowdttemp[0] = item.ID;
            rowdttemp[1] = item.InfDetID;
            rowdttemp[2] = item.OperNum;
            rowdttemp[3] = item.DepRegion;
            rowdttemp[8] = item.WorkerRate;
            rowdttemp[6] = item.TariffNetNum;
            rowdttemp[7] = item.KindPay;
            rowdttemp[4] = item.ProfCode;
            rowdttemp[5] = item.NameKindWork;
            rowdttemp[9] = item.ItemCTN;
            rowdttemp[10] = item.PreparTimeCTN;
            rowdttemp[11] = item.ItemPayNorm;
            rowdttemp[12] = item.PreparTimePayNorm;
            rowdttemp[13] = item.Valuation;
            rowdttemp[14] = item.ValPreparTime;
            rowdttemp[15] = item.CoeffCTN;
            rowdttemp[16] = item.DocNum;
            dttemp.Rows.Add(rowdttemp);
        }

        public static void AddRowToApplyDt(string prodNum, string seriaFrom, string seriaTo, DataTable dtInput)
        {
            DataRow rowdttemp = dtInput.NewRow();
            rowdttemp[0] = prodNum;
            rowdttemp[1] = seriaFrom;
            rowdttemp[2] = seriaTo;
            dtInput.Rows.Add(rowdttemp);
        }


        public static void SetDgColumns(DataGridView dginput)
        {
            DataGridColumnSetting tempSetting;
            foreach (DataGridViewColumn row in dginput.Columns)
            {
                if (DgColumnSettings.ContainsKey(row.Name))
                {
                    tempSetting = DgColumnSettings[row.Name];
                    row.HeaderText = tempSetting.ColumnName;
                    row.Width = tempSetting.ColumnWidth;
                }
            }
          
        }


    }

   
}
