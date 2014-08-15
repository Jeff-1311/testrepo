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
    public partial class fmGroupUpdate : Form
    {
        DataTable _dtOperApply;
        DataGridView _currentLabourDg;
        List<int> _infDetIdList;
        ICommand currentExecutedComand;
        WhereOperationUse _applyWhereOper;
        fmNormsViewer _fmParent;
        bool _isUpdate;
        public fmGroupUpdate()
        {
            InitializeComponent();
        }


        public fmGroupUpdate(fmNormsViewer fminput)
        {
            InitializeComponent();
            _currentLabourDg = fminput.DgOperViewer;
            _fmParent = fminput;
            InitializeFormFields(_currentLabourDg);
          
        }


        public void InitializeFormFields(DataGridView inputdg)
        {
            _infDetIdList = new List<int>();
            foreach (DataGridViewRow row in inputdg.Rows)
                _infDetIdList.Add((int)row.Cells[0].Value);
            vtbSeriaFrom.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbSeriaFrom.ValidateValue = VtboxMethods.CheckSeria;
            vtbSeriaTo.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbSeriaTo.ValidateValue = VtboxMethods.CheckSeria;
            vtbProdName.ValidateValue = VtboxMethods.CheckProdNume;
            vtbProdName.StringAutoCorrectionMethod = VtboxMethods.correctForInt;
            vtbLowerCoff.StringAutoCorrectionMethod = VtboxMethods.correctForDouble;
            vtbLowerCoff.ValidateValue = VtboxMethods.checkForDouble;
            vtbLowerCoff.ValueTxt = "0";
            _dtOperApply = UserDataTables.CreateOperAppDt();
            dgOperApply.DataSource = _dtOperApply;
            chBoxWithoutNew_CheckedChanged(chBoxWithoutNew, new EventArgs());
        }

        private void btAddOper_Click(object sender, EventArgs e)
        {
            

                if (!vtbProdName.IsValid || !vtbSeriaFrom.IsValid || !vtbSeriaTo.IsValid)
                    return;
                if (!FillTrudoyomkostDB.DcMaxApply.ContainsKey(vtbProdName.ValueTxt))
                    return;

                int SeriaFrom = MathFunctionForSeries.GetIntSeriaNumber(vtbSeriaFrom.ValueTxt);
                int SeriaTo = MathFunctionForSeries.GetIntSeriaNumber(vtbSeriaTo.ValueTxt);
                short prodCipher = FillTrudoyomkostDB.DcInfProducts[vtbProdName.ValueTxt];

                if (SeriaFrom > SeriaTo)
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
                    _applyWhereOper = new WhereOperationUse();
                    _applyWhereOper.SeriaFrom = SeriaFrom;
                    _applyWhereOper.SeriaTo = SeriaTo;
                    _applyWhereOper.InfProductsChipher = prodCipher;
                    if (_isUpdate)
                    {
                        currentExecutedComand = new UpdateLabourCard(_infDetIdList, _applyWhereOper);
                    }
                    else
                    {
                        UpdateAndCreateNewLabourCard tempComand;
                        if (currentExecutedComand == null)
                        {
                            currentExecutedComand = new UpdateAndCreateNewLabourCard(_infDetIdList, _applyWhereOper);
                            tempComand = currentExecutedComand as UpdateAndCreateNewLabourCard;
                            if (vtbLowerCoff.IsValid)
                            {
                                tempComand.ApplyCoeff = double.Parse(vtbLowerCoff.ValueTxt);
                            }
                        }
                        else
                        {
                            tempComand = currentExecutedComand as UpdateAndCreateNewLabourCard;
                            if (tempComand != null)
                            {
                                tempComand.ApplyWhereOpers = _applyWhereOper;


                            }
                        }

                    }
                    currentExecutedComand.execute();
                    _fmParent.UpdateDataGrids();
                }
            }

        private void chBoxWithoutNew_CheckedChanged(object sender, EventArgs e)
        {
           
            CheckBox tempCbox = sender as CheckBox;
            if (tempCbox != null)
            {
                if (tempCbox.Checked)
                {
                    _isUpdate = true;
                   
                }
                else
                {
                    _isUpdate = false;
                }
            }
        }
            
        }
    }

