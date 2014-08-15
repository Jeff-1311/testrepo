using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;


using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace Trudoyomkost
{
    public partial class mainForm : Form
    {
        private fmNormsViewer _newfmNormsViewer;
        private Dictionary<string, int> dcIDDetNum = FillTrudoyomkostDB.DicDetNumAndId;
        private Dictionary<string, short> _infProductsDc;
        private Dictionary<string, MaxApply> _dcMaxApply = FillTrudoyomkostDB.DcMaxApply;
        private IList<string> _detNumList = new List<string>();
        private List<WhereUse> _whereUseList;
        private List<InfDepList> _depList;
        private List<InfDet> _infDetList;
        private List<string> _taskNumbers;
        fmAuthentication fmAuthent;

        private DataTable _dtWhereUse;
        private int _seriaFrom;
        private int _seriaTo;
        private int _selectProdCipher;
        private int _selectDetCount;
        private int _detNumID;
        private int _currentProfCode;
        private string _depProducer;
        private string _selectTaskNumber;
        public Users CurrentUser;
        public bool IsAuthentication = false;
        private AutoCompleteStringCollection _autoComSourceForDetNumlst = new AutoCompleteStringCollection();
        private TrudoyomkostDBDataSet.infProfessionDataTable _dtInfProf =
           new TrudoyomkostDBDataSet.infProfessionDataTable();
        private TrudoyomkostDBDataSet.infProductsDataTable _infProductsDt =
            new TrudoyomkostDBDataSet.infProductsDataTable();
        #region Constor mainForm

        public mainForm()
        {
            InitializeComponent();
            InitializeFormElemets();
            fmAuthent = new fmAuthentication(this);

            labelDateCreateAndVersion.Text = "Дата создания БД: " + File.GetCreationTime(Environment.CurrentDirectory + @"\TrudoyomkostDB.sdf").ToString() + "       Версия программы: " + Properties.Settings.Default.Version;
        }

        #endregion

        #region Initialize Form Elements

        private void InitializeFormElemets()
        {
            UserDataTables.CreatetbWhereUse(ref _dtWhereUse);

            dgWhereUse.DataSource = _dtWhereUse;
            dgWhereUse.Columns[0].HeaderText = "Серия С -КТС";
            dgWhereUse.Columns[1].HeaderText = "Серия По -КТС";
            dgWhereUse.Columns[2].HeaderText = "Изделие";
            dgWhereUse.Columns[3].HeaderText = "Кол-во";
            dgWhereUse.Columns[4].Visible = false;
            dgWhereUse.Columns[5].Visible = false;
            dgWhereUse.Columns[6].Visible = false;

            tbRoundNum.Text = TrudoyomkostSettings.RoundNum.ToString();
            tbDepNum.Text = TrudoyomkostSettings.DepNum.ToString();
            dgProfesion.DataSource = _dtInfProf;
            AutoCompaleTextBox();
            vtbSeriaFrom.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbSeriaFrom.ValidateValue = VtboxMethods.CheckSeria;
            vtbSeriaTo.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbSeriaTo.ValidateValue = VtboxMethods.CheckSeria;
            vtbProfCode.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbProfCode.ValidateValue = VtboxMethods.checkForInt;

            dgProfesion.Columns[0].Width = 50;
            dgProfesion.Columns[1].Width = 160;
            dgProfesion.Columns[2].Width = 160;
            dgProfesion.Columns[3].Visible = false;

        }

        #endregion

        #region Загрузка данных в програму
        private void FillFormData()
        {
            using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                LinqQueryForTrudoyomkost.FillInfProductsList(currentContext, ref FillTrudoyomkostDB.infProductList);
                LinqQueryForTrudoyomkost.FillLabourNormList(currentContext, ref FillTrudoyomkostDB.LabourNormList);
                LinqQueryForTrudoyomkost.FillWhereOperationUseList(currentContext, ref FillTrudoyomkostDB.WhereOperationUseList);

                _whereUseList = LinqQueryForTrudoyomkost.FillWhereUselst(currentContext);
                _depList = LinqQueryForTrudoyomkost.FillinfDeplst(currentContext);
                _infDetList = LinqQueryForTrudoyomkost.FillinfDetList(currentContext);

                FillTrudoyomkostDB.whereUseList = _whereUseList;
                FillTrudoyomkostDB.infDetList = _infDetList;

                LinqQueryForTrudoyomkost.FilldcInfProducts(currentContext, ref FillTrudoyomkostDB.DcInfProducts);

                _infProductsDc = FillTrudoyomkostDB.DcInfProducts;

                LinqQueryForTrudoyomkost.FilldcDetNumForProduct(ref dcIDDetNum, _infProductsDc.Values.First());
                LinqQueryForTrudoyomkost.FilldtInfProf(currentContext, ref _dtInfProf);
                LinqQueryForTrudoyomkost.GetInfTariffList(currentContext, ref FillTrudoyomkostDB.tariffList);
                LinqQueryForTrudoyomkost.FillDictDepIDCode(currentContext, ref FillTrudoyomkostDB.DicDepCodeAndId);

                _detNumList.Clear();
                foreach (var item in dcIDDetNum)
                {
                    _detNumList.Add(item.Key);
                }
            }//

        }
        #endregion
        private void btFillFormData_Click(object sender, EventArgs e)
        {
            fmAuthent.ShowDialog();
            btViewDetNumNorms.Enabled = false;

        }

        private void authorizationBackGround_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            FillFormData();
        }

        private void authorizationBackGround_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            foreach (var item in _infProductsDc)
            {
                cbSelectProduct.Items.Add(item.Key);
            }

            cbSelectProduct.SelectedIndex = 0;
            _selectProdCipher = _infProductsDc[cbSelectProduct.SelectedItem.ToString()];

            SetlboxDetNum();

            this.cbSelectProduct.SelectedIndexChanged += new System.EventHandler(this.cbSelectProduct_SelectedIndexChanged);
            loadingBox.Visible = false;
            loadingBox.Dispose();
            btSearchDetNum.Enabled = true;
            lboxDetNum.Enabled = true;
            if (TrudoyomkostSettings.IsAggregateDep)
                gboxAgrDep.Visible = true;
            checkBox3.Checked = TrudoyomkostSettings.IsAggregateDep;
        }


        #region Fill ComboBox with list DetNum for Select Product
        private void cbSelectProduct_SelectedIndexChanged(object sender, EventArgs e)//выбор изделия
        {
            _selectProdCipher = _infProductsDc[cbSelectProduct.SelectedItem.ToString()]; //шифр изделия, например 17 у 158го изделия
            LinqQueryForTrudoyomkost.FilldcDetNumForProduct(ref dcIDDetNum, _selectProdCipher);//что за dcIDDetNum???
            _detNumList.Clear(); 
            _detNumList = dcIDDetNum.Keys.ToList();// это будет выведенно 

            SetlboxDetNum();
        }
        #endregion
        public void SetlboxDetNum()
        {
            lboxDetNum.Items.Clear(); // очишаем ListBox 
            string[] tempArr = _detNumList.ToArray(); // _detNumList который будет ввыведен
            lboxDetNum.Items.AddRange(tempArr);
            _autoComSourceForDetNumlst.Clear();
            _autoComSourceForDetNumlst.AddRange(tempArr);
        }




        private void AutoCompaleTextBox()
        {
            tbSearchDetNum.AutoCompleteCustomSource = _autoComSourceForDetNumlst;
            tbSearchDetNum.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbSearchDetNum.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }




        private void button4_Click(object sender, EventArgs e)
        {

            if (openMdbFile.ShowDialog() == DialogResult.OK)
            {
                string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=True", openMdbFile.FileName);
                ImportAnTables.conectionString = connectionString;
            }

            DataSet inputSet = ImportAnTables.FillData();

            FillTrudoyomkostDB.InsertFromANTables(inputSet);


            dataGridView1.DataSource = FillTrudoyomkostDB.InfDetDataTable;
            dataGridView2.DataSource = FillTrudoyomkostDB.WhereUseDataTable;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            FillTrudoyomkostDB.TarifTableAdapter = new ODLDB210410DataSetTableAdapters.TARIFTableAdapter();
            FillTrudoyomkostDB.TarifDataTable = new ODLDB210410DataSet.TARIFDataTable();
            FillTrudoyomkostDB.Tn148TableAdapter = new ODLDB210410DataSetTableAdapters.TN148TableAdapter();
            FillTrudoyomkostDB.Tn158TableAdapter = new ODLDB210410DataSetTableAdapters.TN158TableAdapter();
            FillTrudoyomkostDB.Tn70TableAdapter = new ODLDB210410DataSetTableAdapters.TN70TableAdapter();
            FillTrudoyomkostDB.Tn148DataTable = new ODLDB210410DataSet.TN148DataTable();
            FillTrudoyomkostDB.Tn158DataTable = new ODLDB210410DataSet.TN158DataTable();
            FillTrudoyomkostDB.Tn70DataTable = new ODLDB210410DataSet.TN70DataTable();
            FillTrudoyomkostDB.Tn148TableAdapter.Fill(FillTrudoyomkostDB.Tn148DataTable);
            FillTrudoyomkostDB.Tn158TableAdapter.Fill(FillTrudoyomkostDB.Tn158DataTable);
            FillTrudoyomkostDB.InsertFromTNTables();
            dataGridView3.DataSource = FillTrudoyomkostDB.LabourNormDataTable;
            dataGridView4.DataSource = FillTrudoyomkostDB.WhereOperationUseDataTable;
        }

        private void btTarifImport_Click(object sender, EventArgs e)
        {
            FillTrudoyomkostDB.TarifTableAdapter.Fill(FillTrudoyomkostDB.TarifDataTable);
            FillTrudoyomkostDB.InsertFromTARIF();
        }


        //нажатие мышкой на один из номеров ктс в главном меню
        private void listBox1_Click(object sender, EventArgs e)
        {
            chbWithoutNorm.Enabled = true;
            _detNumID = dcIDDetNum[lboxDetNum.SelectedItem.ToString()];
            tbSearchDetNum.Text = lboxDetNum.SelectedItem.ToString();

            LinqQueryForTrudoyomkost.FillLabourListForSelectDet(_detNumID, ref FillTrudoyomkostDB.CurrentLabourNormList);

            LinqQueryForTrudoyomkost.SetWhereUseDt(_dtWhereUse, _depList, _infProductsDc, _detNumID);

            _taskNumbers = LinqQueryForTrudoyomkost.SelectAllTaskNum();
            cboxChangeTaskNum.DataSource = _taskNumbers;

            var tempResult2 = from infdet in _infDetList
                              join depList in _depList
                                on infdet.DepProducer equals depList.ID
                              where infdet.ID == _detNumID
                              select new
                              {
                                  depList.Code
                              };
            _depProducer = tempResult2.First().Code.ToString();

            lbDepProducer.Text = _depProducer;
            lbDetNum.Text = lboxDetNum.SelectedItem.ToString();
            lbNormMapNumber.Text = _detNumID.ToString();

            FillTrudoyomkostDB.NormMapNumber = lbNormMapNumber.Text;

            CreateMaxApplyDc(new List<string>() { "158", "148", "70" });
        }


        public void CreateMaxApplyDc(List<string> listProdName)
        {
            MaxApply tMaxApply;
            _dcMaxApply.Clear();
            foreach (var item in listProdName)
            {
                tMaxApply = MaxSeriesForProduct(item, ref dgWhereUse);
                if (tMaxApply.SeriaFrom != 0)
                {
                    _dcMaxApply.Add(item, tMaxApply);
                }
            }
        }





        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                lboxDetNum.SelectedItem = lboxDetNum.Items[lboxDetNum.FindString(tbSearchDetNum.Text)];
                listBox1_Click(this, e);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Деталь отсутствует");
            }

        }

        private void dgWhereUse_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            FillgbShortInfoNormMap(e, ref dgWhereUse);
            LinqQueryForTrudoyomkost.FillOperNumlbox(_detNumID, _seriaFrom, _seriaTo, _selectProdCipher,
                                                     ref lBoxOperNum);
        }

        private void FillgbShortInfoNormMap(DataGridViewCellEventArgs e, ref DataGridView dgTemp)
        {
            vtbSeriaFrom.ValueTxt = dgTemp.Rows[e.RowIndex].Cells[0].Value.ToString();
            vtbSeriaTo.ValueTxt = dgTemp.Rows[e.RowIndex].Cells[1].Value.ToString();
            lbProduct.Text = dgTemp.Rows[e.RowIndex].Cells[2].Value.ToString();
            lbItemPerProd.Text = dgTemp.Rows[e.RowIndex].Cells[3].Value.ToString();
            _selectDetCount = int.Parse(dgTemp.Rows[e.RowIndex].Cells[3].Value.ToString());
            lbDepConsumer.Text = dgTemp.Rows[e.RowIndex].Cells[4].Value.ToString();
            _seriaFrom = int.Parse(dgTemp.Rows[e.RowIndex].Cells[5].Value.ToString());
            _seriaTo = int.Parse(dgTemp.Rows[e.RowIndex].Cells[6].Value.ToString());
            _selectProdCipher = _infProductsDc[dgTemp.Rows[e.RowIndex].Cells[2].Value.ToString()];
            btViewDetNumNorms.Enabled = true;

        }

        #region Return Max Values From dgWhereUse Per Product (148,158,70 etc.)

        private MaxApply MaxSeriesForProduct(string productName, ref DataGridView dgTemp)
        {
            int maxSeriaTo = 0;
            int maxSeriaFrom = 0;
            int countPerProduct = 0;
            foreach (DataGridViewRow rView in dgTemp.Rows)
            {
                if (productName.Equals(rView.Cells[2].Value.ToString()))
                {
                    if ((int)rView.Cells[5].Value > maxSeriaTo)
                    {
                        maxSeriaFrom = (int)rView.Cells[5].Value;
                        maxSeriaTo = (int)rView.Cells[6].Value;
                        countPerProduct = (int)rView.Cells[3].Value;
                    }
                }
            }
            return new MaxApply(maxSeriaFrom, maxSeriaTo, productName, countPerProduct);
        }

        #endregion


        private void btViewDetNumNorms_Click(object sender, EventArgs e)
        {
            _newfmNormsViewer = new fmNormsViewer(lbDetNum.Text, dcIDDetNum[lbDetNum.Text], _seriaFrom,
                                                  _seriaTo, _selectProdCipher, _infProductsDc, _selectDetCount, _selectTaskNumber);
            _newfmNormsViewer.ShowDialog();
        }



        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }



        private void vtbSeriaFrom_Validated(object sender, EventArgs e)
        {
            ValidatingTextBox tempVtb = sender as ValidatingTextBox;
            _seriaFrom = MathFunctionForSeries.GetIntSeriaNumber(tempVtb.ValueTxt);
            LinqQueryForTrudoyomkost.FillOperNumlbox(_detNumID, _seriaFrom, _seriaTo, _selectProdCipher,
                                                     ref lBoxOperNum);

        }

        private void vtbSeriaTo_Validated(object sender, EventArgs e)
        {
            ValidatingTextBox tempVtb = sender as ValidatingTextBox;
            _seriaTo = MathFunctionForSeries.GetIntSeriaNumber(tempVtb.ValueTxt);
            LinqQueryForTrudoyomkost.FillOperNumlbox(_detNumID, _seriaFrom, _seriaTo, _selectProdCipher,
                                                     ref lBoxOperNum);
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            exLoadPictureBox.Visible = true;
            exportBackGround.RunWorkerAsync();


        }
        private void exportBackGround_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            MDBProvider mdbProvider = new MDBProvider();
            DataTable resultdt = new DataTable();
            ODLDB210410DataSet dataset = new ODLDB210410DataSet();
            resultdt = dataset.AN148;

            mdbProvider.CreateDB();
            mdbProvider.CreateTNTable("TN148", dataset.TN148);
            mdbProvider.CreateTNTable("TN158", dataset.TN148);
            mdbProvider.CreateTNTable("TN178", dataset.TN148);
            mdbProvider.CreateTNTable("TN70", dataset.TN148);


            resultdt = LinqQueryForTrudoyomkost.FillTNTable(31);
            DataTable testDt = new DataTable();

            mdbProvider.simpleInsert(resultdt);
            resultdt = LinqQueryForTrudoyomkost.FillTNTable(17);
            mdbProvider.simpleInsert(resultdt);


        }
        private void button3_Click(object sender, EventArgs e)
        {

            //foreach (DataRow drow in fTable.Rows)
            //{
            //    fff.TN148.ImportRow(drow);
            //}
            //foreach (DataRow drow in fTable.Rows)
            //{
            //    fff.TN158.ImportRow(drow);
            //}


            //System.IO.StreamWriter xmlSW = new System.IO.StreamWriter("Customers.xml");
            //fff.WriteXml("Customers.xml");
            //xmlSW.Close();
        }

        #region Users Settings

        #region Handler Button for Add record to InfProfession

        private void btProfAdd_Click_1(object sender, EventArgs e)
        {
            foreach (DataRow row in _dtInfProf.Rows)
            {
                if ((int)row[0] == int.Parse(vtbProfCode.ValueTxt))
                    return;
            }
            if (checkValidateProfRow(gpEditProff))
            {
                using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
                {

                    FillTrudoyomkostDB.InfProfessionTableAdapter.InsertQuery(int.Parse(vtbProfCode.ValueTxt),
                                                                             tbProfName.Text, tbProfKindWork.Text, 1049);
                    LinqQueryForTrudoyomkost.FilldtInfProf(currentContext, ref _dtInfProf);
                }
                errorProv.SetError(gpEditProff, String.Empty);
            }
            else
            {
                errorProv.SetError(gpEditProff, "Заполните поля");
            }
        }
        #endregion
        private void btDelProf_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Вы уверены, что хотите удалить запись?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FillTrudoyomkostDB.InfProfessionTableAdapter.DeleteRow(_currentProfCode);
                using (
                    var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
                {
                    LinqQueryForTrudoyomkost.FilldtInfProf(currentContext, ref _dtInfProf);
                }
            }
        }

        #region Function check output data before add to infProfession table
        private bool checkValidateProfRow(GroupBox gpInput)
        {
            foreach (var item in gpInput.Controls)
            {
                ValidatingTextBox vtbTemp = item as ValidatingTextBox;
                TextBox tbtemp = item as TextBox;

                if (vtbTemp != null)
                    if (string.IsNullOrEmpty(vtbTemp.ValueTxt))
                        return false;


                if (tbtemp != null)
                    if (string.IsNullOrEmpty(tbtemp.Text))
                        return false;


            }
            return true;
        }
        #endregion


        private void dgProfesion_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView tempdg = sender as DataGridView;
            _currentProfCode = (int)tempdg.Rows[e.RowIndex].Cells[0].Value;
        }


        private void buttRoundNum_Click(object sender, EventArgs e)
        {
            TrudoyomkostSettings.set_RoundNumExtracted(tbRoundNum.Text);
        }

        private void btDepNum_MouseClick(object sender, MouseEventArgs e)
        {
            TrudoyomkostSettings.set_DepNumExtracted(tbDepNum.Text);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox tempcbox = sender as CheckBox;
            if (tempcbox.Checked)
            {
                TrudoyomkostSettings.IsAggregateDep = true;
                return;
            }
            TrudoyomkostSettings.IsAggregateDep = false;


        }






        private void tbSearchDetNum_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click_1(this, new EventArgs());
        }


        private void mainForm_Shown(object sender, EventArgs e)
        {
            fmAuthent.ShowDialog();
        }


        private void btDe_Click(object sender, EventArgs e)
        {
            var tempresult = from laboritem in FillTrudoyomkostDB.CurrentLabourNormList
                             join whereOperitem in FillTrudoyomkostDB.WhereOperationUseList
                             on laboritem.ID equals whereOperitem.LabourNormID
                             where laboritem.OperNum == lBoxOperNum.SelectedItem.ToString()
                             select whereOperitem;

            WhereOperationUse firstittem = tempresult.First();
            FillTrudoyomkostDB.WhereOperationUseList.Remove(firstittem);
            FillTrudoyomkostDB.WhereOperationUseTableAdapter.DeleteQuery(firstittem.LabourNormID);

            LinqQueryForTrudoyomkost.FillOperNumlbox(_detNumID, _seriaFrom, _seriaTo, _selectProdCipher,
                                                    ref lBoxOperNum);
        }





        private void cboxChangeTaskNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox input = sender as ComboBox;
            if (input != null)
            {
                if (cboxChangeTaskNum.Visible == true)
                {
                    LinqQueryForTrudoyomkost.FillLabourListForSelectDet(_detNumID, ref FillTrudoyomkostDB.CurrentLabourNormList);
                    _selectTaskNumber = input.SelectedValue.ToString();
                    LinqQueryForTrudoyomkost.SelectLabourNormTaskNum(_selectTaskNumber);
                }
            }

        }
        #endregion

        private DataTable _allLabourNormForSeries;

        private void tabNormView_Enter(object sender, EventArgs e)
        {
            _allLabourNormForSeries = LinqQueryForTrudoyomkost.GetAllLabourNormForSeria(_seriaFrom, _seriaTo, _selectProdCipher);


            dgLabourViewer.DataSource = _allLabourNormForSeries;
            if (dgLabourViewer.Columns.Count != 0)
            {
                UserDataTables.SetDgColumns(dgLabourViewer);
            }


        }

        private Dictionary<string, string> _totalRowsNames = new Dictionary<string, string>() {
            {"DetCount", "Количество наименований деталей"},
            {"TotalDetCount", "Количество деталей"},
            {"NormalCount", "Количество наименований нормалей"},
            {"TotalNormalCount", "Количество нормалей"},
            {"AssemblyCount", "Количество наименований cборок"},
            {"TotalAssemblyCount","Количество  cборок" },
            {"AssembNormalCount","Количество наименований зборочных нормалей" },
            {"TotalAssembNormalCount", "Количество  cборочных нормалей"},
            {"PayNormAmount", "Количество норм(платежных)"},
            {"CTNormAmount", "Колисество норм(РТН)"}
        };
        private void tabTotal_Enter(object sender, EventArgs e)
        {
            label26.Text = lbProduct.Text;
            label27.Text = vtbSeriaFrom.ValueTxt;
            TotalForSeria currentTotalSeria = new TotalForSeria();

            currentTotalSeria.DetCount = LinqQueryForTrudoyomkost.GetAmountDetForSeria(_seriaFrom, _seriaTo, _selectProdCipher, "Д ");
            currentTotalSeria.AssembNormalCount = LinqQueryForTrudoyomkost.GetAmountDetForSeria(_seriaFrom, _seriaTo, _selectProdCipher, "CH");
            currentTotalSeria.AssemblyCount = LinqQueryForTrudoyomkost.GetAmountDetForSeria(_seriaFrom, _seriaTo, _selectProdCipher, "C ");
            currentTotalSeria.NormalCount = LinqQueryForTrudoyomkost.GetAmountDetForSeria(_seriaFrom, _seriaTo, _selectProdCipher, "H ");


            currentTotalSeria.TotalDetCount = LinqQueryForTrudoyomkost.GetTotalDetForSeria(_seriaFrom, _seriaTo, _selectProdCipher, "Д ");
            currentTotalSeria.TotalAssembNormalCount = LinqQueryForTrudoyomkost.GetTotalDetForSeria(_seriaFrom, _seriaTo, _selectProdCipher, "CH");
            currentTotalSeria.TotalAssemblyCount = LinqQueryForTrudoyomkost.GetTotalDetForSeria(_seriaFrom, _seriaTo, _selectProdCipher, "C ");
            currentTotalSeria.TotalNormalCount = LinqQueryForTrudoyomkost.GetTotalDetForSeria(_seriaFrom, _seriaTo, _selectProdCipher, "H ");


            //  currentTotalSeria.PayNormAmount = LinqQueryForTrudoyomkost.VozvratKolvaItemPayNorm(_seriaFrom, _seriaTo, _selectProdCipher);
            currentTotalSeria.CTNormAmount = (int)LinqQueryForTrudoyomkost.GetTotalDetForSeria2(_seriaFrom, _seriaTo, _selectProdCipher);
            currentTotalSeria.PayNormAmount = (int)LinqQueryForTrudoyomkost.GetTotalDetForSeria3(_seriaFrom, _seriaTo, _selectProdCipher);

            Type t = currentTotalSeria.GetType();
            FieldInfo[] finfo = t.GetFields();

            dgTotalTypeDet.Rows.Clear();
            foreach (var p in finfo)
            {
                dgTotalTypeDet.Rows.Add(_totalRowsNames[p.Name], "шт", p.GetValue(currentTotalSeria));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {

                foreach (var item in currentContext.LabourNorm)
                {
                    if (item.ItemCTN == 0 && item.PreparTimeCTN == 0 && item.ItemPayNorm == 0 && item.PreparTimePayNorm == 0)
                    {
                        currentContext.LabourNorm.DeleteOnSubmit(item);
                    }
                    if (item.KindPay == "C")
                        item.KindPay = "C";
                    currentContext.SubmitChanges();
                }
            }
        }

        private void btSelectDir_Click(object sender, EventArgs e)
        {
            DialogResult result = selectMdbFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                TrudoyomkostSettings.MdbFileDir = selectMdbFolder.SelectedPath;
            }
        }



        private void exportBackGround_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            exLoadPictureBox.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            saveXlcDialog.FileName = "Sample.xlsx";
            saveXlcDialog.Filter = "Excel 2007 files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveXlcDialog.FilterIndex = 1;
            saveXlcDialog.RestoreDirectory = true;
            saveXlcDialog.OverwritePrompt = false;

            //  If the user hit Cancel, then abort!
            if (saveXlcDialog.ShowDialog() != DialogResult.OK)
                return;


            string targetFilename = saveXlcDialog.FileName;


            //  Step 1: Create a DataSet, and put some sample data in it

            //DataSet myDS = ConnectDB.connectDBF(@DBFPath, @DBFName);
            //DataTable daDBF = new DataTable();
            //daDBF = myDS.Tables[0];

            DataSet ds = new DataSet();
            ds.Tables.Add(_allLabourNormForSeries);

            // ds.Tables.Add(daDBF);
            //CreateSampleData(); returns dataset
            // DataSet ds = CreateSampleData();

            //  Step 2: Create the Excel file
            try
            {
                CreateExcelFile.CreateExcelDocument(ds, targetFilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't create Excel file.\r\nException: " + ex.Message);
                return;
            }

            //  Step 3:  Let's open our new Excel file and shut down this application.
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(targetFilename);
            p.Start();

            this.Close();

        }

        private void chBoxNotFill_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxNotFill.Checked)
            {
                LinqQueryForTrudoyomkost.GetEmptyDetNum(ref _detNumList, _seriaFrom, _infProductsDc[cbSelectProduct.SelectedItem.ToString()]);
                SetlboxDetNum();
            }

        }
        DataTable dtTotralForSeria;
        private void tabFullReport_Enter(object sender, EventArgs e)
        {
            dtTotralForSeria = new DataTable("OperationApplyTable");
            DataColumn ProfCode = new DataColumn("ProfCode", typeof(int));
            DataColumn NameKindWork = new DataColumn("NameKindWork", typeof(string));
            DataColumn KindPayColumn = new DataColumn("KindPay", typeof(string));
            DataColumn WorkerRateColumn = new DataColumn("WorkerRate", typeof(double));
            DataColumn ItemCTNColumn = new DataColumn("ItemCTN", typeof(decimal));
            DataColumn PreparTimeCTNColumn = new DataColumn("PreparTimeCTN", typeof(decimal));
            DataColumn ItemPayNormColumn = new DataColumn("ItemPayNorm", typeof(decimal));
            DataColumn PreparTimePayNormColumn = new DataColumn("PreparTimePayNorm", typeof(decimal));
            DataColumn ValuationColumn = new DataColumn("Valuation", typeof(double));
            DataColumn ValPreparTimeColumn = new DataColumn("ValPreparTime", typeof(double));
            dtTotralForSeria.Columns.AddRange(new DataColumn[] { 
                ProfCode,NameKindWork, KindPayColumn,WorkerRateColumn, ItemCTNColumn, 
                PreparTimeCTNColumn, ItemPayNormColumn, PreparTimePayNormColumn ,ValuationColumn,ValPreparTimeColumn
            });

            LinqQueryForTrudoyomkost.GetTotalCalculNormReport(_seriaFrom, _selectProdCipher, ref dtTotralForSeria);
            dgTotalNormCalc.DataSource = dtTotralForSeria;
            UserDataTables.SetDgColumns(dgTotalNormCalc);
            label31.Text = lbProduct.Text;
            label30.Text = vtbSeriaFrom.ValueTxt;
        }

        private void tabProducts_Enter(object sender, EventArgs e)
        {
            using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {
                dgProducts.DataSource = LinqQueryForTrudoyomkost.GetAllProducts(currentContext);
                dgProducts.Columns[0].ReadOnly = true;
            }
        }


        private void dgProducts_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            short cipher = 0;
            DataGridView tempDg;
            tempDg = sender as DataGridView;
            if (tempDg != null)
                cipher = (short)tempDg.Rows[e.RowIndex].Cells[0].Value;

            //tempDg.Rows[e.RowIndex].
            using (var currentContext = new TrudoyomkostDBContext(Properties.Settings.Default.TrudoyomkostDBConnectionString))
            {

                var selectEntity = currentContext.InfProducts.Where(item => item.Cipher == cipher).First();

                selectEntity.Product = tempDg.Rows[e.RowIndex].Cells[1].Value.ToString();
                selectEntity.Mask = (byte)tempDg.Rows[e.RowIndex].Cells[3].Value;
                selectEntity.ProductTotal = tempDg.Rows[e.RowIndex].Cells[2].Value.ToString();
                currentContext.SubmitChanges();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            DataTable dt = dtTotralForSeria.Clone();
            for (int i = 0; i < dtTotralForSeria.Rows.Count; i++)//dgTotalNormCalc
            {
                dt.ImportRow(dtTotralForSeria.Rows[i]);
            }
            TotalForProductXtraRep newReport = new TotalForProductXtraRep();
            newReport.DataSource = dt;
            newReport.xrLabel2.Text = lbProduct.Text;
            newReport.xrLabel4.Text = vtbSeriaFrom.ValueTxt;
            newReport.xrTableCell1.DataBindings.Add("Text", dt, "ProfCode");
            newReport.xrTableCell2.DataBindings.Add("Text", dt, "NameKindWork");
            newReport.xrTableCell3.DataBindings.Add("Text", dt, "KindPay");
            newReport.xrTableCell4.DataBindings.Add("Text", dt, "WorkerRate");
            newReport.xrTableCell5.DataBindings.Add("Text", dt, "ItemCTN");
            newReport.xrTableCell6.DataBindings.Add("Text", dt, "PreparTimeCTN");
            newReport.xrTableCell7.DataBindings.Add("Text", dt, "ItemPayNorm");
            newReport.xrTableCell8.DataBindings.Add("Text", dt, "PreparTimePayNorm");
            newReport.xrTableCell9.DataBindings.Add("Text", dt, "Valuation");
            newReport.xrTableCell10.DataBindings.Add("Text", dt, "ValPreparTime");
            newReport.ShowPreview();
        }

        private void zapolnenie_norm_Click(object sender, EventArgs e)
        {
            
            zapolnenie_norm.Visible = false;
            textBoxForTariffPass.Visible = true;
            passForTariffoK.Visible = true;
            labelForTariffPass.Visible = true;


        }

        private void passForTariffoK_Click(object sender, EventArgs e)
        {
            
            if (textBoxForTariffPass.Text == Properties.Settings.Default.TariffPass)
            {
                dobavlenieNorm dobavlenieNorm = new dobavlenieNorm(this);
                dobavlenieNorm.Show();

                textBoxForTariffPass.Text = "";
                textBoxForTariffPass.Visible = false;
                passForTariffoK.Visible = false;
                labelForTariffPass.Visible = false;
                zapolnenie_norm.Visible = true;

            }
            else
            {
                MessageBox.Show("Неверный пароль");
                textBoxForTariffPass.Text = "";
                textBoxForTariffPass.Visible = false;
                passForTariffoK.Visible = false;
                labelForTariffPass.Visible = false;
                zapolnenie_norm.Visible = true;
            }
        }

  

        
    

    }
}

