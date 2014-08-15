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
    public partial class fmOperationViewer : Form
    {
        private LabourNorm templabour = new LabourNorm();
        private string _isValidateGbNotific = Properties.Settings.Default.IsValidateGbNotific;
        private int _currentRow;
        private DataGridView _dgNorm;
        private DataTable _dtOperApply;
        private Dictionary<string, ShortProfInfo> _dcShortInfProf = new Dictionary<string, ShortProfInfo>();
        private Dictionary<InfTariffInfo, double> _dcInfTariffInfo = new Dictionary<InfTariffInfo, double>();
        private Dictionary<WhereOperUseStruct, WhereOperationUse> _oldAndNewApplyDict = new Dictionary<WhereOperUseStruct, WhereOperationUse>();
        private WhereOperUseStruct _oldWhereOperUse;
        private int _currentDgApplyIndex;
        private ShortProfInfo _shortProfInfo;
        private Dictionary<string, short> _dcInfProducts;
        private bool IsValidForm = true;
        private int _countPerProduct;
        private double _hourCost;
        private bool IsNewItem;
        private fmNormsViewer _parentForm;
        private bool _timeInHour;
        private AutoCompleteStringCollection _autoComSourceForDocNum = new AutoCompleteStringCollection();
        private int _newID;



        #region Constructor For New Form

        public fmOperationViewer(fmNormsViewer parentForm)
        {
            templabour.TariffNetNum = Properties.Settings.Default.TariffNetNum; //сдесь ставится тариф(наверное)
            _parentForm = parentForm;
            IsNewItem = true;
            _timeInHour = parentForm.TimeInHour;
            templabour.InfDetID = parentForm.InfDetID;
            _countPerProduct = parentForm.CountPerProduct;
            _dcInfProducts = parentForm.DcInfProducts;
            InitializeComponent();
            _dtOperApply = UserDataTables.CreateOperAppDt();
            dgOperApply.DataSource = _dtOperApply;
            InitializeFormElement();
            btUpdateForm.Enabled = false;
            btSaveAndLimit.Enabled = false;
            btLimitedOper.Visible = false;
            btNext.Enabled = false;
            btPrev.Enabled = false;
            vtbTaskNum.ValueTxt = "";
            vtbItemCTN.ValueTxt = "0";
            vtbPreparTimeCTN.ValueTxt = "0";
            cbKindPay.SelectedIndex = 0; //Вид оплаты, сдельно или повременно
        }

        #endregion

        #region Constructor For EditForm

        public fmOperationViewer(int countPerProduct, fmNormsViewer parentForm)
        {
            InitializeComponent();

            _parentForm = parentForm;
            _timeInHour = parentForm.TimeInHour;
            _currentRow = parentForm.CurrentRow;
            _dgNorm = parentForm.DgOperViewer;
            _dcInfProducts = parentForm.DcInfProducts;

            FillLabourNormFromDg(_dgNorm, _currentRow, ref templabour);
            InitializeFormElement();
            IsNewItem = false;
            _countPerProduct = countPerProduct;



            InitializeGbOperItem();
            btSaveAndLimit.Enabled = false;
            btSaveFrom.Enabled = false;
            vtbProdName.Enabled = false;
            _newID = FillTrudoyomkostDB.LabourNormList.Max().ID;
            _newID++;
        }

        #endregion

        #region  Fill Obj LabourNorm From DataGrid Row

        private void FillLabourNormFromDg(DataGridView dgNorm, int currentRow, ref LabourNorm templabour)
        {
            templabour.TariffNetNum = Properties.Settings.Default.TariffNetNum; //сдесь ставится тариф!!
            templabour.ID = (int)dgNorm.Rows[currentRow].Cells[0].Value;
            templabour.InfDetID = (int)dgNorm.Rows[currentRow].Cells[1].Value;
            templabour.OperNum = dgNorm.Rows[currentRow].Cells[2].Value.ToString();
            templabour.DepRegion = (double)dgNorm.Rows[currentRow].Cells[3].Value;
            templabour.ProfCode = (int)dgNorm.Rows[currentRow].Cells[4].Value;
            templabour.NameKindWork = dgNorm.Rows[currentRow].Cells[5].Value.ToString();
            templabour.KindPay = dgNorm.Rows[currentRow].Cells[7].Value.ToString();
            templabour.WorkerRate = (double)dgNorm.Rows[currentRow].Cells[8].Value;
            templabour.ItemCTN = (double)(Decimal)dgNorm.Rows[currentRow].Cells[9].Value;
            templabour.PreparTimeCTN = (double)(Decimal)dgNorm.Rows[currentRow].Cells[10].Value;
            templabour.ItemPayNorm = (double)(Decimal)dgNorm.Rows[currentRow].Cells[11].Value;
            templabour.PreparTimePayNorm = (double)(Decimal)dgNorm.Rows[currentRow].Cells[12].Value;
            templabour.CoeffCTN = (double)dgNorm.Rows[currentRow].Cells[15].Value;
            templabour.DocNum = dgNorm.Rows[currentRow].Cells[16].Value.ToString();
            templabour.TaskNumber = dgNorm.Rows[currentRow].Cells[18].Value.ToString();
            _dtOperApply = _parentForm.DcApplyOper[(int)dgNorm.Rows[currentRow].Cells[0].Value];
            dgOperApply.DataSource = _dtOperApply;


        }

        #endregion

        #region Initialize fmOperationViewer element

        private void InitializeFormElement()
        {
            this.Location = new Point(_parentForm.Location.X, _parentForm.Location.Y);


            vtbOperNum.ValidateValue = VtboxMethods.checkForNonEmpty;
            vtbTaskNum.ValidateValue = VtboxMethods.checkForNonEmpty;
            vtbWorkRate.ValidateValue = VtboxMethods.checkWorkRate;
            vtbWorkRate.StringAutoCorrectionMethod = VtboxMethods.correctForDouble;

            vtbItemCTN.ValidateValue = VtboxMethods.checkForDouble;
            vtbItemCTN.StringAutoCorrectionMethod = VtboxMethods.correctForDouble;
            vtbPreparTimeCTN.ValidateValue = VtboxMethods.checkForDouble;
            vtbPreparTimeCTN.StringAutoCorrectionMethod = VtboxMethods.correctForDouble;
            vtbCoeffCTN.ValidateValue = VtboxMethods.checkForDouble;
            vtbCoeffCTN.StringAutoCorrectionMethod = VtboxMethods.correctForDouble;

            vtbItemPayNorm.ValidateValue = VtboxMethods.checkForDouble;
            vtbItemPayNorm.StringAutoCorrectionMethod = VtboxMethods.correctForDouble;
            vtbPreparTimePayNorm.ValidateValue = VtboxMethods.checkForDouble;
            vtbPreparTimePayNorm.StringAutoCorrectionMethod = VtboxMethods.correctForDouble;

            vtbLoverCoeff.ValidateValue = VtboxMethods.checkForDouble;
            vtbLoverCoeff.StringAutoCorrectionMethod = VtboxMethods.correctForDouble;
            vtbDepRegion.ValidateValue = VtboxMethods.checkForDouble;
            vtbPreparTimePayNorm.StringAutoCorrectionMethod = VtboxMethods.correctForDouble;


            vtbSeriaFrom.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbSeriaFrom.ValidateValue = VtboxMethods.CheckSeria;
            vtbSeriaTo.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbSeriaTo.ValidateValue = VtboxMethods.CheckSeria;
            vtbProdName.ValidateValue = VtboxMethods.CheckProdNume;
            vtbProdName.StringAutoCorrectionMethod = VtboxMethods.correctForInt;

            using (var newLocalDb = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                LinqQueryForTrudoyomkost.FilldcInfProfession(newLocalDb, ref _dcShortInfProf, ref vcbProfCode);
                LinqQueryForTrudoyomkost.FilldcInfTariffInfo(newLocalDb, ref _dcInfTariffInfo);
            }
            vcbProfCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            vcbProfCode.AutoCompleteSource = AutoCompleteSource.ListItems;
            _autoComSourceForDocNum = LinqQueryForTrudoyomkost.FillDocNumlst();
            foreach (var item in _autoComSourceForDocNum)
            {
                cbDocNum.Items.Add(item);
            }

            cbDocNum.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbDocNum.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbDocNum.AutoCompleteCustomSource = _autoComSourceForDocNum;

            dgOperApply.Columns[1].Width = 120;
            dgOperApply.Columns[2].Width = 120;
            vtbSeriaTo.ValueTxt = " ";
            vtbLoverCoeff.ValueTxt = "1";
            vtbCoeffCTN.ValueTxt = "0";
            if (TrudoyomkostSettings.IsAggregateDep)
            {
                vtbTaskNum.Enabled = true;
            }

        }

        # endregion

        #region Update LabourNorm

        private void UpdateLabourNorm(ref LabourNorm labourNorm, bool timeInHour)
        {
            _shortProfInfo = _dcShortInfProf[vcbProfCode.SelectedItem.ToString()];
            labourNorm.OperNum = vtbOperNum.ValueTxt;
            labourNorm.DepRegion = double.Parse(vtbDepRegion.ValueTxt);
            labourNorm.ProfCode = _shortProfInfo.ProfCode;
            labourNorm.NameKindWork = _shortProfInfo.NameKindWork;
            labourNorm.KindPay = cbKindPay.SelectedItem.ToString();
            labourNorm.WorkerRate = double.Parse(vtbWorkRate.ValueTxt);
            if (timeInHour)
            {
                labourNorm.ItemCTN = double.Parse(vtbItemCTN.ValueTxt);
                labourNorm.PreparTimeCTN = double.Parse(vtbPreparTimeCTN.ValueTxt);
                labourNorm.ItemPayNorm = double.Parse(vtbItemPayNorm.ValueTxt);
                labourNorm.PreparTimePayNorm = double.Parse(vtbPreparTimePayNorm.ValueTxt);
            }
            else
            {
                labourNorm.ItemCTN = Math.Round(double.Parse(vtbItemCTN.ValueTxt) / 60, TrudoyomkostSettings.RoundNum);
                labourNorm.PreparTimeCTN = Math.Round(double.Parse(vtbPreparTimeCTN.ValueTxt) / 60, TrudoyomkostSettings.RoundNum);
                labourNorm.ItemPayNorm = Math.Round(double.Parse(vtbItemPayNorm.ValueTxt) / 60, TrudoyomkostSettings.RoundNum);
                labourNorm.PreparTimePayNorm = Math.Round(double.Parse(vtbPreparTimePayNorm.ValueTxt) / 60, TrudoyomkostSettings.RoundNum);
            }

            if (TrudoyomkostSettings.DepNum == 22 && labourNorm.ProfCode == 19433)
                labourNorm.ItemPayNorm = Math.Round((labourNorm.ItemPayNorm * 8) / 7.2, TrudoyomkostSettings.RoundNum);

            labourNorm.DocNum = cbDocNum.Text;
            labourNorm.Date = DateTime.Now;
            labourNorm.TaskNumber = vtbTaskNum.ValueTxt;

            _hourCost =
                _dcInfTariffInfo[
                    new InfTariffInfo(templabour.TariffNetNum, labourNorm.KindPay, labourNorm.WorkerRate)];
            labourNorm.CoeffCTN = MathFunctionForSeries.CalculateCoeffCTN(labourNorm.ItemPayNorm,
                                                                          labourNorm.PreparTimePayNorm,
                                                                          labourNorm.ItemCTN, labourNorm.PreparTimeCTN,
                                                                          _countPerProduct);
            labourNorm.Valuation = MathFunctionForSeries.CalculateValuation(labourNorm.ItemPayNorm, _hourCost);
            labourNorm.ValPreparTime = MathFunctionForSeries.CalculateValPrepareTime(labourNorm.PreparTimePayNorm,
                                                                                     _hourCost);

        }

        #endregion

        #region Update ItemPayNorm (Apply with LoverCoeff)

        public void UpdateItemPayNorm(ref LabourNorm laborNorm, bool timeInHour)
        {
            if (timeInHour)
                laborNorm.ItemPayNorm = double.Parse(vtbItemPayNorm.ValueTxt);
            else
            {
                laborNorm.ItemPayNorm = (double.Parse(vtbItemPayNorm.ValueTxt)) / 60;
            }
        }

        #endregion

        #region Initialize gbOperItem ,give ValueTxt - value and give other value gbox

        private void InitializeGbOperItem()
        {
            _shortProfInfo = new ShortProfInfo(templabour.ProfCode, templabour.NameKindWork);
            vcbProfCode.SelectedItem = templabour.ProfCode.ToString() + " " + templabour.NameKindWork;
            cbKindPay.SelectedItem = templabour.KindPay;

            vtbOperNum.ValueTxt = templabour.OperNum;
            vtbWorkRate.ValueTxt = templabour.WorkerRate.ToString();
            vtbDepRegion.ValueTxt = templabour.DepRegion.ToString();

            vtbItemCTN.ValueTxt = ((Decimal)templabour.ItemCTN).ToString();
            vtbPreparTimeCTN.ValueTxt = ((Decimal)templabour.PreparTimeCTN).ToString();
            vtbCoeffCTN.ValueTxt = ((Decimal)templabour.CoeffCTN).ToString();

            vtbItemPayNorm.ValueTxt = ((Decimal)templabour.ItemPayNorm).ToString();
            vtbPreparTimePayNorm.ValueTxt = ((Decimal)templabour.PreparTimePayNorm).ToString();

            cbDocNum.SelectedItem = templabour.DocNum;
            vtbTaskNum.ValueTxt = templabour.TaskNumber;

            dgOperApply_CellClick(dgOperApply, new DataGridViewCellEventArgs(0, 0));
            _oldAndNewApplyDict.Clear();
        }

        #endregion

        #region  Check ValidateForm

        private void CheckValidateForm(GroupBox gbInput)
        {
            ValidatingTextBox vtbtemp;
            GroupBox gbtemp;
            ComboBox cbtemp;
            DataGridView dgtemp;
            ValidatingComboBox vcbtemp;
            foreach (var item in gbInput.Controls)
            {
                vtbtemp = item as ValidatingTextBox;
                cbtemp = item as ComboBox;
                gbtemp = item as GroupBox;
                dgtemp = item as DataGridView;
                vcbtemp = item as ValidatingComboBox;

                if (vcbtemp != null)
                    if (!vcbtemp.IsValid)
                    {
                        IsValidForm = false;
                    }
                if (dgtemp != null)
                    if (dgtemp.Rows.Count == 0)
                        IsValidForm = false;

                if (gbtemp != null)
                    CheckValidateForm(gbtemp);
                if (cbtemp != null)
                    if (cbtemp.SelectedItem == null && string.IsNullOrEmpty(cbtemp.Text))
                    {
                        IsValidForm = false;
                    }

                if (vtbtemp != null && vtbtemp.Enabled)
                    if (!vtbtemp.IsValid)
                    {
                        IsValidForm = false;
                    }
            }
        }

        #endregion

        #region ClearForm
        public void ClearForm()
        {

            _parentForm.TempdtOperApply = _dtOperApply;
            _parentForm.TempDepRegion = vtbDepRegion.ValueTxt;

         



            _parentForm.TempDocNum = (cbDocNum.SelectedItem != null) ? cbDocNum.SelectedItem.ToString() : cbDocNum.Text;

            this.Close();
            fmOperationViewer newOpertion = new fmOperationViewer(_parentForm);
            newOpertion._dtOperApply = _parentForm.TempdtOperApply;
            newOpertion.dgOperApply.DataSource = _dtOperApply;
            newOpertion.vtbDepRegion.ValueTxt = _parentForm.TempDepRegion;

            newOpertion.cbDocNum.SelectedIndex = _autoComSourceForDocNum.IndexOf(_parentForm.TempDocNum);
            cbKindPay.SelectedIndex = 0;
            newOpertion.Show();
        }
        #endregion

        #region Function For Cheking Validate Product Name

        #endregion

        #region Handler PageUp, PageDown Keys

        private void fmOperationViewer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
                btNext_Click(this, new EventArgs());
            if (e.KeyCode == Keys.PageDown)
                btPrev_Click(this, new EventArgs());
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        #endregion

        #region Handler Event btNex, btPrev

        private void btNext_Click(object sender, EventArgs e)
        {
            if (_dgNorm.Rows.Count - 1 != _currentRow)
                FillLabourNormFromDg(_dgNorm, ++_currentRow, ref templabour);
            InitializeGbOperItem();
        }

        private void btPrev_Click(object sender, EventArgs e)
        {
            if (_currentRow > 0)
                FillLabourNormFromDg(_dgNorm, --_currentRow, ref templabour);
            InitializeGbOperItem();
        }

        #endregion


        #region Save, Upadate, Save With Old Operation Limit Click
        private void btLimitedOper_MouseClick(object sender, MouseEventArgs e)
        {

            if (!vtbProdName.IsValid || !vtbSeriaFrom.IsValid || !vtbSeriaTo.IsValid)
                return;
            if (MathFunctionForSeries.GetIntSeriaNumber(vtbSeriaFrom.ValueTxt) > MathFunctionForSeries.GetIntSeriaNumber(vtbSeriaTo.ValueTxt))
                return;

            foreach (DataRow itemRow in _dtOperApply.Rows)
            {
                if ((MathFunctionForSeries.GetIntSeriaNumber(itemRow[1].ToString()) >= MathFunctionForSeries.GetIntSeriaNumber(vtbSeriaFrom.ValueTxt)) && itemRow[0].ToString().Equals(vtbProdName.ValueTxt))
                    return;
            }


            int seriaTo = MathFunctionForSeries.GetIntSeriaNumber(vtbSeriaTo.ValueTxt);
            int seriaFrom = MathFunctionForSeries.GetIntSeriaNumber(vtbSeriaFrom.ValueTxt);
            short prodChiper = _dcInfProducts[vtbProdName.ValueTxt];
            WhereOperationUse tempOperUse = new WhereOperationUse(_newID, seriaFrom, seriaTo, prodChiper);
            WhereOperationUse exsititem = _oldAndNewApplyDict.Values.ToList().Find(item => item.InfProductsChipher == prodChiper);

            if (exsititem == null)
            {
                _oldAndNewApplyDict.Add(_oldWhereOperUse, tempOperUse);
                _dtOperApply.Rows.RemoveAt(_currentDgApplyIndex);
                _dtOperApply.Rows.Add(vtbProdName.ValueTxt, vtbSeriaFrom.ValueTxt, vtbSeriaTo.ValueTxt);
            }

            btSaveFrom.Enabled = false;
            btUpdateForm.Enabled = false;
            btSaveAndLimit.Enabled = true;

        }


        private void btnSaveForm_Click(object sender, EventArgs e)
        {

            CheckValidateForm(gbOperItems);
            if (!IsValidForm)
            {
                MessageBox.Show(_isValidateGbNotific);
                IsValidForm = true;
            }
            else
            {
                using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
                {
                    btSaveFrom.Enabled = false;



                    int Id = currentContext.LabourNorm.ToList().Max().ID;

                    Id++;
                    templabour.ID = Id;


                    UpdateLabourNorm(ref templabour, _timeInHour);
                    InitializeGbOperItem();

                    FillTrudoyomkostDB.LabourNormList.Add(templabour);
                    FillTrudoyomkostDB.LabourNormTableAdapter.InsertQuery(templabour.InfDetID, templabour.OperNum, templabour.DepRegion, templabour.ProfCode, templabour.NameKindWork,
                        templabour.TariffNetNum, templabour.KindPay, templabour.WorkerRate, templabour.ItemCTN, templabour.PreparTimeCTN,
                        templabour.ItemPayNorm, templabour.PreparTimePayNorm, templabour.Valuation, templabour.ValPreparTime, templabour.CoeffCTN, templabour.DocNum, templabour.Date, templabour.TaskNumber);


                    int WhereUseId = currentContext.WhereOperationUse.ToList().Last().Id;

                    foreach (DataRow row in _dtOperApply.Rows)
                    {
                        WhereUseId++;
                        int seriaFrom = MathFunctionForSeries.GetIntSeriaNumber(row[1].ToString());
                        int seriaTo = MathFunctionForSeries.GetIntSeriaNumber(row[2].ToString());
                        short prodChipher = _dcInfProducts[row[0].ToString()];
                        WhereOperationUse _whereOperUseitem = new WhereOperationUse(templabour.ID, seriaFrom, seriaTo, prodChipher, WhereUseId);

                        FillTrudoyomkostDB.WhereOperationUseList.Add(_whereOperUseitem);
                        FillTrudoyomkostDB.WhereOperationUseTableAdapter.InsertQuery(templabour.ID, seriaFrom, seriaTo, prodChipher);
                    }

                    _parentForm.UpdateDataGrids();

                    ClearForm();
                    btSaveFrom.Enabled = true;

                }
            }
        }

        private void btUpdateForm_Click(object sender, EventArgs e)
        {
            CheckValidateForm(gbOperItems);
            if (!IsValidForm)
            {
                MessageBox.Show(_isValidateGbNotific);
                IsValidForm = true;
            }
            else
            {
                using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
                {
                    btUpdateForm.Enabled = false;
                    UpdateLabourNorm(ref templabour, _timeInHour);



                    LabourNorm item2 = FillTrudoyomkostDB.LabourNormList.Find(itemEl => itemEl.ID == templabour.ID);
                    if (FillTrudoyomkostDB.LabourNormList.Contains(item2))
                    {
                        FillTrudoyomkostDB.LabourNormList.Remove(item2);
                        FillTrudoyomkostDB.LabourNormList.Add(templabour.Copy());


                        FillTrudoyomkostDB.LabourNormTableAdapter.UpdateQuery(templabour.OperNum, templabour.DepRegion,
                                                                         templabour.ProfCode, templabour.NameKindWork,
                                                                         templabour.TariffNetNum, templabour.KindPay,
                                                                         templabour.WorkerRate, templabour.ItemCTN,
                                                                         templabour.PreparTimeCTN,
                                                                         templabour.ItemPayNorm,
                                                                         templabour.PreparTimePayNorm,
                                                                         templabour.Valuation,
                                                                         templabour.ValPreparTime,
                                                                         templabour.CoeffCTN, templabour.DocNum,
                                                                         templabour.Date,
                                                                         templabour.TaskNumber,
                                                                         templabour.ID);

                    }

                    _parentForm.UpdateDataGrids();
                    btUpdateForm.Enabled = true;

                }
            }
        }


        private void btSaveAndLimit_Click(object sender, EventArgs e)
        {

            CheckValidateForm(gbOperItems);
            if (!IsValidForm)
            {
                MessageBox.Show(_isValidateGbNotific);
                IsValidForm = true;
                return;
            }
            using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                foreach (var item in _oldAndNewApplyDict)
                {
                    WhereOperationUse tempItem = new WhereOperationUse();
                    tempItem = FillTrudoyomkostDB.WhereOperationUseList.ToList().Find(currItem => (currItem.SeriaFrom == item.Key.SeriaFrom && currItem.SeriaTo == item.Key.SeriaTo && currItem.InfProductsChipher == item.Key.ProductChipher && currItem.LabourNormID == templabour.ID));
                    tempItem.SeriaTo = item.Value.SeriaFrom - 1;
                    FillTrudoyomkostDB.WhereOperationUseTableAdapter.UpdateSeriaTo(tempItem.SeriaTo, item.Key.ProductChipher, item.Key.SeriaFrom, templabour.ID);

                    FillTrudoyomkostDB.WhereOperationUseList.Add(item.Value);
                    currentContext.WhereOperationUse.InsertOnSubmit(item.Value);
                    currentContext.SubmitChanges();
                }

                templabour.ID = _newID;


                btSaveAndLimit.Enabled = false;
                UpdateLabourNorm(ref templabour, _timeInHour);


                FillTrudoyomkostDB.LabourNormTableAdapter.InsertQuery(templabour.InfDetID, templabour.OperNum, templabour.DepRegion, templabour.ProfCode, templabour.NameKindWork,
                    templabour.TariffNetNum, templabour.KindPay, templabour.WorkerRate, templabour.ItemCTN, templabour.PreparTimeCTN,
                    templabour.ItemPayNorm, templabour.PreparTimePayNorm, templabour.Valuation, templabour.ValPreparTime, templabour.CoeffCTN, templabour.DocNum, templabour.Date, templabour.TaskNumber);


                FillTrudoyomkostDB.LabourNormList.Add(templabour);
                templabour = new LabourNorm();

            }

            _parentForm.UpdateDataGrids();
            _newID++;
            btSaveAndLimit.Enabled = true;
        }



        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Checking LoverCoeff AND Count LoverCoeff

        public void CheckLoverCoeff()
        {
            if (string.IsNullOrEmpty(vtbItemCTN.ValueTxt) || string.IsNullOrEmpty(vtbPreparTimeCTN.ValueTxt))
                gbLoverCoff.Enabled = false;

            else if (double.Parse(vtbItemCTN.ValueTxt) != 0 && double.Parse(vtbPreparTimeCTN.ValueTxt) != 0)
                gbLoverCoff.Enabled = true;

            else
            {
                gbLoverCoff.Enabled = false;
            }

        }


        private void btLovCofCount_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(vtbItemPayNorm.ValueTxt) && !string.IsNullOrEmpty(vtbLoverCoeff.ValueTxt))
            {
                vtbItemPayNorm.ValueTxt = MathFunctionForSeries.ReCalcItemPayNorm(double.Parse(vtbLoverCoeff.ValueTxt),
                                                                                    double.Parse(vtbItemPayNorm.ValueTxt)).ToString();

                UpdateItemPayNorm(ref templabour, _timeInHour);
            }
        }

        #endregion


        private void vtbCoeffCTN_Validated(object sender, EventArgs e)
        {
            double CoeffCTN = 0;
            double.TryParse(vtbCoeffCTN.ValueTxt, out CoeffCTN);
            if (CoeffCTN == 1 && !string.IsNullOrEmpty(vtbItemCTN.ValueTxt) &&
                !string.IsNullOrEmpty(vtbPreparTimeCTN.ValueTxt))
            {
                vtbItemPayNorm.ValueTxt = vtbItemCTN.ValueTxt;
                vtbPreparTimePayNorm.ValueTxt = vtbPreparTimeCTN.ValueTxt;
            }
            if (!string.IsNullOrEmpty(vtbItemCTN.ValueTxt) && CoeffCTN != 0)
            {

                vtbItemPayNorm.ValueTxt = Math.Round(((double.Parse(vtbItemCTN.ValueTxt)) * CoeffCTN), TrudoyomkostSettings.RoundNum).ToString();
            }

        }



        private void btAddOper_MouseClick(object sender, MouseEventArgs e) //кнопка добавить
        {
            if (!vtbProdName.IsValid || !vtbSeriaFrom.IsValid || !vtbSeriaTo.IsValid)
                return;
            bool IsDublProdName = false;
            foreach (DataGridViewRow itemRow in dgOperApply.Rows)
            {
                if (itemRow.Cells[0].Value.ToString().Equals(vtbProdName.ValueTxt))
                    IsDublProdName = true;
            }
            if (!IsDublProdName)
            {
                UserDataTables.AddRowToApplyDt(vtbProdName.ValueTxt, vtbSeriaFrom.ValueTxt, vtbSeriaTo.ValueTxt, _dtOperApply);
            }
        }



        private void cbProfCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox cbinput = sender as ComboBox;
            string text = cbinput.Text;
            if (!Char.IsDigit(e.KeyChar))
            {
                cbinput.Text = "";
            }
        }


        private void dgOperApply_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
            {
                return;
            }

            vtbProdName.ValueTxt = dgOperApply.Rows[e.RowIndex].Cells[0].Value.ToString();
            int seriaFrom = MathFunctionForSeries.GetIntSeriaNumber(dgOperApply.Rows[e.RowIndex].Cells[1].Value.ToString());
            int seriaTo = MathFunctionForSeries.GetIntSeriaNumber(dgOperApply.Rows[e.RowIndex].Cells[2].Value.ToString());
            short productChipher = _dcInfProducts[dgOperApply.Rows[e.RowIndex].Cells[0].Value.ToString()];
            _oldWhereOperUse = new WhereOperUseStruct(templabour.ID, seriaFrom, seriaTo, productChipher);
            _currentDgApplyIndex = e.RowIndex;

        }












    }
}