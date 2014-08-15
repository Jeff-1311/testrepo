using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Trudoyomkost
{

    partial class LabourNorm : IComparable<LabourNorm>
    {
        public LabourNorm(int id, int infDetId, string operNum, double depRegion, int profCode, string nameKindWork,
        byte tariffNetNum, string kindPay, double workerRate, double itemCtn, double preparTimeCtn, double itemPayNorm,
double preparTimePayNorm, double valuation, double? valPreparTime, double coeffCtn, string docNum, System.DateTime date,string taskNum )
        {
            _ID = id;
            _InfDetID = infDetId;
            _OperNum = operNum;
            _DepRegion = depRegion;
            _ProfCode = profCode;
            _NameKindWork = nameKindWork;
            _TariffNetNum = tariffNetNum;
            _KindPay = kindPay;
            _WorkerRate = workerRate;
            _ItemCTN = itemCtn;
            _PreparTimeCTN = preparTimeCtn;
            _ItemPayNorm = itemPayNorm;
            _PreparTimePayNorm = preparTimePayNorm;
            _Valuation = valuation;
            _ValPreparTime = valPreparTime;
            _CoeffCTN = coeffCtn;
            _DocNum = docNum;
            _Date = date;
            _TaskNumber = taskNum;
        }

        int IComparable<LabourNorm>.CompareTo(LabourNorm other)
        {
            int sumOther = other.ID;
            int sumThis = this.ID;

            if (sumOther > sumThis)
                return -1;
            else if (sumOther == sumThis)
                return 0;
            else
                return 1;
        }

        public static LabourNorm FillLabourNormFromDg(DataGridView dgNorm, int currentRow, int infDetID)
        {
            LabourNorm newlaborNorm = new LabourNorm();

            newlaborNorm.ID = 0;
            newlaborNorm.InfDetID = infDetID;
            newlaborNorm.OperNum = dgNorm.Rows[currentRow].Cells[2].Value.ToString();
            newlaborNorm.DepRegion = (double)dgNorm.Rows[currentRow].Cells[3].Value;
            newlaborNorm.ProfCode = (int)dgNorm.Rows[currentRow].Cells[4].Value;
            newlaborNorm.NameKindWork = dgNorm.Rows[currentRow].Cells[5].Value.ToString();
            newlaborNorm.KindPay = dgNorm.Rows[currentRow].Cells[7].Value.ToString();
            newlaborNorm.WorkerRate = (double)dgNorm.Rows[currentRow].Cells[8].Value;
            newlaborNorm.ItemCTN = (double)dgNorm.Rows[currentRow].Cells[9].Value;
            newlaborNorm.PreparTimeCTN = (double)dgNorm.Rows[currentRow].Cells[10].Value;
            newlaborNorm.ItemPayNorm = (double)dgNorm.Rows[currentRow].Cells[11].Value;
            newlaborNorm.PreparTimePayNorm = (double)dgNorm.Rows[currentRow].Cells[12].Value;
            newlaborNorm.CoeffCTN = (double)dgNorm.Rows[currentRow].Cells[15].Value;
            newlaborNorm.DocNum = dgNorm.Rows[currentRow].Cells[16].Value.ToString();
            newlaborNorm.TaskNumber = dgNorm.Rows[currentRow].Cells[18].Value.ToString();
            return newlaborNorm;
        }

        public LabourNorm Copy()
        {
            return new LabourNorm(this.ID, this.InfDetID, this.OperNum, this.DepRegion, this.ProfCode,
                                     this.NameKindWork, this.TariffNetNum, this.KindPay, this.WorkerRate, this.ItemCTN,
                                     this.PreparTimeCTN, this.ItemPayNorm, this.PreparTimePayNorm,
                                      this.Valuation, this.ValPreparTime, this.CoeffCTN, this.DocNum, DateTime.Now, this.TaskNumber);
        }
      
    }
     partial class WhereOperationUse
    {
        public WhereOperationUse(int LabourNormID, int SeriaFrom, int SeriaTo, short InfProductsChipher)
        {
            this.LabourNormID = LabourNormID;
            this.SeriaFrom = SeriaFrom;
            this.SeriaTo = SeriaTo;
            this.InfProductsChipher = InfProductsChipher;
        }
        public WhereOperationUse(int LabourNormID, int SeriaFrom, int SeriaTo, short InfProductsChipher, int Id)
        {
            this.LabourNormID = LabourNormID;
            this.SeriaFrom = SeriaFrom;
            this.SeriaTo = SeriaTo;
            this.InfProductsChipher = InfProductsChipher;
            this.Id = Id;
        }
    }
}
