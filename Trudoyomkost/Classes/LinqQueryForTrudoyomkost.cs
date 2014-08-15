using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlServerCe;
using Wintellect.PowerCollections;
using System.Reflection;
namespace Trudoyomkost
{
    internal static class LinqQueryForTrudoyomkost
    {



        #region Dictionary AllRows, Field: DetNum, ID dbcontext   Used Table: infDet
        public static void FillDictDetNumID(TrudoyomkostDBContext dbcontext, ref Dictionary<string, int> dictDetNumID)
        {
            var detnumInDb = from infdet in dbcontext.InfDet
                             select new
                             {
                                 infdet.DetNum,
                                 infdet.ID
                             };
            dictDetNumID.Clear();

            foreach (var item in detnumInDb)
            {
                dictDetNumID.Add(item.DetNum, item.ID);
            }
        }
        #endregion

        #region Dictionary AllRows, Field: Code, ID, dbcontext Used Table: infDepList
        public static void FillDictDepIDCode(TrudoyomkostDBContext dbcontext, ref Dictionary<int, int> dictDepIDCode)
        {
            var detnumInDB = from infdep in dbcontext.InfDepList
                             select new
                             {
                                 infdep.Code,
                                 infdep.ID
                             };
            dictDepIDCode.Clear();

            foreach (var item in detnumInDB)
            {

                dictDepIDCode.Add(item.Code, item.ID);
            }

        }
        #endregion

        #region Dictionary AllRows, Field: ProfCode, NameKindWork, dbcontext  Table: InfProfession
        public static void FillDictInfProfession(TrudoyomkostDBContext dbcontext, ref Dictionary<string, int> dictInfProfession)
        {

            var detnumInDB = from infdet in dbcontext.InfProfession
                             select new
                             {
                                 infdet.ProfCode,
                                 infdet.NameKindWork
                             };
            dictInfProfession.Clear();

            foreach (var item in detnumInDB)
            {

                dictInfProfession.Add(item.NameKindWork, item.ProfCode);
            }

        }
        #endregion

        #region AllRows DataTable , Field: ProfCode,Name,NameKindWork,LangCode Table InfProfession
        public static void FilldtInfProf(TrudoyomkostDBContext dbcontext, ref TrudoyomkostDBDataSet.infProfessionDataTable dtInfProff)
        {
            var tempResult = from infproff in dbcontext.InfProfession
                             select infproff;
            dtInfProff.Clear();
            foreach (var item in tempResult)
            {
                DataRow rowdttemp = dtInfProff.NewRow();
                rowdttemp[0] = item.ProfCode;
                rowdttemp[1] = item.Name;
                rowdttemp[2] = item.NameKindWork;
                rowdttemp[3] = item.LangCode;
                dtInfProff.Rows.Add(rowdttemp);
            }

        }
        public static DataTable GetAllProducts(TrudoyomkostDBContext dbcontext)
        {
            var result = from infProduct in dbcontext.InfProducts
                         select infProduct;

            return DataTableFromIEnumerable(result);
        }

        #endregion

        #region AllRows ref Dictionary<string, ShortProfInfo> , ref comboBox , dbContext , Columns: ProfCode,NameKindWork, Used Table: infProfession
        public static void FilldcInfProfession(TrudoyomkostDBContext dbcontext, ref Dictionary<string, ShortProfInfo> dcShortInfProf, ref ValidatingComboBox cbinfProf)
        {
            var tempResult = from infprofession in dbcontext.InfProfession
                             select new
                             {
                                 infprofession.ProfCode,
                                 infprofession.NameKindWork
                             };
            dcShortInfProf.Clear();
            foreach (var item in tempResult)
            {
                string fullInfo = item.ProfCode.ToString() + " " + item.NameKindWork;
                dcShortInfProf.Add(fullInfo, new ShortProfInfo(item.ProfCode, item.NameKindWork));
                cbinfProf.Items.Add(fullInfo);
            }

        }
        #endregion

        #region Dictionary<infDet.ID, Dictionary<labournorm.OperNum,labournorm.ID>> ,dbcontext, Table: LabourNorm, infDet
        //public static void FillDictIDOperNum(TrudoyomkostDBContext dbcontext, ref Dictionary<string, Dictionary<string, int>> dictOperID)
        //{
        //    dictOperID.Clear();

        //    var labourNormList = dbcontext.LabourNorm.Join(dbcontext.InfDet, labourNorm => labourNorm.InfDetID,
        //    InfDet => InfDet.ID, (labourNorm, InfDet) => new
        //    {
        //    InfDet.DetNum,
        //    labourNorm.ID,
        //    labourNorm.OperNum
        //    })
        //    .GroupBy(lauborList => lauborList.DetNum)
        //    .Select(groupLabourList => new
        //    {
        //    Key = groupLabourList.Key,
        //    Value = groupLabourList
        //    });

        //    foreach (var item in labourNormList)
        //    {
        //        dictOperID.Add(item.Key, new Dictionary<string, int>());
        //        foreach (var inItem in item.Value)
        //        {
        //            dictOperID[item.Key].Add(inItem.OperNum, inItem.ID);
        //        }
        //    }
        //}
        #endregion

        #region AllRows List<WhereUse> ,dbcontext, Table:WhereUse
        public static List<WhereUse> FillWhereUselst(TrudoyomkostDBContext dbcontext)
        {
            var tempresult = from whereuselst in dbcontext.WhereUse
                             select whereuselst;

            List<WhereUse> whereUseLst = tempresult.ToList();
            return whereUseLst;
        }
        #endregion

        #region AllRows List<InfDepList>, dbcontext Used Table: infDepList
        public static List<InfDepList> FillinfDeplst(TrudoyomkostDBContext dbcontext)
        {
            var tempresult = from deplist in dbcontext.InfDepList
                             select deplist;

            List<InfDepList> infDeplist = tempresult.ToList();
            return infDeplist;
        }
        #endregion

        #region AllRows List<InfDet> , dbcontext, Used Table: infDet
        public static List<InfDet> FillinfDetList(TrudoyomkostDBContext dbcontext)
        {
            var tempresult = from detlist in dbcontext.InfDet
                             select detlist;

            List<InfDet> infDetlist = tempresult.ToList();
            return infDetlist;
        }
        #endregion

        #region AllRows List<Users> , dbcontext, Used Table: Users
        public static List<Users> FillUsersList(TrudoyomkostDBContext dbcontext)
        {
            try
            {

                var tempResult = from userList in dbcontext.Users
                                 select userList;


                var usersList = tempResult.ToList();
                return usersList;
            }
            catch (SqlCeException e)
            {
                var usersList = new List<Users>();
                return usersList;
            }
        }
        #endregion

        #region AllRows BigList<LabourNorm> ,dbcontext, Used Table: LabourNorm
        public static void FillLabourNormList(TrudoyomkostDBContext dbcontext, ref BigList<LabourNorm> inputList)
        {
            var tempresult = from labourlist in dbcontext.LabourNorm
                             select labourlist;
            inputList.Clear();
            //IList temp =  tempresult.ToList();

            foreach (var item in tempresult)
            {
                inputList.Add(item);
            }
        }
        #endregion

        #region AllRows BigList<WhereOperationUse> , dbcontext, Used Table  WhereOperationUse
        public static void FillWhereOperationUseList(TrudoyomkostDBContext dbcontext, ref BigList<WhereOperationUse> inputList)
        {
            var tempresult = from labourlist in dbcontext.WhereOperationUse
                             select labourlist;
            inputList.Clear();

            foreach (var item in tempresult)
            {
                inputList.Add(item);
            }
        }
        #endregion

        #region AllRows where Mask==1 Dictionaty< infproducts.Product,infproducts.Cipher>  dbcontext , Used Table: infProducts
        public static void FilldcInfProducts(TrudoyomkostDBContext dbcontext, ref Dictionary<string, short> dicInfProduct)
        {
            var listProducts = from infproducts in dbcontext.InfProducts
                               where infproducts.Mask == 1 //выбор изделий
                               select new
                               {
                                   infproducts.Product,
                                   infproducts.Cipher
                               };


            dicInfProduct.Clear();
            foreach (var item in listProducts)
            {
                dicInfProduct.Add(item.Product, item.Cipher);
            }

        }
        #endregion

        #region AllRows where Mask==1  List<infProducts> , dbcontext , Used Table: infProducts

        public static void FillInfProductsList(TrudoyomkostDBContext dbcontext, ref List<InfProducts> inputList)
        {
            var tempResult = from infproducts in dbcontext.InfProducts
                             where infproducts.Mask == 1
                             select infproducts;
            inputList.Clear();
            inputList = tempResult.ToList();
        }
        #endregion

        #region AllRows List<tariff.TariffNetNum> , dbcontext , Used Table: infTariff
        public static void GetInfTariffList(TrudoyomkostDBContext dbcontext, ref List<byte> inputTariffList)
        {
            var result = from tariff in dbcontext.InfTariff

                         select tariff.TariffNetNum;

            inputTariffList = result.Distinct().ToList();
        }
        #endregion



        #region AllRows Dictionary<InfTariffInfo, double>, dbcontext , Used Table: infTariff
        public static void FilldcInfTariffInfo(TrudoyomkostDBContext dbcontext, ref Dictionary<InfTariffInfo, double> dcInfTariffInfo)
        {
            var tempResult = from inftariff in dbcontext.InfTariff
                             select inftariff;
            dcInfTariffInfo.Clear();
            foreach (var item in tempResult)
                if (!dcInfTariffInfo.ContainsKey(new InfTariffInfo(item.TariffNetNum, item.KindPay, item.WorkerRate)))
                {
                    dcInfTariffInfo.Add(new InfTariffInfo(item.TariffNetNum, item.KindPay, item.WorkerRate), item.HourCost);
                }

        }
        #endregion

        #region DataTable For Export  , Used Table: LabourNorm, WhereOperationUse, infDet, infDepList, infProducts
        public static DataTable FillTNTable(short productChipher)
        {
            var labourNormlist = from labourNorm in FillTrudoyomkostDB.LabourNormList
                                 join whereOpeationUse in FillTrudoyomkostDB.WhereOperationUseList
                                 on labourNorm.ID equals whereOpeationUse.LabourNormID
                                 join infprod in FillTrudoyomkostDB.infProductList on whereOpeationUse.InfProductsChipher equals infprod.Cipher
                                 where (whereOpeationUse.InfProductsChipher == productChipher)
                                 select new
                                 {
                                     infprod.Product,
                                     labourNorm.InfDetID,
                                     OperNum = labourNorm.OperNum,
                                     labourNorm.WorkerRate,
                                     labourNorm.KindPay,
                                     labourNorm.ProfCode,
                                     NameKindWork = labourNorm.NameKindWork,
                                     labourNorm.TariffNetNum,
                                     labourNorm.ItemCTN,
                                     labourNorm.PreparTimeCTN,
                                     labourNorm.ItemPayNorm,
                                     labourNorm.PreparTimePayNorm,
                                     labourNorm.Valuation,
                                     labourNorm.ValPreparTime,
                                     labourNorm.CoeffCTN,
                                     DocNum = labourNorm.DocNum,
                                     labourNorm.DepRegion,
                                     labourNorm.Date,
                                     TaskNumber = labourNorm.TaskNumber,
                                     whereOpeationUse.SeriaFrom,
                                     whereOpeationUse.SeriaTo
                                 };

            var listDet = from infdet in FillTrudoyomkostDB.infDetList
                          join whereUse in FillTrudoyomkostDB.whereUseList
                          on infdet.ID equals whereUse.InfDetID
                          join infdep in FillTrudoyomkostDB.DicDepCodeAndId on infdet.DepNative equals infdep.Value
                          where whereUse.InfProductsCipher == productChipher
                          select new
                          {
                              infdet.ID,
                              infdet.DetNum,
                              SignIrregDet = (infdet.SignIrregDet == "" ? " " : infdet.SignIrregDet),
                              WhereUseSeriaFrom = whereUse.SeriaFrom,
                              WhereUseSeriaTo = whereUse.SeriaTo,
                              Code = infdet.DepNative == null ? TrudoyomkostSettings.DepNum : infdep.Key
                          };


            var totalResuslt = (from det in listDet
                                join labour in labourNormlist
                                on det.ID equals labour.InfDetID
                                 into gj
                                from subnorm in gj.DefaultIfEmpty()

                                select new
                                {
                                    det.DetNum,
                                    det.SignIrregDet,
                                    Code = det.Code,
                                    Product = (subnorm == null ? " " : subnorm.Product),
                                    OperNum = (subnorm == null ? " " : subnorm.OperNum),

                                    WorkerRate = (subnorm == null ? 0 : subnorm.WorkerRate),
                                    KindPay = (subnorm == null ? " " : subnorm.KindPay),
                                    ProfCode = (subnorm == null ? 0 : subnorm.ProfCode),
                                    NameKindWork = (subnorm == null ? " " : subnorm.NameKindWork),
                                    TariffNetNum = (subnorm == null ? 0 : subnorm.TariffNetNum),

                                    ItemCTN = (subnorm == null ? 0 : subnorm.ItemCTN),
                                    PreparTimeCTN = (subnorm == null ? 0 : subnorm.PreparTimeCTN),
                                    ItemPayNorm = (subnorm == null ? 0 : subnorm.ItemPayNorm),
                                    PreparTimePayNorm = (subnorm == null ? 0 : subnorm.ItemPayNorm),
                                    Valuation = (subnorm == null ? 0 : subnorm.Valuation),
                                    ValPreparTime = (subnorm == null ? 0 : subnorm.ValPreparTime),
                                    CoeffCTN = (subnorm == null ? 0 : subnorm.CoeffCTN),
                                    DocNum = (subnorm == null ? " " : subnorm.DocNum),
                                    DepRegion = (subnorm == null ? 0 : subnorm.DepRegion),
                                    Date = (subnorm == null ? DateTime.Now : subnorm.Date),
                                    TaskNumber = (subnorm == null ? " " : subnorm.TaskNumber),
                                    SeriaFrom = (subnorm == null ? det.WhereUseSeriaFrom : subnorm.SeriaFrom),
                                    SeriaTo = (subnorm == null ? det.WhereUseSeriaTo : subnorm.SeriaTo)
                                }).Distinct();


            DataTable resultdt = new DataTable();
            ODLDB210410DataSet dataset = new ODLDB210410DataSet();

            if (productChipher == 31)
            {
                resultdt = dataset.TN148;
            }
            else if (productChipher == 17)
            {
                resultdt = dataset.TN158;
            }


            foreach (var item in totalResuslt)
            {
                DataRow rowdttemp = resultdt.NewRow();
                rowdttemp["NDET"] = item.DetNum;
                rowdttemp["CEH"] = MathFunctionForSeries.GetOldFormatDepNum(Properties.Settings.Default.DepNum);
                rowdttemp["UCH"] = item.DepRegion.ToString();
                rowdttemp["B1"] = item.KindPay;
                rowdttemp["OP"] = " ";
                rowdttemp["OPT"] = item.OperNum == "" ? " " : item.OperNum;
                rowdttemp["RASCE"] = item.Valuation.ToString();
                rowdttemp["RAZR"] = item.WorkerRate.ToString();
                rowdttemp["DUBL_CEH"] = MathFunctionForSeries.GetOldFormatDepNum(item.Code);
                rowdttemp["PRN"] = item.SignIrregDet.ToString();
                rowdttemp["PR_IZD"] = item.Product.ToString();
                rowdttemp["N_IZD_C"] = MathFunctionForSeries.GetOldFormatSeria(item.SeriaFrom);
                rowdttemp["N_IZD_PO"] = MathFunctionForSeries.GetOldFormatSeria(item.SeriaTo);
                rowdttemp["N_zadan"] = item.TaskNumber == "" ? " " : item.TaskNumber ?? " ";
                rowdttemp["N_DOK"] = (item.DocNum.Length > 16) ? item.DocNum.Substring(0, 16) : item.DocNum == "" ? " " : item.DocNum;
                rowdttemp["KOD_PROF"] = item.ProfCode.ToString();
                rowdttemp["NAIM_VIDA_RAB"] = item.NameKindWork == "" ? " " : item.NameKindWork;
                rowdttemp["PTN_SHT"] = item.ItemCTN;
                rowdttemp["PTN_PZV"] = item.PreparTimeCTN;
                rowdttemp["PLAT_SHT"] = item.ItemPayNorm;
                rowdttemp["PLAT_PZV"] = item.PreparTimePayNorm;
                rowdttemp["RASC_PZV"] = item.ValPreparTime;
                rowdttemp["PTN"] = item.CoeffCTN;
                rowdttemp["Tarif"] = item.TariffNetNum;
                rowdttemp["Дата_форм"] = String.Format("{0:yyyyMMdd}", item.Date);
                rowdttemp["Pr_metki"] = " ";
                rowdttemp["N_izd_c_r"] = MathFunctionForSeries.GetStringSeriaNumber(item.SeriaFrom);
                rowdttemp["N_izd_po_r"] = MathFunctionForSeries.GetStringSeriaNumber(item.SeriaTo);
                resultdt.Rows.Add(rowdttemp);

            }
            return resultdt;
        }
        #endregion

        #region Full infTariff List<infTariff >
        public static void GetInfTariff(ref List<InfTariff> inputlist)
        {
            using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                inputlist = currentContext.InfTariff.ToList();
            }
        }
        #endregion


        #region Fill operNum listBox for need diapason items, Used Lists: FillTrudoyomkostDB.LabourNormList, FillTrudoyomkostDB.WhereOperationUseList
        public static void FillOperNumlbox(int infDetID, int seriaFrom, int seriaTo, int infProdChipher, ref ListBox lbox)
        {
            var tempResult = from labourNorm in FillTrudoyomkostDB.LabourNormList
                             join whereOpeationUse in FillTrudoyomkostDB.WhereOperationUseList on labourNorm.ID
                                 equals whereOpeationUse.LabourNormID
                             where labourNorm.InfDetID == infDetID
                             where whereOpeationUse.InfProductsChipher == infProdChipher &&
                                                    whereOpeationUse.SeriaFrom <= seriaFrom && whereOpeationUse.SeriaTo >= seriaFrom

                             select new
                             {
                                 labourNorm.OperNum
                             }
                                 into allOper
                                 orderby allOper.OperNum
                                 select allOper;

            lbox.Items.Clear();
            foreach (var item in tempResult)
            {
                lbox.Items.Add(item.OperNum);
            }
        }
        #endregion

        public static void FillLabourNormMore50sign()
        {
            var temResult = from labourNorm in FillTrudoyomkostDB.LabourNormList
                            where labourNorm.OperNum.Length >= 50
                            select new
                            {
                                labourNorm.OperNum,
                                labourNorm.ID
                            };

            foreach (var item in temResult)
            {
                string temp = item.OperNum.Remove(0, 50);
                FillTrudoyomkostDB.LabourNormTableAdapter.UpdateOperNum(temp, item.ID);
            }

        }

        #region Fill Dictionary <infdet.DetNum,infdet.ID> for selected Product (148,158,70, etc) FillTrudoyomkostDB.infDetList  FillTrudoyomkostDB.whereUseList
        public static void FilldcDetNumForProduct(ref Dictionary<string, int> dcIDDetNum, int selectProd) //selectProd шифр изделия
        {


            var detnumInDb = (from infdet in FillTrudoyomkostDB.infDetList
                              join whereUse in FillTrudoyomkostDB.whereUseList on infdet.ID equals whereUse.InfDetID
                              where whereUse.InfProductsCipher == selectProd && infdet.Available == true && infdet.IndicOSPK != 1 && infdet.SignIrregDet != "Н"
                              select new
                              {
                                  infdet.DetNum,
                                  infdet.ID
                              }).Distinct();

            dcIDDetNum.Clear();

            foreach (var item in detnumInDb)
            {
                dcIDDetNum.Add(item.DetNum, item.ID);
            }
        }

        public static void GetEmptyDetNum(ref IList<string> detNumList, int seriaFrom, int productChipher)
        {
            List<InfDet> tempInfDetList;
            List<LabourNorm> tempLabourNormList;

            var listDetForSelectedSeria = from infdet in FillTrudoyomkostDB.infDetList
                                          join whereUse in FillTrudoyomkostDB.whereUseList
                                          on infdet.ID equals whereUse.InfDetID
                                          where whereUse.SeriaFrom <= seriaFrom && seriaFrom <= whereUse.SeriaTo && whereUse.InfProductsCipher == productChipher &&
                                          infdet.Available == true && infdet.IndicOSPK != 1 && infdet.SignIrregDet != "Н"

                                          select new
                                          {
                                              infdet.DetNum,
                                              infdet.ID
                                          };


            var listLabourForSelectedSeria = (from labour in FillTrudoyomkostDB.LabourNormList
                                              join whereOperUse in FillTrudoyomkostDB.WhereOperationUseList
                                              on labour.ID equals whereOperUse.LabourNormID
                                              where whereOperUse.SeriaFrom <= seriaFrom && seriaFrom <= whereOperUse.SeriaTo && whereOperUse.InfProductsChipher == productChipher
                                              select new
                                              {
                                                  labour.InfDetID
                                              }).Distinct();

            var tempList = new List<int>();
            foreach (var item in listLabourForSelectedSeria)
            {
                tempList.Add(item.InfDetID);
            }

            detNumList.Clear();

            foreach (var item in listDetForSelectedSeria)
            {
                if (!tempList.Contains(item.ID))
                {
                    detNumList.Add(item.DetNum);
                }
            }

        }

        #endregion

        #region Fill DataTable with operation for select diapasons serias and select product FillTrudoyomkostDB.CurrentLabourNormList, FillTrudoyomkostDB.WhereOperationUseList
        public static void FilldtNormViewer(int seriaFrom, int seriaTo, int infProdChipher, ref TrudoyomkostDBDataSet.LabourNormDataTable dttemp, bool timeInHours, ref NormTotal normTotalbyTheJob, ref NormTotal normTotalbyTheTime)
        {

            var tempResult = from labourNorm in FillTrudoyomkostDB.CurrentLabourNormList
                             join whereOpeationUse in FillTrudoyomkostDB.WhereOperationUseList on labourNorm.ID
                                 equals whereOpeationUse.LabourNormID
                             where
                                 whereOpeationUse.InfProductsChipher == infProdChipher &&
                                 ((whereOpeationUse.SeriaFrom == seriaFrom && seriaFrom <= whereOpeationUse.SeriaTo) || (whereOpeationUse.SeriaFrom <= seriaFrom && whereOpeationUse.SeriaTo >= seriaFrom))

                             select labourNorm

                                 into allInfo
                                 orderby allInfo.OperNum
                                 select allInfo;

            dttemp.Clear();
            normTotalbyTheJob.Clear();
            normTotalbyTheTime.Clear();
            normTotalbyTheTime.ValuatePrTimeSum = 0;
            normTotalbyTheJob.ValuatePrTimeSum = 0;

            foreach (var item in tempResult)
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
                rowdttemp[9] = (timeInHours) ? (Decimal)item.ItemCTN : (Decimal)Math.Round((item.ItemCTN * 60), TrudoyomkostSettings.RoundNum);
                rowdttemp[10] = (timeInHours) ? (Decimal)item.PreparTimeCTN : (Decimal)Math.Round(item.PreparTimeCTN * 60, TrudoyomkostSettings.RoundNum);
                rowdttemp[11] = (timeInHours) ? (Decimal)item.ItemPayNorm : (Decimal)Math.Round(item.ItemPayNorm * 60, TrudoyomkostSettings.RoundNum);
                rowdttemp[12] = (timeInHours) ? (Decimal)item.PreparTimePayNorm : (Decimal)Math.Round((item.PreparTimePayNorm * 60), TrudoyomkostSettings.RoundNum);
                rowdttemp[13] = item.Valuation;
                rowdttemp[14] = item.ValPreparTime;
                rowdttemp[15] = Math.Round(item.CoeffCTN, TrudoyomkostSettings.RoundNum);
                rowdttemp[16] = item.DocNum;
                rowdttemp[17] = item.Date;
                rowdttemp[18] = item.TaskNumber;
                dttemp.Rows.Add(rowdttemp);
                if (item.ProfCode != 2322 && item.ProfCode != 2351)
                {
                    if (item.KindPay.Equals("С") || item.KindPay.Equals("C"))
                    {
                        normTotalbyTheJob.ItemCTNSum += item.ItemCTN;
                        normTotalbyTheJob.PrTimeCTNSum += item.PreparTimeCTN;
                        normTotalbyTheJob.ItemPaySum += item.ItemPayNorm;
                        normTotalbyTheJob.ValuateSum += item.Valuation;
                        normTotalbyTheJob.PrTimePaySum += item.PreparTimePayNorm;
                        normTotalbyTheJob.ValuatePrTimeSum += item.ValPreparTime;

                    }
                    else
                    {
                        normTotalbyTheTime.ItemCTNSum += item.ItemCTN;
                        normTotalbyTheTime.PrTimeCTNSum += item.PreparTimeCTN;
                        normTotalbyTheTime.ItemPaySum += item.ItemPayNorm;
                        normTotalbyTheTime.ValuateSum += item.Valuation;
                        normTotalbyTheTime.PrTimePaySum += item.PreparTimePayNorm;
                        normTotalbyTheTime.ValuatePrTimeSum += item.ValPreparTime;
                    }
                }

            }
            normTotalbyTheJob.ReCalcInMinutes(timeInHours);
            normTotalbyTheTime.ReCalcInMinutes(timeInHours);
            normTotalbyTheJob.SetCoeff();
            normTotalbyTheTime.SetCoeff();

        }
        #endregion

        #region Fill distinct list doc num , Used List: FillTrudoyomkostDB.LabourNormList
        public static AutoCompleteStringCollection FillDocNumlst()
        {
            AutoCompleteStringCollection temList = new AutoCompleteStringCollection();
            var tempresult = (from laborNorm in FillTrudoyomkostDB.LabourNormList
                              select new
                              {
                                  laborNorm.DocNum
                              }).Distinct();
            /* Временно добавил переменную k и вручную добавил новый приказ, 
             * нужно пересмотреть и сделать возможным добовлять новые приказы!!!!*/
            bool k = false;
            foreach (var item in tempresult)
            {
                if (item.DocNum == "Пр.№303 от 01.07.2014")
                    k = true;
                temList.Add(item.DocNum);
            }
            if (!k) temList.Add("Пр.№303 от 01.07.2014");
            return temList;

        }
        
        #endregion

        #region Return List<WhereOperationUse> for selected LabourNormID , Used list: FillTrudoyomkostDB.WhereOperationUseList
        public static List<WhereOperationUse> FillWhereOperationUseList(int LabourNormID)
        {
            var tempResult = from whereOperUse in FillTrudoyomkostDB.WhereOperationUseList
                             where whereOperUse.LabourNormID == LabourNormID
                             select whereOperUse;
            List<WhereOperationUse> tempList = tempResult.ToList();
            return tempList;
        }
        #endregion

        #region Fill ref List<LabourNorm> for selected infDetID
        public static void FillLabourListForSelectDet(int infDetID, ref List<LabourNorm> inputList)
        {
            var tempResult = from labourlist in FillTrudoyomkostDB.LabourNormList
                             where labourlist.InfDetID == infDetID
                             select labourlist;
            inputList.Clear();
            inputList = tempResult.ToList();
        }
        #endregion

        #region Fill Dictionary <labour.ID, DataTable>
        public static void FilldcOperApply(ref Dictionary<int, DataTable> dcInput)
        {
            var tempResult = from whereOperUse in FillTrudoyomkostDB.WhereOperationUseList
                             join labournorm in FillTrudoyomkostDB.CurrentLabourNormList on whereOperUse.LabourNormID equals labournorm.ID
                             from infproduct in FillTrudoyomkostDB.infProductList
                             where
                              whereOperUse.InfProductsChipher == infproduct.Cipher

                             select new
                             {
                                 labournorm.ID,
                                 whereOperUse.SeriaFrom,
                                 whereOperUse.SeriaTo,
                                 infproduct.Product
                             }
                                 into LstApplyOper
                                 group LstApplyOper by LstApplyOper.ID
                                     into
                                     grLstApplyOper
                                     select new
                                     {
                                         Key = grLstApplyOper.Key,
                                         Value = grLstApplyOper
                                     };


            dcInput.Clear();


            foreach (var item in tempResult)
            {
                DataTable dttemp = UserDataTables.CreateOperAppDt();
                foreach (var itemlst in item.Value)
                {

                    DataRow rowdttemp = dttemp.NewRow();
                    rowdttemp[0] = itemlst.Product;
                    rowdttemp[1] = MathFunctionForSeries.GetStringSeriaNumber(itemlst.SeriaFrom);
                    rowdttemp[2] = MathFunctionForSeries.GetStringSeriaNumber(itemlst.SeriaTo);
                    dttemp.Rows.Add(rowdttemp);
                }
                dcInput.Add(item.Key, dttemp);
            }
        }
        #endregion

        #region Get All List<LabourNorm> for input detid     Used list: FillTrudoyomkostDB.LabourNormList
        public static List<LabourNorm> FillLabournNormForDet(int detId)
        {
            var tempResult = from labourNormlst in FillTrudoyomkostDB.LabourNormList
                             where labourNormlst.InfDetID == detId
                             select labourNormlst;

            List<LabourNorm> labourNormlist = tempResult.ToList();
            return labourNormlist;

        }
        #endregion


        private static void GetInfDet_and_labourNormList_ForSelectSeria(int seriaFrom, short productChipher, out List<InfDet> outInfdetlist, out List<LabourNorm> outLabourNormlist)
        {
            var listDetForSelectedSeria = from infdet in FillTrudoyomkostDB.infDetList
                                          join whereUse in FillTrudoyomkostDB.whereUseList
                                          on infdet.ID equals whereUse.InfDetID
                                          where whereUse.SeriaFrom <= seriaFrom && seriaFrom <= whereUse.SeriaTo && whereUse.InfProductsCipher == productChipher
                                          select infdet;
            outInfdetlist = listDetForSelectedSeria.ToList();

            var listLabourForSelectedSeria = from labour in FillTrudoyomkostDB.LabourNormList
                                             join whereOperUse in FillTrudoyomkostDB.WhereOperationUseList
                                             on labour.ID equals whereOperUse.LabourNormID
                                             where whereOperUse.SeriaFrom <= seriaFrom && seriaFrom <= whereOperUse.SeriaTo && whereOperUse.InfProductsChipher == productChipher
                                             select labour;
            outLabourNormlist = listLabourForSelectedSeria.ToList();
        }


        public static DataTable GetAllLabourNormForSeria(int seriaFrom, int seriaTo, int productChipher)
        {
            var listDetForSelectedSeria = from infdet in FillTrudoyomkostDB.infDetList
                                          join whereUse in FillTrudoyomkostDB.whereUseList
                                          on infdet.ID equals whereUse.InfDetID
                                          where whereUse.SeriaFrom <= seriaFrom && seriaFrom <= whereUse.SeriaTo && whereUse.InfProductsCipher == productChipher
                                          select infdet;

            var listLabourForSelectedSeria = from labour in FillTrudoyomkostDB.LabourNormList
                                             join whereOperUse in FillTrudoyomkostDB.WhereOperationUseList
                                             on labour.ID equals whereOperUse.LabourNormID
                                             where whereOperUse.SeriaFrom <= seriaFrom && seriaFrom <= whereOperUse.SeriaTo && whereOperUse.InfProductsChipher == productChipher
                                             select labour;

            var result = from infdet in listDetForSelectedSeria
                         join
                             labour in listLabourForSelectedSeria on infdet.ID equals labour.InfDetID
                             into gj
                         from subnorm in gj.DefaultIfEmpty()

                         select new
                         {
                             infdet.DetNum,
                             OperNum = (subnorm == null ? String.Empty : subnorm.OperNum),
                             ProfCode = (subnorm == null ? 0 : subnorm.ProfCode),
                             NameKindWork = (subnorm == null ? String.Empty : subnorm.NameKindWork),
                             WorkerRate = (subnorm == null ? 0 : subnorm.WorkerRate),
                             KindPay = (subnorm == null ? String.Empty : subnorm.KindPay),
                             ItemCTN = (subnorm == null ? 0 : subnorm.ItemCTN),
                             PreparTimeCTN = (subnorm == null ? 0 : subnorm.PreparTimeCTN),
                             ItemPayNorm = (subnorm == null ? 0 : subnorm.ItemPayNorm),
                             PreparTimePayNorm = (subnorm == null ? 0 : subnorm.ItemPayNorm),
                             Valuation = (subnorm == null ? 0 : subnorm.Valuation),
                             ValPreparTime = (subnorm == null ? 0 : subnorm.ValPreparTime),
                             TaskNumber = (subnorm == null ? String.Empty : subnorm.TaskNumber),
                         };


            DataTable dt = DataTableFromIEnumerable(result);
            return dt;

        }

        private static DataTable DataTableFromIEnumerable(IEnumerable ien)
        {
            DataTable dt = new DataTable();

            foreach (var obj in ien)
            {

                Type t = obj.GetType();
                PropertyInfo[] pis = t.GetProperties();
                if (dt.Columns.Count == 0)
                {
                    foreach (PropertyInfo pi in pis)
                    {
                        Type pt = pi.PropertyType;


                        if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                            pt = Nullable.GetUnderlyingType(pt);
                        dt.Columns.Add(pi.Name, pt);
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pi in pis)
                {
                    object value = pi.GetValue(obj, null);

                    if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        if (Nullable.GetUnderlyingType(pi.PropertyType).Equals(typeof(double)))
                            if (value == null)
                                dr[pi.Name] = 0;
                            else
                                dr[pi.Name] = value;


                        if (Nullable.GetUnderlyingType(pi.PropertyType).FullName == typeof(System.DateTime).FullName)
                            if (value == null)
                                dr[pi.Name] = DateTime.Now;
                            else
                                dr[pi.Name] = value;
                    }
                    else
                        dr[pi.Name] = value;
                }
                dt.Rows.Add(dr);
            }
            return dt;

        }
        public static void SetWhereUseDt(DataTable inputdt, List<InfDepList> depList, Dictionary<string, short> infProducts, int detNumID)
        {
            var tempResult = from whereUse in FillTrudoyomkostDB.whereUseList
                             join dep in depList on whereUse.DepConsumer equals dep.ID
                             from infproduct in infProducts
                             where infproduct.Value == whereUse.InfProductsCipher && whereUse.InfDetID == detNumID
                             select new
                             {
                                 whereUse.InfDetID,
                                 whereUse.SeriaFrom,
                                 whereUse.SeriaTo,
                                 whereUse.CountPerProduct,
                                 dep.Code,
                                 infproduct.Key
                             }
                                 into allInfo
                                 group allInfo by allInfo.InfDetID
                                     into groupAllinfo
                                     select new
                                     {
                                         Key = groupAllinfo.Key,
                                         Value = groupAllinfo
                                     };


            inputdt.Rows.Clear();
            foreach (var item in tempResult)
            {
                foreach (var itemIn in item.Value)
                {
                    DataRow rowdtAll = inputdt.NewRow();
                    rowdtAll[0] = MathFunctionForSeries.GetStringSeriaNumber(itemIn.SeriaFrom);
                    rowdtAll[1] = MathFunctionForSeries.GetStringSeriaNumber(itemIn.SeriaTo);
                    rowdtAll[2] = itemIn.Key;
                    rowdtAll[3] = itemIn.CountPerProduct;
                    rowdtAll[4] = itemIn.Code;
                    rowdtAll[5] = itemIn.SeriaFrom;
                    rowdtAll[6] = itemIn.SeriaTo;
                    inputdt.Rows.Add(rowdtAll);
                }
            }

        }

        public static void SelectLabourNormTaskNum(string inputTaskNum)
        {
            var result = FillTrudoyomkostDB.CurrentLabourNormList.Where(labour => labour.TaskNumber == inputTaskNum);
            FillTrudoyomkostDB.CurrentLabourNormList = result.ToList();
        }

        public static List<string> SelectAllTaskNum()
        {
            var result = FillTrudoyomkostDB.CurrentLabourNormList.Select(labor => labor.TaskNumber).Distinct();
            var list = result.ToList();
            return list;
        }


        public static int GetAmountDetForSeria(int seriaFrom, int seriaTo, int productChipher, string PKP)
        {

            var result = FillTrudoyomkostDB.infDetList.Join(FillTrudoyomkostDB.whereUseList,
                                                                                    i => i.ID,
                                                                                    w => w.InfDetID,
                                                                                (i, w) => new { i.ID, i.PKP, w.SeriaFrom, w.SeriaTo, w.InfProductsCipher })
                                                                                .Where(r => r.PKP.Equals(PKP) && r.SeriaFrom <= seriaFrom && seriaFrom <= r.SeriaTo && r.InfProductsCipher == productChipher);

            return result.Count();
        }

        public static int GetTotalDetForSeria(int seriaFrom, int seriaTo, int productChipher, string PKP)
        {

            var result = FillTrudoyomkostDB.infDetList.Join(FillTrudoyomkostDB.whereUseList,
                                                                                    i => i.ID,
                                                                                    w => w.InfDetID,
                                                                                (i, w) => new { i.ID, i.PKP, w.SeriaFrom, w.SeriaTo, w.InfProductsCipher, w.CountPerProduct })
                                                                                .Where(r => r.PKP.Equals(PKP) && r.SeriaFrom <= seriaFrom && seriaFrom <= r.SeriaTo && r.InfProductsCipher == productChipher);

            int total = 0;
            foreach (var item in result)
            {
                total += item.CountPerProduct;
            }
            return total;
        }

        // начало моих изменений

        public static object GetTotalDetForSeria2(int seriaFrom, int seriaTo, int productChipher)
        {

            var result = FillTrudoyomkostDB.LabourNormList.Join(FillTrudoyomkostDB.whereUseList,
                                                                                    i => i.InfDetID,
                                                                                    w => w.InfDetID,
                                                                                (i, w) => new { i.InfDetID, i.ItemCTN, i.ItemPayNorm, w.SeriaFrom, w.SeriaTo, w.InfProductsCipher })
                                                                                .Where(r => r.SeriaFrom <= seriaFrom && seriaFrom <= r.SeriaTo && r.InfProductsCipher == productChipher && r.ItemCTN != 0);


            int total = 0;
            foreach (var item in result)
            {
                total++;
            }
            return total;

        }

        public static object GetTotalDetForSeria3(int seriaFrom, int seriaTo, int productChipher)
        {

            var result = FillTrudoyomkostDB.LabourNormList.Join(FillTrudoyomkostDB.whereUseList,
                                                                                    i => i.InfDetID,
                                                                                    w => w.InfDetID,
                                                                                (i, w) => new { i.InfDetID, i.ItemCTN, i.ItemPayNorm, w.SeriaFrom, w.SeriaTo, w.InfProductsCipher })
                                                                                .Where(r => r.SeriaFrom <= seriaFrom && seriaFrom <= r.SeriaTo && r.InfProductsCipher == productChipher && r.ItemPayNorm != 0);


            int total = 0;
            foreach (var item in result)
            {
                total++;
            }
            return total;

        }


        static public int VozvratKolvaItemPayNorm(int seriaFrom, int seriaTo, int productChipher)
        {
            //Подключаем sdf
            string connStr = @"Data Source = 'd:\Projects\TRUDOEMKOST\трудоемкость2\Trudoyomkost\Trudoyomkost\bin\Debug\TrudoyomkostDB.sdf'";

            BindingSource bindingSorce = new BindingSource();

            SqlCeConnection coon = new SqlCeConnection(connStr);
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = coon;
            cmd.CommandText = @"SELECT * FROM infDet
 LEFT JOIN
  LabourNorm ON infDet.ID = LabourNorm.infDetID
     LEFT JOIN
  whereUse ON infDet.ID = whereUse.infDetID";
            SqlCeDataAdapter adapter = new SqlCeDataAdapter(cmd);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            bindingSorce.DataSource = dataset.Tables[0];
            coon.Close();
            //  return dataset.Tables[0];

            int iterat = 0;
            for (int ix = 0; ix < dataset.Tables[0].Rows.Count; ix++)
            {
                if (dataset.Tables[0].Rows[ix]["ItemPayNorm"].ToString() != "0")
                    if (dataset.Tables[0].Rows[ix]["SeriaFrom"] != null && dataset.Tables[0].Rows[ix]["SeriaTo"] != null)
                        if (!DBNull.Value.Equals(dataset.Tables[0].Rows[ix]["infProductsCipher"]))
                            if (Convert.ToInt64(dataset.Tables[0].Rows[ix]["SeriaFrom"]) <= seriaFrom && seriaFrom <= Convert.ToInt64(dataset.Tables[0].Rows[ix]["SeriaTo"]) && Convert.ToInt64(dataset.Tables[0].Rows[ix]["infProductsCipher"]) == productChipher)
                                iterat++;


            }
            return iterat;
        }

        //конец измений



        public static void GetAmountNormsForSeria(int seriaFrom, int seriaTo, int productChipher, ref int AmountPayNorm, ref int AmountCTNorm)
        {

        }

        public static void GetTotalCalculNormReport(int seriaFrom, int productChipher, ref DataTable dttemp)
        {
            var currentDetList = from infdet in FillTrudoyomkostDB.infDetList
                                 join whereuse in FillTrudoyomkostDB.whereUseList
                                 on infdet.ID equals whereuse.InfDetID
                                 where whereuse.SeriaFrom <= seriaFrom && seriaFrom <= whereuse.SeriaTo && whereuse.InfProductsCipher == productChipher
                                 select new
                                 {
                                     infdet.ID,
                                     whereuse.CountPerProduct
                                 };


            var result = from labourNorm in FillTrudoyomkostDB.LabourNormList
                         join det in currentDetList
                         on labourNorm.InfDetID equals det.ID
                         join whereOperUse in FillTrudoyomkostDB.WhereOperationUseList
                         on labourNorm.ID equals whereOperUse.LabourNormID
                         where labourNorm.WorkerRate > 0.0 && labourNorm.ProfCode != 0 && whereOperUse.SeriaFrom <= seriaFrom && seriaFrom <= whereOperUse.SeriaTo && whereOperUse.InfProductsChipher == productChipher
                         orderby labourNorm.KindPay
                         orderby labourNorm.WorkerRate
                         select new
                         {
                             det.CountPerProduct,
                             labourNorm.InfDetID,
                             OperNum = labourNorm.OperNum,
                             labourNorm.WorkerRate,
                             labourNorm.KindPay,
                             labourNorm.ProfCode,
                             NameKindWork = labourNorm.NameKindWork,
                             labourNorm.TariffNetNum,
                             labourNorm.ItemCTN,
                             labourNorm.PreparTimeCTN,
                             labourNorm.ItemPayNorm,
                             labourNorm.PreparTimePayNorm,
                             labourNorm.Valuation,
                             labourNorm.ValPreparTime,
                             labourNorm.CoeffCTN,
                             DocNum = labourNorm.DocNum,
                             labourNorm.DepRegion,
                             labourNorm.Date,
                             TaskNumber = labourNorm.TaskNumber,

                         }
                             into TotalRes
                             group TotalRes by TotalRes.ProfCode
                                 into groupAllinfo
                                 select new
                                 {
                                     Key = groupAllinfo.Key,
                                     Value = groupAllinfo
                                 };




            NormTotal kindProfAndRateTotal = new NormTotal();
            NormTotal ProffCodeTotal = new NormTotal();
            NormTotal TotalForProductByTheJob = new NormTotal();
            NormTotal TotalForProductByTheTime = new NormTotal();

            dttemp.Clear();
            foreach (var item in result)
            {
                DataRow rowdttemp = dttemp.NewRow();

                rowdttemp["ProfCode"] = item.Key;
                rowdttemp["NameKindWork"] = item.Value.First().NameKindWork;

                string beginKindPay = item.Value.First().KindPay;
                double beginWorkRate = item.Value.First().WorkerRate;

                dttemp.Rows.Add(rowdttemp);

                string t = item.Key.ToString();
                foreach (var labour in item.Value)
                {

                    if ((beginKindPay != labour.KindPay || beginWorkRate != Math.Round(labour.WorkerRate, 2)))
                    {

                        AddRowIntoDgTotal(dttemp, ref kindProfAndRateTotal, ref rowdttemp, beginKindPay, beginWorkRate);

                        kindProfAndRateTotal.Clear();
                        beginKindPay = labour.KindPay;
                        beginWorkRate = Math.Round(labour.WorkerRate, 2);
                    }

                    kindProfAndRateTotal.CalcTotal(labour.ItemCTN * labour.CountPerProduct,
                                                    labour.PreparTimeCTN,
                                                    labour.ItemPayNorm * labour.CountPerProduct,
                                                    labour.PreparTimePayNorm,
                                                    labour.Valuation,
                                                    labour.ValPreparTime ?? 0
                                                    );

                    if (labour == item.Value.Last())
                    {
                        AddRowIntoDgTotal(dttemp, ref kindProfAndRateTotal, ref rowdttemp, beginKindPay, beginWorkRate);

                        kindProfAndRateTotal.Clear();
                        rowdttemp = dttemp.NewRow();
                        dttemp.Rows.Add(rowdttemp);
                        //item.Value.
                        rowdttemp = dttemp.NewRow();
                        beginKindPay = "С";

                        ProffCodeTotal.ItemCTNSum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.ItemCTN * lbn.CountPerProduct);
                        ProffCodeTotal.PrTimeCTNSum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.PreparTimeCTN);
                        ProffCodeTotal.ItemPaySum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.ItemPayNorm * lbn.CountPerProduct);
                        ProffCodeTotal.PrTimePaySum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.PreparTimePayNorm);
                        ProffCodeTotal.ValuateSum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.Valuation);
                        ProffCodeTotal.ValuatePrTimeSum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.ValPreparTime);
                        TotalForProductByTheJob.CalcTotal(ProffCodeTotal.ItemCTNSum, ProffCodeTotal.PrTimeCTNSum, ProffCodeTotal.ItemPaySum, ProffCodeTotal.PrTimePaySum, ProffCodeTotal.ValuateSum, ProffCodeTotal.ValuatePrTimeSum ?? 0);
                        AddRowIntoDgTotal(dttemp, ref ProffCodeTotal, ref rowdttemp, beginKindPay);

                        beginKindPay = "П";
                        ProffCodeTotal.ItemCTNSum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.ItemCTN * lbn.CountPerProduct);
                        ProffCodeTotal.PrTimeCTNSum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.PreparTimeCTN);
                        ProffCodeTotal.ItemPaySum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.ItemPayNorm * lbn.CountPerProduct);
                        ProffCodeTotal.PrTimePaySum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.PreparTimePayNorm);
                        ProffCodeTotal.ValuateSum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.Valuation);
                        ProffCodeTotal.ValuatePrTimeSum = item.Value.Where(lbn => lbn.KindPay == beginKindPay).Sum(lbn => lbn.ValPreparTime);
                        TotalForProductByTheTime.CalcTotal(ProffCodeTotal.ItemCTNSum, ProffCodeTotal.PrTimeCTNSum, ProffCodeTotal.ItemPaySum, ProffCodeTotal.PrTimePaySum, ProffCodeTotal.ValuateSum, ProffCodeTotal.ValuatePrTimeSum ?? 0);
                        AddRowIntoDgTotal(dttemp, ref ProffCodeTotal, ref rowdttemp, beginKindPay);

                    }

                }
                dttemp.Rows.Add(dttemp.NewRow());
            }
            AddRowIntoDgTotal(dttemp, TotalForProductByTheJob, "Итог для изделия Сдельно");
            AddRowIntoDgTotal(dttemp, TotalForProductByTheTime, "Итог для изделия Повременно");
        }

        private static void AddRowIntoDgTotal(DataTable dttemp, NormTotal inputNormTotal, string inputStr)
        {
            DataRow rowdttemp = dttemp.NewRow();
            rowdttemp["NameKindWork"] = inputStr;
            rowdttemp["ItemCTN"] = (Decimal)Math.Round(inputNormTotal.ItemCTNSum, TrudoyomkostSettings.RoundNum);
            rowdttemp["PreparTimeCTN"] = (Decimal)Math.Round(inputNormTotal.PrTimeCTNSum, TrudoyomkostSettings.RoundNum);
            rowdttemp["ItemPayNorm"] = (Decimal)Math.Round(inputNormTotal.ItemPaySum, TrudoyomkostSettings.RoundNum);
            rowdttemp["PreparTimePayNorm"] = (Decimal)Math.Round(inputNormTotal.PrTimePaySum, TrudoyomkostSettings.RoundNum);
            rowdttemp["Valuation"] = Math.Round(inputNormTotal.ValuateSum, TrudoyomkostSettings.RoundNum);
            rowdttemp["ValPreparTime"] = Math.Round(inputNormTotal.ValuatePrTimeSum ?? 0, TrudoyomkostSettings.RoundNum);
            dttemp.Rows.Add(rowdttemp);
        }


        private static void AddRowIntoDgTotal(DataTable dttemp, ref NormTotal kindProfAndRateTotal, ref DataRow rowdttemp, string beginKindPay)
        {
            rowdttemp = dttemp.NewRow();
            rowdttemp["KindPay"] = beginKindPay;
            rowdttemp["ItemCTN"] = (Decimal)Math.Round(kindProfAndRateTotal.ItemCTNSum, TrudoyomkostSettings.RoundNum);
            rowdttemp["PreparTimeCTN"] = (Decimal)Math.Round(kindProfAndRateTotal.PrTimeCTNSum, TrudoyomkostSettings.RoundNum);
            rowdttemp["ItemPayNorm"] = (Decimal)Math.Round(kindProfAndRateTotal.ItemPaySum, TrudoyomkostSettings.RoundNum);
            rowdttemp["PreparTimePayNorm"] = (Decimal)Math.Round(kindProfAndRateTotal.PrTimePaySum, TrudoyomkostSettings.RoundNum);
            rowdttemp["Valuation"] = Math.Round(kindProfAndRateTotal.ValuateSum, TrudoyomkostSettings.RoundNum);
            rowdttemp["ValPreparTime"] = Math.Round(kindProfAndRateTotal.ValuatePrTimeSum ?? 0, 4);
            dttemp.Rows.Add(rowdttemp);
        }

        private static void AddRowIntoDgTotal(DataTable dttemp, ref NormTotal kindProfAndRateTotal, ref DataRow rowdttemp, string beginKindPay, double beginWorkRate)
        {

            rowdttemp = dttemp.NewRow();

            rowdttemp["KindPay"] = beginKindPay;
            rowdttemp["WorkerRate"] = beginWorkRate;
            rowdttemp["ItemCTN"] = (Decimal)Math.Round(kindProfAndRateTotal.ItemCTNSum, TrudoyomkostSettings.RoundNum);
            rowdttemp["PreparTimeCTN"] = (Decimal)Math.Round(kindProfAndRateTotal.PrTimeCTNSum, TrudoyomkostSettings.RoundNum);
            rowdttemp["ItemPayNorm"] = (Decimal)Math.Round(kindProfAndRateTotal.ItemPaySum, TrudoyomkostSettings.RoundNum);
            rowdttemp["PreparTimePayNorm"] = (Decimal)Math.Round(kindProfAndRateTotal.PrTimePaySum, TrudoyomkostSettings.RoundNum);
            rowdttemp["Valuation"] = Math.Round(kindProfAndRateTotal.ValuateSum, TrudoyomkostSettings.RoundNum);
            rowdttemp["ValPreparTime"] = Math.Round(kindProfAndRateTotal.ValuatePrTimeSum ?? 0, 4);
            dttemp.Rows.Add(rowdttemp);
        }



    }



}
