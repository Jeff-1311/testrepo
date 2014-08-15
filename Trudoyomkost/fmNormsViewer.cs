using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Trudoyomkost
{
    public partial class fmNormsViewer : Form
    {




        private int _depNum = Properties.Settings.Default.DepNum;
        private fmOperationViewer _fmOperationViewer;
        private DataTable _dtOperApply = UserDataTables.CreateOperAppDt();
        private List<int> _operNumlst = new List<int>();

        Dictionary<int, DataTable> _dcApplyOper = new Dictionary<int, DataTable>();

        private TrudoyomkostDBDataSet.LabourNormDataTable _dtLabourNorm =
            new TrudoyomkostDBDataSet.LabourNormDataTable();

        public readonly int _infDetID;
        private int _seriaFrom;
        private int _seriaTo;
        private int _infProdChipher;
        private string _detNum;



        private int _currentRow;
        private int _currentLabNormId;
        private int _countPerProduct;
        private string _docNum;
        private ICommand _deleteLabourNormCmd;
        private ICommand _addWhereOperUseCmd;
        private ICommand _delWhereOperUseCmd;
        private ICommand _updateOldLabourCard;
        public DataTable TempdtOperApply;
        public string TempDepRegion;
        public string TempDocNum;
        private readonly Dictionary<string, short> _dcInfProducts;
        private WhereOperationUse _whereOperationUseItem;
        private LabourCardXtraRep newlab;

        private NormTotal _normTotalbyTheJob = new NormTotal();
        private NormTotal _normTotalByTheTime = new NormTotal();
        private short _selectProductChiper;
        private string _taskNum;

        private string _detName;




        #region Form Properties

        public Dictionary<int, DataTable> DcApplyOper
        {
            get { return _dcApplyOper; }
            set { _dcApplyOper = value; }
        }


        public int InfDetID
        {
            get { return _infDetID; }
        }

        public int CountPerProduct
        {
            get { return _countPerProduct; }
        }

        public Dictionary<string, short> DcInfProducts
        {
            get { return _dcInfProducts; }
        }

        public bool TimeInHour
        {
            get { return rbHours.Checked; }
        }

        public int CurrentRow
        {
            get { return _currentRow; }
        }

        public DataGridView DgOperViewer
        {
            get { return dgOperViewer; }
        }

        public DataTable DtOperApply
        {
            get { return _dtOperApply; }
        }

        #endregion

        public fmNormsViewer()
        {
            InitializeComponent();
        }

        #region Form Constructor



        public fmNormsViewer(string detNum, int infdetid, int seriaFrom, int seriaTo, int infproductCipher,
                             Dictionary<string, short> dcInfProducts, int detCount, string taskNum)
        {
            _detNum = detNum;
            _infDetID = infdetid;
            _seriaFrom = seriaFrom;
            _seriaTo = seriaTo;
            _infProdChipher = infproductCipher;
            _dcInfProducts = dcInfProducts;
            _countPerProduct = detCount;
            _taskNum = taskNum;

            _detName = connectSDF(@"Data Source=D:\Projects\трудоемкость2\Trudoyomkost\Trudoyomkost\bin\Debug\SPRNAM.sdf", detNum);

            // labelVsDetName.Text = connectSDF(@"Data Source=D:\Projects\трудоемкость2\Trudoyomkost\Trudoyomkost\bin\Debug\SPRNAM.sdf", detNum);

            //labelVsDetName.Text = "123";
            //= connectSDF(@"Data Source=D:\TrudoyomkostDB.sdf", detNum);



            InitializeComponent();
            InitializeFormElemets();
            IniztializeDataGrids();
        }

        #endregion

        #region InitializeFromElements

        public void InitializeFormElemets()
        {

            Type t = _normTotalbyTheJob.GetType();
            PropertyInfo[] pinfo = t.GetProperties();
            int i = 0;
            foreach (var p in pinfo)
            {
                dgTotal.Columns.Add(p.Name, "");
                dgTotal.Columns[i].Width = 60;
                i++;
            }

            foreach (var item in FillTrudoyomkostDB.DcMaxApply)
            {
                _lboxMaxSeriaInfo.Items.Add(item.Key + "   Серия С " +
                                           MathFunctionForSeries.GetStringSeriaNumber(item.Value.SeriaFrom)
                                           + "   Серия По " +
                                           MathFunctionForSeries.GetStringSeriaNumber(item.Value.SeriaTo));
            }


            LinqQueryForTrudoyomkost.FilldtNormViewer(_seriaFrom, _seriaTo, _infProdChipher,
                                                      ref _dtLabourNorm, rbHours.Checked, ref _normTotalbyTheJob, ref _normTotalByTheTime);

            LinqQueryForTrudoyomkost.FilldcOperApply(ref _dcApplyOper);
            UpdateDgTotal();

            cboxTariff.DataSource = FillTrudoyomkostDB.tariffList;
            cboxTariff.SelectedIndex = cboxTariff.Items.IndexOf(TrudoyomkostSettings.TariffNetNum);
            cboxTariff.SelectedValueChanged += new System.EventHandler(cboxTariff_SelectedValueChanged);

            //Filing _dcApplyOper during Form loading...

            dgOperViewer.DataSource = _dtLabourNorm;
            dgOperApply.DataSource = _dtOperApply;
            lbDetNum.Text = _detNum;

            labelVsDetName.Text = _detName;
            //  labelVsDetName.Text = _detName;

            vtbSeriaFrom.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbSeriaFrom.ValidateValue = VtboxMethods.CheckSeria;
            vtbSeriaTo.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbSeriaTo.ValidateValue = VtboxMethods.CheckSeria;
            vtbProdName.ValidateValue = CheckProdNume;
            vtbProdName.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
        }

        #endregion


        static public string connectSDF(string pathVsName, string ktcName)
        {

            try
            {
                //Подключаем sdf
                string connStr = pathVsName;
                BindingSource bindingSorce = new BindingSource();
                SqlCeConnection coon = new SqlCeConnection(connStr);
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.Connection = coon;
                cmd.CommandText = @"SELECT * FROM sprnam WHERE ktcName = " + "'" + ktcName + "'";
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                bindingSorce.DataSource = dataset.Tables[0];
                coon.Close();
                return dataset.Tables[0].Rows[0]["name"].ToString();
            }
            catch
            {

                return "Ошибка: не найдено соответствие";
            }

        }


        #region InizializeDataGrids

        private void IniztializeDataGrids()
        {
            DataColumn dcCheck = new DataColumn("Выбор", typeof(bool));
            _dtLabourNorm.Columns.Add(dcCheck);
            for (int i = 0; i < dgOperViewer.Columns.Count - 2; i++)
            {
                dgOperViewer.Columns[i].ReadOnly = true;
            }

            SetSizeDg(dgOperViewer);
            CheckDelOper();
        }

        public void CheckDelOper()
        {
            if (dgOperApply.Rows.Count > 1)
            {
                btApplyDel.Enabled = true;
            }
            else
            {
                btApplyDel.Enabled = false;
            }
        }

        public void SetSizeDg(DataGridView dgOperViewer)
        {
            dgOperViewer.ColumnHeadersHeight = 50;
            dgOperViewer.Columns[2].HeaderText = "Номер операции";
            dgOperViewer.Columns[3].HeaderText = "Уча- сток";
            dgOperViewer.Columns[4].HeaderText = "Код професии";
            dgOperViewer.Columns[5].HeaderText = "Наименование вида робот";
            dgOperViewer.Columns[6].HeaderText = "Т-ф";
            dgOperViewer.Columns[7].HeaderText = "В/О";
            dgOperViewer.Columns[8].HeaderText = "Роз- ряд";
            dgOperViewer.Columns[9].HeaderText = "РТН шт.";
            dgOperViewer.Columns[10].HeaderText = "РТН ПЗВ";
            dgOperViewer.Columns[11].HeaderText = "Плат. шт.";
            dgOperViewer.Columns[12].HeaderText = "Плат. ПЗВ";
            dgOperViewer.Columns[13].HeaderText = "Расценок";
            dgOperViewer.Columns[14].HeaderText = "Расц. ПЗВ";
            dgOperViewer.Columns[15].HeaderText = "Пов.коф. РТН";
            dgOperViewer.Columns[16].HeaderText = "№ документа";
            dgOperViewer.Columns[0].Visible = false;
            dgOperViewer.Columns[1].Visible = false;

            dgOperViewer.Columns[17].Visible = false;
            dgOperViewer.Columns[18].Visible = false;

            dgOperViewer.Columns[2].Width = 140;
            dgOperViewer.Columns[3].Width = 35;
            dgOperViewer.Columns[4].Width = 43;
            dgOperViewer.Columns[6].Width = 20;
            dgOperViewer.Columns[7].Width = 35;
            dgOperViewer.Columns[8].Width = 35;
            dgOperViewer.Columns[9].Width = 60;
            dgOperViewer.Columns[10].Width = 60;
            dgOperViewer.Columns[11].Width = 60;
            dgOperViewer.Columns[12].Width = 60;
            dgOperViewer.Columns[13].Width = 60;
            dgOperViewer.Columns[14].Width = 60;
            dgOperViewer.Columns[15].Width = 60;
            dgOperViewer.Columns[19].Width = 40;
            dgOperViewer.Columns[19].ReadOnly = false;

            dgOperApply.Columns[1].Width = 140;
            dgOperApply.Columns[2].Width = 140;
        }



        public void UpdateApplyDataGrid()
        {

            LinqQueryForTrudoyomkost.FillLabourListForSelectDet(_infDetID, ref FillTrudoyomkostDB.CurrentLabourNormList);
            if (TrudoyomkostSettings.IsAggregateDep && _taskNum != "")
                LinqQueryForTrudoyomkost.SelectLabourNormTaskNum(_taskNum);

            LinqQueryForTrudoyomkost.FilldcOperApply(ref _dcApplyOper);
            UpdateDgTotal();
            dgOperViewer_RowEnter(this.dgOperViewer, new DataGridViewCellEventArgs(0, 0));

        }
        public void UpdateDataGrids()
        {
            LinqQueryForTrudoyomkost.FillLabourListForSelectDet(_infDetID, ref FillTrudoyomkostDB.CurrentLabourNormList);
            if (TrudoyomkostSettings.IsAggregateDep && _taskNum != "")
                LinqQueryForTrudoyomkost.SelectLabourNormTaskNum(_taskNum);

            LinqQueryForTrudoyomkost.FilldcOperApply(ref _dcApplyOper);
            LinqQueryForTrudoyomkost.FilldtNormViewer(_seriaFrom, _seriaTo, _infProdChipher, ref _dtLabourNorm, rbHours.Checked, ref _normTotalbyTheJob, ref _normTotalByTheTime);
            UpdateDgTotal();
            dgOperViewer_RowEnter(this.dgOperViewer, new DataGridViewCellEventArgs(0, 0));
        }

        #endregion

        private void UpdateDgTotal()
        {
            dgTotal.Rows.Clear();
            _normTotalbyTheJob.SetValueIntoDg(dgTotal);
            _normTotalByTheTime.SetValueIntoDg(dgTotal);
        }



        private bool CheckProdNume(string productName)
        {
            if (!_dcInfProducts.ContainsKey(productName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(productName))
            {
                return false;
            }
            foreach (DataGridViewRow item in dgOperApply.Rows)
            {
                if (item.Cells[0].Value.ToString().Equals(productName))
                {
                    return false;
                }
            }

            return true;
        }
        #region dataGrid OperViewer Event Row Enter

        private void dgOperViewer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgtemp;
            dgtemp = sender as DataGridView;


            if (dgtemp != null)
            {
                if (e.RowIndex >= 0 && dgtemp.Rows.Count > 0)
                {

                    _docNum = dgOperViewer.Rows[e.RowIndex].Cells[16].Value.ToString();
                    btOperEdit.Enabled = true;
                    if (!pAddApplier.Enabled)
                        pAddApplier.Enabled = true;
                    btDoubl.Enabled = true;


                    _currentRow = e.RowIndex;

                    _currentLabNormId = (int)dgOperViewer.Rows[_currentRow].Cells[0].Value;

                    if (_dcApplyOper.ContainsKey(_currentLabNormId))
                    {
                        _dtOperApply = _dcApplyOper[_currentLabNormId];
                        dgOperApply.DataSource = _dtOperApply;
                    }
                }
                CheckDelOper();
            }
        }

        #endregion


        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Handler Apply NormCard to Other Product

        private void btAddApply_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow dgrow in dgOperApply.Rows)
            {
                if (dgrow.Cells[0].Value.ToString() == vtbProdName.ValueTxt)
                    return;

            }

            if (!vtbProdName.IsValid || !vtbSeriaFrom.IsValid)
            {
                return;
            }
            if (!FillTrudoyomkostDB.DcMaxApply.ContainsKey(vtbProdName.ValueTxt))
            {
                return;
            }


            int seriaFrom = MathFunctionForSeries.GetIntSeriaNumber(vtbSeriaFrom.ValueTxt);
            int seriaTo = MathFunctionForSeries.GetIntSeriaNumber(vtbSeriaTo.ValueTxt);
            short prodChipher = _dcInfProducts[vtbProdName.ValueTxt];
            _updateOldLabourCard = new UpdateOldNormCardCommand(seriaFrom, prodChipher);
            _updateOldLabourCard.execute();
            if (_operNumlst.Count == 0 && _dtLabourNorm.Rows.Count > 0)
            {

                foreach (DataRow tRow in _dtLabourNorm.Rows)
                {
                    _operNumlst.Add((int)tRow[0]);
                }

            }

            foreach (var item in _operNumlst)
            {
                bool IsApplyProduct = true;
                foreach (DataRow itemrow in _dcApplyOper[item].Rows)
                {
                    if (vtbProdName.ValueTxt.Equals(itemrow[0]))
                    {
                        IsApplyProduct = false;
                        break;
                    }
                }

                if (IsApplyProduct)
                {
                    _whereOperationUseItem = new WhereOperationUse(item, seriaFrom, seriaTo, prodChipher);
                    _addWhereOperUseCmd = new AddWhereOperUseCommand(_whereOperationUseItem);
                    _addWhereOperUseCmd.execute();
                }

            }

            UpdateDataGrids();
            CheckDelOper();
            _operNumlst.Clear();
        }

        #endregion

        #region panelNormsEdit Create fmOperationViewer

        private void btOperEdit_Click(object sender, EventArgs e)
        {
            _fmOperationViewer = new fmOperationViewer(_countPerProduct, this);
            _fmOperationViewer.Show();
        }

        private void btAddOperation_Click(object sender, EventArgs e)
        {
            _fmOperationViewer = new fmOperationViewer(this);
            _fmOperationViewer.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_operNumlst.Count == 0)
                return;
            this.Enabled = false;
            foreach (var item in _operNumlst)
            {
                _deleteLabourNormCmd = new DeleteLabourNormCommand(item);
                _deleteLabourNormCmd.execute();
            }

            UpdateDataGrids();
            _operNumlst.Clear();
            this.Enabled = true;
        }

        #endregion


        #region Handler Cheaked Minutes or Hours

        private void rbMinutes_MouseCaptureChanged(object sender, EventArgs e)
        {
            UpdateDataGrids();
        }
        private void rbHours_MouseCaptureChanged(object sender, EventArgs e)
        {
            UpdateDataGrids();
        }

        #endregion


        private void btnDyblirovanie_Click(object sender, EventArgs e)
        {
            if (dgOperViewer.Rows.Count > 0)
            {
                CreatePnDoubleNorm();
            }

        }

        public void CreatePnDoubleNorm()
        {
            fmDoubleNorms fmDoubleNorms = new fmDoubleNorms(this);
            fmDoubleNorms.ShowDialog();
        }

        #region Create Report With Labour Norm

        private void btViewLaborCard_Click(object sender, EventArgs e)
        {
            newlab = new LabourCardXtraRep();

            InitilizeReportElement();
            newlab.ShowPreviewDialog();


            newlab = new LabourCardXtraRep(true);
            InitilizeReportElement();
            newlab.ShowPreviewDialog();

            newlab = new LabourCardXtraRep(false);
            InitilizeReportElement();
            newlab.ShowPreviewDialog();

        }


        public void InitilizeReportElement()
        {



            int i = 0;
            if (_dtOperApply.Rows.Count > 0)
            {
                newlab.dgOperApply.Rows.Add(_dtOperApply.Rows.Count);

                foreach (DataRow trow in _dtOperApply.Rows)
                {
                    string prodNum = trow[0].ToString();
                    newlab.dgOperApply[0, i].Value = prodNum;

                    newlab.dgOperApply[1, i].Value = trow[1];
                    newlab.dgOperApply[2, i].Value = trow[2];
                    if (FillTrudoyomkostDB.DcMaxApply.ContainsKey(prodNum))
                        newlab.dgOperApply[3, i].Value = FillTrudoyomkostDB.DcMaxApply[prodNum].CountPerProd.ToString();
                    i++;
                }
            }

            newlab.dgOperApply.Height += newlab.dgOperApply.Rows.Count * 22;
            if (_dtLabourNorm.Rows.Count > 0)
            {
                newlab.dgOperViewer.Rows.Add(_dtLabourNorm.Rows.Count);
                i = 0;
                foreach (DataRow rData in _dtLabourNorm.Rows)
                {
                    newlab.dgOperViewer[0, i].Value = rData[2];
                    newlab.dgOperViewer[1, i].Value = rData[4];
                    newlab.dgOperViewer[2, i].Value = rData[5];
                    newlab.dgOperViewer[3, i].Value = rData[3];
                    newlab.dgOperViewer[4, i].Value = rData[6].ToString() + rData[7].ToString();
                    newlab.dgOperViewer[5, i].Value = rData[8];
                    newlab.dgOperViewer[6, i].Value = rData[9];
                    newlab.dgOperViewer[7, i].Value = rData[10];
                    newlab.dgOperViewer[8, i].Value = rData[15];
                    newlab.dgOperViewer[9, i].Value = rData[11];
                    newlab.dgOperViewer[10, i].Value = rData[13];
                    newlab.dgOperViewer[11, i].Value = rData[12];
                    newlab.dgOperViewer[12, i].Value = rData[14];


                    i++;

                }
            }
            newlab.InitializeNormTotals(_normTotalbyTheJob, _normTotalByTheTime);
            newlab.InitializeHeader(_depNum.ToString(), lbDetNum.Text, FillTrudoyomkostDB.NormMapNumber, _docNum, "1");

            newlab.dgOperViewer.Height += newlab.dgOperViewer.Rows.Count * 22;
            newlab.xrFooter.Top += dgOperViewer.Rows.Count * 23;
        }


        #endregion


        #region Handler For dgOperView CellContentClick

        private void dgOperViewer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 19)
            {
                AddRemovelstOperNum((int)dgOperViewer.Rows[e.RowIndex].Cells[0].Value, _operNumlst);
            }

        }

        public void AddRemovelstOperNum(int inputNum, List<int> operNumLst)
        {
            if (operNumLst.Contains(inputNum))
                operNumLst.Remove(inputNum);
            else
            {
                operNumLst.Add(inputNum);
            }
        }

        #endregion

        private void btApplyDel_Click(object sender, EventArgs e)
        {
            if (_operNumlst.Count > 0)
            {
                foreach (var item in _operNumlst)
                {
                    _whereOperationUseItem = new WhereOperationUse(item, 1, 1, _selectProductChiper);
                    _delWhereOperUseCmd = new DeleteWhereOperUseCommand(_whereOperationUseItem);
                    _delWhereOperUseCmd.execute();
                }
                this.UpdateDataGrids();
                _operNumlst.Clear();
            }
            else
            {
                _delWhereOperUseCmd = new DeleteWhereOperUseCommand(_whereOperationUseItem);
                _delWhereOperUseCmd.execute();
                this.UpdateApplyDataGrid();
            }
        }







        private void dgOperApply_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView tempdg = sender as DataGridView;

            if (tempdg != null)
            {
                if (tempdg.Rows.Count >= 1)
                {
                    int seriaFrom = MathFunctionForSeries.GetIntSeriaNumber(tempdg.Rows[e.RowIndex].Cells[1].Value.ToString());
                    int seriaTo = MathFunctionForSeries.GetIntSeriaNumber(tempdg.Rows[e.RowIndex].Cells[2].Value.ToString());
                    _selectProductChiper = FillTrudoyomkostDB.DcInfProducts[tempdg.Rows[e.RowIndex].Cells[0].Value.ToString()];

                    _whereOperationUseItem = new WhereOperationUse(_currentLabNormId, seriaFrom, seriaTo, _selectProductChiper);
                }
            }
        }

        private void vtbSeriaTo_Load(object sender, EventArgs e)
        {

        }

        private void vtbSeiraFrom_Load(object sender, EventArgs e)
        {

        }

        private void vtbProdName_Load(object sender, EventArgs e)
        {

        }

        private void cboxTariff_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox tempCbox = sender as ComboBox;
            if (tempCbox != null)
                TrudoyomkostSettings.TariffNetNum = (byte)tempCbox.SelectedValue;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new fmGroupUpdate(this)).ShowDialog();
        }

        bool IsfirstClosingAttempt = true;
        private void fmNormsViewer_FormClosing(object sender, FormClosingEventArgs e)//здесь осушествляется выход из формы
        {

            // if (IsfirstClosingAttempt )
            if (!TrudoyomkostSettings.IsAggregateDep)
            {
                if (dgOperViewer.Rows.Count == 0)
                    return;
                _operNumlst.Clear();
                foreach (DataRow tRow in _dtLabourNorm.Rows)
                {
                    _operNumlst.Add((int)tRow[0]);
                }

                List<string> tempList = new List<string>();

                var result = from operID in _operNumlst

                             join whelem in FillTrudoyomkostDB.WhereOperationUseList
                                 on operID equals whelem.LabourNormID
                             join prod in FillTrudoyomkostDB.DcInfProducts
                             on whelem.InfProductsChipher equals prod.Value
                             select new { prod.Key };


                foreach (var item in result)
                {
                    tempList.Add(item.Key);
                }



                foreach (var item in FillTrudoyomkostDB.DcMaxApply)
                {
                    List<string> tempListFindAll = tempList.FindAll(x => x == item.Key);
                    if (tempListFindAll.Count != _dtLabourNorm.Count)
                    {
                        _validateTimer.Enabled = true;
                        _validateTimer.Start();
                        e.Cancel = true;
                        IsfirstClosingAttempt = false;
                        return;
                    }
                }

                _validateTimer.Stop();
                //MessageBox.Show("Вы не можете выйти пока не применить норму на все изделия");
            }
            else
            {
                e.Cancel = false;
            }
        }
        private void _validateTimer_Tick(object sender, EventArgs e)
        {
            if (_lboxMaxSeriaInfo.Visible)
                _lboxMaxSeriaInfo.Visible = false;
            else
            {
                _lboxMaxSeriaInfo.Visible = true;
            }
        }





    }
}


