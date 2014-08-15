using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



namespace Trudoyomkost
{
    #region InfTariffInfo Struct
    public struct InfTariffInfo
    {
        private byte _tariffNetNum;
        private string _KindPay;
        private double _workerRate;

        public InfTariffInfo(byte tariiffNetNum, string kindPay, double workerRate)
        {
            _tariffNetNum = tariiffNetNum;
            _KindPay = kindPay;
            _workerRate = workerRate;
        }

    }
    #endregion
    #region WhereOperUseStruct Struct 
    public struct WhereOperUseStruct
    {
        private int _labourNormId;
        private int _seriaFrom;
        private int _seriaTo;
        private short _productChipher;

        public WhereOperUseStruct(int labourNormId, int seriaFrom, int seriaTo, short productChipher)
        {
            _labourNormId = labourNormId;
            _seriaFrom = seriaFrom;
            _seriaTo = seriaTo;
            _productChipher = productChipher;
        }
        public int LabourNormId
        {
            get
            {
                return _labourNormId;
            }

        }
        public int SeriaFrom
        {
            get
            {
                return _seriaFrom;
            }

        }
        public int SeriaTo
        {
            get
            {
                return _seriaTo;
            }

        }
        public short ProductChipher
        {
            get
            {
                return _productChipher;
            }
        }
    }
    #endregion

    #region MaxApply Struct
    public struct MaxApply 
    {
        private int _seriaFrom;
        private int _seriaTo;
        private string _productName;
        private int _countPerProd;


        public MaxApply(int maxSeriaFrom, int maxSeriaTo, string productName, int countPerProd)
        {
            _seriaFrom = maxSeriaFrom;
            _seriaTo = maxSeriaTo;
            _productName = productName;
            _countPerProd = countPerProd;
        }

        public int SeriaFrom
        {
            get { return _seriaFrom; }
        }
        public int SeriaTo
        {
            get { return _seriaTo; }
        }
        public string ProductName
        {
            get { return _productName; }
        }

        public int CountPerProd
        {
            get { return _countPerProd; }
        }
    }
    #endregion

    #region ShortProfInfo Struct
    public struct ShortProfInfo
    {
        private int _profCode;
        private string _nameKindWork;

        public ShortProfInfo(int ProfCode, string NameKindWork)
        {
            _profCode = ProfCode;
            _nameKindWork = NameKindWork;
        }

        public int ProfCode
        {
            get
            {
                return _profCode;
            }
        }

        public string NameKindWork
        {
            get
            {
                return _nameKindWork;
            }
        }
    }
    #endregion
    #region NormTotal Struct
    public struct NormTotal
    {

        #region NormTotalProperties
        private double _itemCTNSum;

        public double ItemCTNSum
        {
            get { return _itemCTNSum; }
            set { _itemCTNSum = value; }
        }
        private double _prTimeCTNSum;

        public double PrTimeCTNSum
        {
            get { return _prTimeCTNSum; }
            set { _prTimeCTNSum = value; }
        }
        private double _itemPaySum;

        public double ItemPaySum
        {
            get { return _itemPaySum; }
            set { _itemPaySum = value; }
        }
        private double _prTimePaySum;

        public double PrTimePaySum
        {
            get { return _prTimePaySum; }
            set { _prTimePaySum = value; }
        }
        private double _valuateSum;

        public double ValuateSum
        {
            get { return _valuateSum; }
            set { _valuateSum = value; }
        }
       
        private double? _valuatePrTimeSum;

        public double? ValuatePrTimeSum
        {
            get { return _valuatePrTimeSum; }
            set { _valuatePrTimeSum = value; }
        }
        private double _coeff;

        public double Coeff
        {
            get { return _coeff; }
        }
        #endregion
        public void ReCalcInMinutes(bool IstimeInHours)
        {
            if (!IstimeInHours)
            {
                _itemCTNSum = ItemCTNSum * 60;
                _prTimeCTNSum = PrTimeCTNSum * 60;
                _itemPaySum = ItemPaySum * 60;
                _prTimePaySum = PrTimePaySum * 60;
            }
        }
             

        public void SetCoeff()
        {
            _coeff = 0;
            if (_itemCTNSum != 0 && _itemPaySum != 0)
                _coeff = Math.Round((_itemPaySum/_itemCTNSum ), 4);
        }
        public void Clear()
        {
            _itemCTNSum = 0;
            _prTimeCTNSum = 0;
            _itemPaySum = 0;
            _valuateSum = 0;
            _prTimePaySum = 0;
            _valuatePrTimeSum = 0;

        }

        public void CalcTotal(double itemCtn, double preparTimeCtn, double itemPayNorm, double prepareTimePay,  double valuation , double valuationPreparTime)
        {
            this.ItemCTNSum += itemCtn;
            this.PrTimeCTNSum += preparTimeCtn;
            this.ItemPaySum += itemPayNorm;
            this.PrTimePaySum += prepareTimePay;
            this.ValuateSum += valuation;
            if (this.ValuatePrTimeSum == null)
                this.ValuatePrTimeSum = 0;
            this.ValuatePrTimeSum += valuationPreparTime;
        }
        public void SetValueIntoDg(DataGridView dgInput)
        {
            dgInput.Rows.Add();
            int lastrow = dgInput.RowCount - 1;
            
            
            foreach (DataGridViewColumn column in dgInput.Columns)
            {

                string columnName = column.Name;
                switch (columnName)
                {
                    case "ItemCTNSum":
                        dgInput.Rows[lastrow].Cells[column.Name].Value = ItemCTNSum;
                        break;
                    case "PrTimeCTNSum":
                        dgInput.Rows[lastrow].Cells[column.Name].Value = PrTimeCTNSum;
                        break;
                    case "ItemPaySum" :
                        dgInput.Rows[lastrow].Cells[column.Name].Value = ItemPaySum;
                        break;
                    case "PrTimePaySum" :
                          dgInput.Rows[lastrow].Cells[column.Name].Value = PrTimePaySum;
                        break;
                    case "ValuateSum":
                        dgInput.Rows[lastrow].Cells[column.Name].Value = ValuateSum;
                        break;
                    case "ValuatePrTimeSum":
                        dgInput.Rows[lastrow].Cells[column.Name].Value = ValuatePrTimeSum;
                        break;
                    case "Coeff":
                     dgInput.Rows[lastrow].Cells[column.Name].Value = Coeff;
                        break;
                }

             

                

            }
        }
    }
  #endregion

    #region Trudoyomkost Settings
    public static class TrudoyomkostSettings
   {

       public static bool IsAggregateDep {
           get { return Properties.Settings.Default.IsAggregateDep; }

           set { Properties.Settings.Default.IsAggregateDep = value;
           Properties.Settings.Default.Save();
           }
        
       }

       public static int RoundNum
       {
           get {
               return Properties.Settings.Default.RoundNum;
               }
       }
       public static void set_RoundNumExtracted(string inputstr)
       {
           int num;
           int.TryParse(inputstr, out num);
           Properties.Settings.Default.RoundNum = num;
           Properties.Settings.Default.Save();
       }

       public static int DepNum
       {
           get
           {
               return Properties.Settings.Default.DepNum;
           }
       }
       public static void set_DepNumExtracted(string inputstr)
       {
           int num;
           int.TryParse(inputstr, out num);
           Properties.Settings.Default.DepNum = num;
           Properties.Settings.Default.Save();
       }
       public static byte TariffNetNum {
           get { return Properties.Settings.Default.TariffNetNum; }

           set { 
               if(value.GetType()== typeof(Byte))
               Properties.Settings.Default.TariffNetNum = value;
               Properties.Settings.Default.Save();
           }
       }

       public static string MdbFileDir
       {
           get { return Properties.Settings.Default.MdbFileDir; }

           set {  
               Properties.Settings.Default.MdbFileDir = value;
               Properties.Settings.Default.Save();
               }

       }

        
       
   }
#endregion


    public struct TotalForSeria
    {
        public int DetCount;          // количество наименований деталей
        public int TotalDetCount;     // количество  деталей
        public int NormalCount;       // количество наименований нормалей
        public int TotalNormalCount;  // количество нормалей
        public int AssemblyCount;     // количество наименований cборок
        public int TotalAssemblyCount;// количество  cборок
        public int AssembNormalCount;      // количество наименований зборочных нормалей
        public int TotalAssembNormalCount; // количество  cборочных нормалей
        public int PayNormAmount;          // количество платежных норм 
        public int CTNormAmount;           // количество расчетных норм
       
    }


    public struct TotalCalculNormReport
    {
        
    }


    
            

}
      
                 
