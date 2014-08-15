using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ErikEJ.SqlCe;
using Trudoyomkost.TrudoyomkostDBDataSetTableAdapters;
using Wintellect.PowerCollections;




namespace Trudoyomkost
{
    internal static class FillTrudoyomkostDB
    {
        private static String connString = Properties.Settings.Default.TrudoyomkostDBConnectionString;
        private static Dictionary<string, int> DicCheckRepeatTN = new Dictionary<string, int>();
        private static IList<string> ListAllLabourNorm = new List<string>();


        public static ODLDB210410DataSetTableAdapters.TN148TableAdapter Tn148TableAdapter;
        public static ODLDB210410DataSetTableAdapters.TN158TableAdapter Tn158TableAdapter;
        public static ODLDB210410DataSetTableAdapters.TN70TableAdapter Tn70TableAdapter;
        public static ODLDB210410DataSetTableAdapters.TARIFTableAdapter TarifTableAdapter;

        public static ODLDB210410DataSet.TN148DataTable Tn148DataTable;
        public static ODLDB210410DataSet.TN158DataTable Tn158DataTable;
        public static ODLDB210410DataSet.TN70DataTable Tn70DataTable;
        public static ODLDB210410DataSet.TARIFDataTable TarifDataTable;


        private static TrudoyomkostDBDataSetTableAdapters.whereUseTableAdapter _whereUseTableAdapter =
        new whereUseTableAdapter();

        public static TrudoyomkostDBDataSetTableAdapters.infDepListTableAdapter InfDepListTableAdapter = new infDepListTableAdapter();
        public static TrudoyomkostDBDataSetTableAdapters.infDetTableAdapter InfDetTableAdapter = new infDetTableAdapter();
        public static TrudoyomkostDBDataSetTableAdapters.LabourNormTableAdapter LabourNormTableAdapter = new LabourNormTableAdapter();
        public static TrudoyomkostDBDataSetTableAdapters.whereOperationUseTableAdapter WhereOperationUseTableAdapter =
        new whereOperationUseTableAdapter();
        public static TrudoyomkostDBDataSetTableAdapters.infProfessionTableAdapter InfProfessionTableAdapter = new infProfessionTableAdapter();
        public static TrudoyomkostDBDataSetTableAdapters.infProductsTableAdapter InfProductsTableAdapter = new infProductsTableAdapter();
        public static TrudoyomkostDBDataSetTableAdapters.whereUseTableAdapter WhereUseTableAdapter = new whereUseTableAdapter();



        public static Dictionary<string, MaxApply> DcMaxApply = new Dictionary<string, MaxApply>();
        public static Dictionary<string, int> DicDetNumAndId = new Dictionary<string, int>();
        public static Dictionary<int, int> DicDepCodeAndId = new Dictionary<int, int>();
        public static Dictionary<string, int> DictInfProfession = new Dictionary<string, int>();

        public static Dictionary<string, short> DcInfProducts = new Dictionary<string, short>();

        public static BigList<LabourNorm> LabourNormList = new BigList<LabourNorm>();
        public static BigList<WhereOperationUse> WhereOperationUseList = new BigList<WhereOperationUse>();
        public static List<LabourNorm> CurrentLabourNormList = new List<LabourNorm>();
        public static List<InfProducts> infProductList = new List<InfProducts>();
        public static List<WhereUse> whereUseList = new List<WhereUse>();
        public static List<InfDet> infDetList = new List<InfDet>();
        public static List<byte> tariffList = new List<byte>();

        public static TrudoyomkostDBDataSet.infTariffDataTable InfTariffDataTable = new TrudoyomkostDBDataSet.infTariffDataTable();
        public static TrudoyomkostDBDataSet.infDetDataTable InfDetDataTable = new TrudoyomkostDBDataSet.infDetDataTable();
        public static TrudoyomkostDBDataSet.whereUseDataTable WhereUseDataTable = new TrudoyomkostDBDataSet.whereUseDataTable();
        public static TrudoyomkostDBDataSet.infDepListDataTable InfDepListDataTable = new TrudoyomkostDBDataSet.infDepListDataTable();

        public static TrudoyomkostDBDataSet.whereOperationUseDataTable WhereOperationUseDataTable = new TrudoyomkostDBDataSet.whereOperationUseDataTable();
        public static TrudoyomkostDBDataSet.LabourNormDataTable LabourNormDataTable = new TrudoyomkostDBDataSet.LabourNormDataTable();

        public static string NormMapNumber;






        #region Insert From TN Tables
        public static void InsertFromTNTables()
        {
            InfDetDataTable.Clear();
            using (var newLocalDb = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                LinqQueryForTrudoyomkost.FillDictDetNumID(newLocalDb, ref DicDetNumAndId);
                LinqQueryForTrudoyomkost.FillDictDepIDCode(newLocalDb, ref DicDepCodeAndId);
                LinqQueryForTrudoyomkost.FillDictInfProfession(newLocalDb, ref DictInfProfession);
            }


            int whereOperUseId = 1;

            foreach (var item in Tn148DataTable)
            {
                if (DicDetNumAndId.ContainsKey(item.NDET))
                {
                    LabourNorm itemlabourNorm = FillItemLabourNorm(item);
                    itemlabourNorm.ID = whereOperUseId;
                    FilltmpLabourNormRow(itemlabourNorm);

                    WhereOperationUse itemWhereOperUse = FillItemWhereOperationUse(item);
                    itemWhereOperUse.LabourNormID = whereOperUseId;
                    FilltmpWhereOperUseRow(itemWhereOperUse);

                    ListAllLabourNorm.Add(item.NDET);
                    whereOperUseId++;
                }
            }

            using (var newLocalDb = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                LinqQueryForTrudoyomkost.FillDictDetNumID(newLocalDb, ref DicDetNumAndId);
            }
            DicCheckRepeatTN.Clear();
            foreach (var item in Tn158DataTable)
            {
                if (DicDetNumAndId.ContainsKey(item.NDET))
                {
                    LabourNorm itemlabourNorm = FillItemLabourNorm(item);
                    itemlabourNorm.ID = whereOperUseId;
                    FilltmpLabourNormRow(itemlabourNorm);

                    WhereOperationUse itemWhereOperUse = FillItemWhereOperationUse(item);
                    itemWhereOperUse.LabourNormID = whereOperUseId;
                    FilltmpWhereOperUseRow(itemWhereOperUse);

                    ListAllLabourNorm.Add(item.NDET);
                    whereOperUseId++;
                }
            }
            Properties.Settings.Default.Save();
            SqlCeBulkCopy bulkInsert = new SqlCeBulkCopy(connString);
            DataTable tmptbLabourNorm = LabourNormDataTable;
            DataTable tmpWhereOperUse = WhereOperationUseDataTable;

            if (tmptbLabourNorm.Rows.Count > 0)
            {
                bulkInsert.DestinationTableName = "LabourNorm";
                bulkInsert.WriteToServer(tmptbLabourNorm);
            }
            if (tmpWhereOperUse.Rows.Count > 0)
            {
                bulkInsert.DestinationTableName = "whereOperationUse";
                bulkInsert.WriteToServer(tmpWhereOperUse);
            }
            LabourNormDataTable.Clear();
            WhereOperationUseDataTable.Clear();
            bulkInsert.Close();
        }
        #endregion

        #region Insert From AN Tables
        public static void InsertFromANTables(DataSet tableList)
        {

            using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                LinqQueryForTrudoyomkost.FilldcInfProducts(currentContext, ref DcInfProducts);
                LinqQueryForTrudoyomkost.FillDictDetNumID(currentContext, ref DicDetNumAndId);
                LinqQueryForTrudoyomkost.FillDictDepIDCode(currentContext, ref DicDepCodeAndId);

                Properties.Settings.Default.DetID = currentContext.InfDet.ToList().Count == 0 ? 0 : ++currentContext.InfDet.ToList().Last().ID;

            }

            if (int.Parse((tableList.Tables[0].Rows[0].Field<string>("CEH").Trim())) != Properties.Settings.Default.DepNum)
                return;

            _whereUseTableAdapter.DeleteAllQuery();
            foreach (DataTable itemTable in tableList.Tables)
            {
                foreach (DataRow itemRow in itemTable.Rows)
                {
                    string NDET = (string)itemRow["NDET"];
                    if (!DicDetNumAndId.ContainsKey(NDET))
                    {
                        var infDetItem = FillItemInfDet(itemRow);
                        FilltmpInfDetRow(ref infDetItem);

                        DicDetNumAndId.Add(NDET, Properties.Settings.Default.DetID);
                        Properties.Settings.Default.DetID++;
                    }
                    var itemWhereuse = FillItemWhereUse(itemRow);
                    FilltmpWhereUseRow(itemWhereuse);
                }
            }
            Properties.Settings.Default.Save();
            SqlCeBulkCopy bulkInsert = new SqlCeBulkCopy(connString);
            DataTable tmptbInfDet = InfDetDataTable;
            DataTable tmptbWhereUse = WhereUseDataTable;
            if (tmptbInfDet.Rows.Count > 0)
            {
                bulkInsert.DestinationTableName = "infDet";
                bulkInsert.WriteToServer(tmptbInfDet);

            }
            if (tmptbWhereUse.Rows.Count > 0)
            {
                bulkInsert.DestinationTableName = "whereUse";
                bulkInsert.WriteToServer(tmptbWhereUse);
            }

            WhereUseDataTable.Clear();
            InfDetDataTable.Clear();
            bulkInsert.Close();

        }
        #endregion

        #region Insert From TARIF Table
        public static void InsertFromTARIF()
        {
            foreach (var item in TarifDataTable)
            {
                InfTariff infTariff = FillInfTarifItem(item);
                FilltmpInfTarifDetRow(ref infTariff);
            }

            SqlCeBulkCopy bulkInsert = new SqlCeBulkCopy(connString);
            DataTable tmpInfTariff = InfTariffDataTable;
            if (tmpInfTariff.Rows.Count > 0)
            {
                bulkInsert.DestinationTableName = "infTariff";
                bulkInsert.WriteToServer(tmpInfTariff);
            }
            bulkInsert.Close();
            bulkInsert.Dispose();
            tmpInfTariff.Dispose();
        }
        #endregion



        private static void FilltmpInfTarifDetRow(ref InfTariff infTariff)
        {
            DataRow rowTempInfDet = InfTariffDataTable.NewRow();
            rowTempInfDet[0] = infTariff.TariffNetNum;
            rowTempInfDet[1] = infTariff.KindPay;
            rowTempInfDet[2] = infTariff.WorkerRate;
            rowTempInfDet[3] = infTariff.HourCost;
            InfTariffDataTable.Rows.Add(rowTempInfDet);
        }
        private static void FilltmpInfDetRow(ref InfDet itemInfDet)
        {
            DataRow rowTempInDet = InfDetDataTable.NewRow();
            rowTempInDet[0] = Properties.Settings.Default.DetID;
            rowTempInDet[1] = itemInfDet.DetNum;
            rowTempInDet[2] = itemInfDet.PKP;
            rowTempInDet[3] = itemInfDet.DepProducer;
            rowTempInDet[4] = string.Empty;
            rowTempInDet[5] = itemInfDet.IndicOSPK;
            rowTempInDet[6] = itemInfDet.DocNum;
            rowTempInDet[7] = itemInfDet.DepNative;
            rowTempInDet[8] = 1;
            rowTempInDet[9] = DateTime.Now;
            InfDetDataTable.Rows.Add(rowTempInDet);
        }
        public static void FilltmpInfDetRow(InfDet itemInfDet)
        {
            DataRow rowTempInDet = InfDetDataTable.NewRow();
            rowTempInDet[0] = itemInfDet.ID;
            rowTempInDet[1] = itemInfDet.DetNum;
            rowTempInDet[2] = itemInfDet.PKP;
            rowTempInDet[3] = itemInfDet.DepProducer;
            rowTempInDet[4] = string.Empty;
            rowTempInDet[5] = itemInfDet.IndicOSPK;
            rowTempInDet[6] = itemInfDet.DocNum;
            rowTempInDet[7] = itemInfDet.DepNative;
            rowTempInDet[8] = 1;
            rowTempInDet[9] = DateTime.Now;
            InfDetDataTable.Rows.Add(rowTempInDet);
        }

        public static void FilltmpWhereUseRow(WhereUse itemWhereuse)
        {
            DataRow rowTempWhereUse = WhereUseDataTable.NewRow();
            rowTempWhereUse[0] = itemWhereuse.InfDetID;
            rowTempWhereUse[1] = itemWhereuse.SeriaFrom;
            rowTempWhereUse[2] = itemWhereuse.SeriaTo;
            rowTempWhereUse[3] = itemWhereuse.DepConsumer;
            rowTempWhereUse[4] = itemWhereuse.DepThermal1;
            rowTempWhereUse[5] = itemWhereuse.DepThermal2;
            rowTempWhereUse[6] = itemWhereuse.DepPainting1;
            rowTempWhereUse[7] = itemWhereuse.DepPainting2;
            rowTempWhereUse[8] = itemWhereuse.DepPainting3;
            rowTempWhereUse[9] = itemWhereuse.CountPerProduct;
            rowTempWhereUse[10] = itemWhereuse.InfProductsCipher;
            WhereUseDataTable.Rows.Add(rowTempWhereUse);

        }

        public static void FilltmpLabourNormRow(LabourNorm itemLabourNorm)
        {
            DataRow rowTempLabourNorm = LabourNormDataTable.NewRow();
            rowTempLabourNorm[0] = itemLabourNorm.ID;
            rowTempLabourNorm[1] = itemLabourNorm.InfDetID;
            rowTempLabourNorm[2] = itemLabourNorm.OperNum;
            rowTempLabourNorm[3] = itemLabourNorm.DepRegion;
            rowTempLabourNorm[4] = itemLabourNorm.ProfCode;
            rowTempLabourNorm[5] = itemLabourNorm.NameKindWork;
            rowTempLabourNorm[6] = itemLabourNorm.TariffNetNum;
            rowTempLabourNorm[7] = itemLabourNorm.KindPay;
            rowTempLabourNorm[8] = itemLabourNorm.WorkerRate;
            rowTempLabourNorm[9] = itemLabourNorm.ItemCTN;
            rowTempLabourNorm[10] = itemLabourNorm.PreparTimeCTN;
            rowTempLabourNorm[11] = itemLabourNorm.ItemPayNorm;
            rowTempLabourNorm[12] = itemLabourNorm.PreparTimePayNorm;
            rowTempLabourNorm[13] = itemLabourNorm.Valuation;
            rowTempLabourNorm[14] = itemLabourNorm.ValPreparTime;
            rowTempLabourNorm[15] = itemLabourNorm.CoeffCTN;
            rowTempLabourNorm[16] = itemLabourNorm.DocNum;
            rowTempLabourNorm[17] = itemLabourNorm.Date;
            rowTempLabourNorm[18] = itemLabourNorm.TaskNumber;
            LabourNormDataTable.Rows.Add(rowTempLabourNorm);
        }

        public static void FilltmpWhereOperUseRow(WhereOperationUse itemWhereOperUse)
        {
            DataRow rowTempWhereOperUse = WhereOperationUseDataTable.NewRow();
            rowTempWhereOperUse[0] = itemWhereOperUse.LabourNormID;
            rowTempWhereOperUse[1] = itemWhereOperUse.SeriaFrom;
            rowTempWhereOperUse[2] = itemWhereOperUse.SeriaTo;
            rowTempWhereOperUse[3] = itemWhereOperUse.InfProductsChipher;
            WhereOperationUseDataTable.Rows.Add(rowTempWhereOperUse);
        }

        public static InfDet FillItemInfDet(DataRow inputRow)
        {
            byte i = 0;

            InfDet infDetItem = new InfDet();

            infDetItem.IndicOSPK = (typeof(DBNull) == inputRow["PR_OSPK"].GetType()) ? i : byte.Parse((string)inputRow["PR_OSPK"]);
            infDetItem.DetNum = (typeof(DBNull) == inputRow["NDET"].GetType()) ? "" : (string)inputRow["NDET"];
            infDetItem.PKP = (typeof(DBNull) == inputRow["PKP"].GetType()) ? "" : ((string)inputRow["PKP"]).Trim();
            infDetItem.DocNum = (typeof(DBNull) == inputRow["NDOK"].GetType()) ? "-" : (string)inputRow["NDOK"];
            infDetItem.DepProducer = (typeof(DBNull) == inputRow["CEH"].GetType()) ? 0 : DicDepCodeAndId[int.Parse(((string)inputRow["CEH"]).Trim())];
            infDetItem.DepNative = (typeof(DBNull) == inputRow["DUBL_CEH"].GetType()) ? 0 : DicDepCodeAndId[int.Parse(((string)inputRow["DUBL_CEH"]).Trim())];
            return infDetItem;
        }

        public static WhereUse FillItemWhereUse(DataRow inputRow)
        {
            var whereUseItem = new WhereUse();
            short productChiper = DcInfProducts[(string)inputRow["IZD"]];

            whereUseItem.InfDetID = DicDetNumAndId[(string)inputRow["NDET"]];
            whereUseItem.SeriaFrom = (typeof(DBNull) == inputRow["SS"].GetType()) ? 0 : int.Parse(((string)inputRow["SS"]).Trim());
            whereUseItem.SeriaTo = (typeof(DBNull) == inputRow["SPO"].GetType()) ? 99999999 : int.Parse(((string)inputRow["SPO"]).Trim());
            whereUseItem.CountPerProduct = (typeof(DBNull) == inputRow["KOL"].GetType()) ? 0 : int.Parse(((string)inputRow["KOL"]).Trim());
            whereUseItem.InfProductsCipher = productChiper;

            whereUseItem.DepConsumer = (typeof(DBNull) == inputRow["ZP"].GetType()) ? 0 : DicDepCodeAndId[int.Parse(((string)inputRow["ZP"]).Trim())];
            whereUseItem.DepPainting1 = ((typeof(DBNull) == inputRow["ZP1"].GetType()) || (inputRow["ZP1"].ToString() == "")) ? 0 : DicDepCodeAndId[int.Parse(((string)inputRow["ZP1"]).Trim())];
            whereUseItem.DepPainting2 = ((typeof(DBNull) == inputRow["ZP2"].GetType()) || (inputRow["ZP2"].ToString() == "")) ? 0 : DicDepCodeAndId[int.Parse(((string)inputRow["ZP2"]).Trim())];
            whereUseItem.DepPainting3 = ((typeof(DBNull) == inputRow["ZP3"].GetType()) || (inputRow["ZP3"].ToString() == "")) ? 0 : DicDepCodeAndId[int.Parse(((string)inputRow["ZP3"]).Trim())];
            whereUseItem.DepThermal1 = ((typeof(DBNull) == inputRow["ZT1"].GetType()) || (inputRow["ZT1"].ToString() == "")) ? 0 : DicDepCodeAndId[int.Parse(((string)inputRow["ZT1"]).Trim())];
            whereUseItem.DepThermal2 = ((typeof(DBNull) == inputRow["ZT2"].GetType()) || (inputRow["ZT2"].ToString() == "")) ? 0 : DicDepCodeAndId[int.Parse(((string)inputRow["ZT2"]).Trim())];
            return whereUseItem;
        }

        private static LabourNorm FillItemLabourNorm(ODLDB210410DataSet.TN148Row anrow)
        {
            int resultKOD_PROF = 0;
            byte tarifNetDefault = 2;
            LabourNorm labourNormItem = new LabourNorm();
            if (anrow.IsKOD_PROFNull())
                labourNormItem.ProfCode = 0;
            else if (anrow.KOD_PROF.Contains(','))
                labourNormItem.ProfCode = int.Parse(anrow.KOD_PROF.Split(',').First());
            else
                int.TryParse(anrow.KOD_PROF.Trim(), out resultKOD_PROF);
            labourNormItem.ProfCode = resultKOD_PROF;

            labourNormItem.InfDetID = (!DicDetNumAndId.ContainsKey(anrow.NDET) ? 0 : DicDetNumAndId[anrow.NDET]);
            labourNormItem.OperNum = (anrow.IsOPTNull() ? "" : anrow.OPT);
            if (anrow.IsUCHNull())
            {
                labourNormItem.DepRegion = 0;
            }
            else
            {
                labourNormItem.DepRegion = anrow.UCH.Contains("ъ") || anrow.UCH.Contains("+") || anrow.UCH.First() == '0' || anrow.UCH.First() == ' ' || anrow.UCH.Last() == '-' || anrow.UCH.Last() == ','
            ? 0
            : double.Parse(anrow.UCH.Trim().Replace('.', ',').Replace('/', ','));
            }

            labourNormItem.NameKindWork = anrow.IsNAIM_VIDA_RABNull() ? "" : anrow.NAIM_VIDA_RAB;
            labourNormItem.TariffNetNum = (anrow.IsTarifNull() || anrow.Tarif.Trim() == "")
            ? tarifNetDefault
            : byte.Parse(anrow.Tarif.Trim());
            labourNormItem.KindPay = anrow.IsB1Null() ? "C" : anrow.B1;


            labourNormItem.WorkerRate = (anrow.IsRAZRNull() || anrow.RAZR.Equals("+") || anrow.RAZR.Trim() == "") ? 0 : double.Parse(anrow.RAZR.Replace(".", ",").Trim());
            labourNormItem.ItemCTN = (anrow.IsPTN_SHTNull() ? 0 : double.Parse(anrow.PTN_SHT.ToString()));
            labourNormItem.PreparTimeCTN = (anrow.IsPTN_PZVNull() ? 0 : double.Parse(anrow.PTN_PZV.ToString()));
            labourNormItem.ItemPayNorm = (anrow.IsPLAT_SHTNull() ? 0 : double.Parse(anrow.PLAT_SHT.ToString()));
            labourNormItem.PreparTimePayNorm = (anrow.IsPLAT_PZVNull() ? 0 : double.Parse(anrow.PLAT_PZV.ToString()));
            labourNormItem.Valuation = (anrow.IsRASCENull() || anrow.RASCE == "0.00" || anrow.RASCE == "0.0" || anrow.RASCE.Trim() == "") ? 0 : double.Parse(anrow.RASCE);
            labourNormItem.ValPreparTime = (anrow.IsRASC_PZVNull() ? 0 : double.Parse(anrow.RASC_PZV.ToString()));
            labourNormItem.CoeffCTN = (anrow.IsPTNNull() ? 0 : double.Parse(anrow.PTN.ToString()));
            labourNormItem.DocNum = (anrow.IsN_DOKNull() ? "" : anrow.N_DOK);
            labourNormItem.Date = DateTime.Now;
            labourNormItem.TaskNumber = (anrow.IsN_zadanNull() ? "" : anrow.N_zadan);
            return labourNormItem;
        }
        public static LabourNorm FillItemLabourNorm(ODLDB210410DataSet.TN158Row anrow)
        {
            byte tarifNetDefault = 2;
            int resultKOD_PROF = 0;
            LabourNorm labourNormItem = new LabourNorm();

            labourNormItem.InfDetID = (!DicDetNumAndId.ContainsKey(anrow.NDET) ? 0 : DicDetNumAndId[anrow.NDET]);
            labourNormItem.OperNum = (anrow.IsOPTNull() ? "" : anrow.OPT);
            labourNormItem.DepRegion = (anrow.IsUCHNull() || !char.IsNumber(char.Parse(anrow.UCH.Substring(0, 1))) || anrow.UCH.Contains("+"))
            ? 0
            : double.Parse(anrow.UCH.Trim().Replace('.', ',').Replace('/', ',').Replace("ъ", "").Replace("-", ""));
            if (anrow.IsKOD_PROFNull())
                labourNormItem.ProfCode = 0;
            else if (anrow.KOD_PROF.Contains(','))
                labourNormItem.ProfCode = int.Parse(anrow.KOD_PROF.Split(',').First());
            else
                int.TryParse(anrow.KOD_PROF.Trim(), out resultKOD_PROF);
            labourNormItem.ProfCode = resultKOD_PROF;
            labourNormItem.NameKindWork = anrow.IsNAIM_VIDA_RABNull() ? "" : anrow.NAIM_VIDA_RAB;
            labourNormItem.TariffNetNum = (anrow.IsTarifNull() || anrow.Tarif.Trim() == "")
            ? tarifNetDefault
            : byte.Parse(anrow.Tarif.Trim());
            labourNormItem.KindPay = anrow.IsB1Null() ? "C" : anrow.B1;
            labourNormItem.WorkerRate = (anrow.IsRAZRNull() || anrow.RAZR.Equals("+") || anrow.RAZR.Trim() == "") ? 0 : double.Parse(anrow.RAZR.Replace(".", ",").Trim());
            labourNormItem.ItemCTN = (anrow.IsPTN_SHTNull() ? 0 : double.Parse(anrow.PTN_SHT.ToString()));
            labourNormItem.PreparTimeCTN = (anrow.IsPTN_PZVNull() ? 0 : double.Parse(anrow.PTN_PZV.ToString()));
            labourNormItem.ItemPayNorm = (anrow.IsPLAT_SHTNull() ? 0 : double.Parse(anrow.PLAT_SHT.ToString()));
            labourNormItem.PreparTimePayNorm = (anrow.IsPLAT_PZVNull() ? 0 : double.Parse(anrow.PLAT_PZV.ToString()));
            labourNormItem.Valuation = (anrow.IsRASCENull() || anrow.RASCE == "0.00" || anrow.RASCE == "0.0" || anrow.RASCE.Trim() == "") ? 0 : double.Parse(anrow.RASCE);
            labourNormItem.ValPreparTime = (anrow.IsRASC_PZVNull() ? 0 : double.Parse(anrow.RASC_PZV.ToString()));
            labourNormItem.CoeffCTN = (anrow.IsPTNNull() ? 0 : double.Parse(anrow.PTN.ToString()));
            labourNormItem.DocNum = (anrow.IsN_DOKNull() ? "" : anrow.N_DOK);
            labourNormItem.Date = DateTime.Now;
            labourNormItem.TaskNumber = (anrow.IsN_zadanNull() ? "" : anrow.N_zadan);
            return labourNormItem;
        }


        public static WhereOperationUse FillItemWhereOperationUse(ODLDB210410DataSet.TN148Row anrow)
        {
            int seriaFrom = 0;
            if (!anrow.IsN_IZD_CNull() && anrow.N_IZD_C.Trim() != "")
            {
                int.TryParse(anrow.N_IZD_C.Trim(), out seriaFrom);
                if (seriaFrom < 0)
                {
                    seriaFrom = 0;
                }
            }
            int seriaTo = 99999999;
            if (!anrow.IsN_IZD_PONull() && anrow.N_IZD_PO.Trim() != "")
            {
                int.TryParse(anrow.N_IZD_PO.Trim(), out seriaTo);
                if (seriaTo < 0)
                {
                    seriaTo = 99999999;
                }
            }
            var whereUseItem = new WhereOperationUse();
            whereUseItem.SeriaFrom = seriaFrom;
            whereUseItem.SeriaTo = seriaTo;
            whereUseItem.InfProductsChipher = 31;

            return whereUseItem;
        }

        public static WhereOperationUse FillItemWhereOperationUse(ODLDB210410DataSet.TN158Row anrow)
        {
            int seriaFrom = 0;
            if (!anrow.IsN_IZD_CNull() && anrow.N_IZD_C.Trim() != "")
            {

                int.TryParse(anrow.N_IZD_C.Trim(), out seriaFrom);
                if (seriaFrom < 0)
                {
                    seriaFrom = 0;
                }
            }
            int seriaTo = 99999999;
            if (!anrow.IsN_IZD_PONull() && anrow.N_IZD_PO.Trim() != "")
            {
                int.TryParse(anrow.N_IZD_PO.Trim(), out seriaTo);
                if (seriaTo < 0)
                {
                    seriaTo = 99999999;
                }
            }
            var whereUseItem = new WhereOperationUse();
            whereUseItem.SeriaFrom = seriaFrom;
            whereUseItem.SeriaTo = seriaTo;
            whereUseItem.InfProductsChipher = 17;

            return whereUseItem;
        }


        private static InfTariff FillInfTarifItem(ODLDB210410DataSet.TARIFRow trow)
        {
            InfTariff infTariff = new InfTariff();
            infTariff.KindPay = trow.VO;
            infTariff.HourCost = double.Parse(trow.STOIM_RAZR);
            infTariff.TariffNetNum = byte.Parse(trow.N_TARIF_SETKI);
            infTariff.WorkerRate = double.Parse(trow.RAZR.Replace('.', ','));
            return infTariff;
        }



        private enum ListTablesFromAccessDB
        {
            AN158,
            AN148,
            AN70,
            TN158,
            TN148,
            TN70
        }


    }
}


