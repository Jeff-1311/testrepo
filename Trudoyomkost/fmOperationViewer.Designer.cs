namespace Trudoyomkost
{
    partial class fmOperationViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbOperItems = new System.Windows.Forms.GroupBox();
            this.vcbProfCode = new Trudoyomkost.ValidatingComboBox(this.components);
            this.cbDocNum = new System.Windows.Forms.ComboBox();
            this.vtbDepRegion = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbTaskNum = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbWorkRate = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbOperNum = new Trudoyomkost.ValidatingTextBox(this.components);
            this.dgOperApply = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.gbPlat = new System.Windows.Forms.GroupBox();
            this.gbLoverCoff = new System.Windows.Forms.GroupBox();
            this.btLovCofCount = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.vtbLoverCoeff = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbPreparTimePayNorm = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbItemPayNorm = new Trudoyomkost.ValidatingTextBox(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.gbRtn = new System.Windows.Forms.GroupBox();
            this.vtbCoeffCTN = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbPreparTimeCTN = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbItemCTN = new Trudoyomkost.ValidatingTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbKindPay = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btSaveAndLimit = new System.Windows.Forms.Button();
            this.btSaveFrom = new System.Windows.Forms.Button();
            this.btUpdateForm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btPrev = new System.Windows.Forms.Button();
            this.btNext = new System.Windows.Forms.Button();
            this.gpAddOperApply = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btLimitedOper = new System.Windows.Forms.Button();
            this.vtbSeriaTo = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbSeriaFrom = new Trudoyomkost.ValidatingTextBox(this.components);
            this.vtbProdName = new Trudoyomkost.ValidatingTextBox(this.components);
            this.btAddOper = new System.Windows.Forms.Button();
            this.gbOperItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperApply)).BeginInit();
            this.gbPlat.SuspendLayout();
            this.gbLoverCoff.SuspendLayout();
            this.gbRtn.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gpAddOperApply.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOperItems
            // 
            this.gbOperItems.Controls.Add(this.vcbProfCode);
            this.gbOperItems.Controls.Add(this.cbDocNum);
            this.gbOperItems.Controls.Add(this.vtbDepRegion);
            this.gbOperItems.Controls.Add(this.vtbTaskNum);
            this.gbOperItems.Controls.Add(this.vtbWorkRate);
            this.gbOperItems.Controls.Add(this.vtbOperNum);
            this.gbOperItems.Controls.Add(this.dgOperApply);
            this.gbOperItems.Controls.Add(this.label9);
            this.gbOperItems.Controls.Add(this.gbPlat);
            this.gbOperItems.Controls.Add(this.gbRtn);
            this.gbOperItems.Controls.Add(this.label4);
            this.gbOperItems.Controls.Add(this.label3);
            this.gbOperItems.Controls.Add(this.label2);
            this.gbOperItems.Controls.Add(this.label11);
            this.gbOperItems.Controls.Add(this.label8);
            this.gbOperItems.Controls.Add(this.label7);
            this.gbOperItems.Controls.Add(this.label1);
            this.gbOperItems.Controls.Add(this.cbKindPay);
            this.gbOperItems.Location = new System.Drawing.Point(0, 0);
            this.gbOperItems.Name = "gbOperItems";
            this.gbOperItems.Size = new System.Drawing.Size(381, 511);
            this.gbOperItems.TabIndex = 0;
            this.gbOperItems.TabStop = false;
            // 
            // vcbProfCode
            // 
            this.vcbProfCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.vcbProfCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.vcbProfCode.Location = new System.Drawing.Point(113, 112);
            this.vcbProfCode.Name = "vcbProfCode";
            this.vcbProfCode.SelectedIndex = -1;
            this.vcbProfCode.SelectedItem = null;
            this.vcbProfCode.Size = new System.Drawing.Size(241, 30);
            this.vcbProfCode.TabIndex = 3;
            // 
            // cbDocNum
            // 
            this.cbDocNum.FormattingEnabled = true;
            this.cbDocNum.Location = new System.Drawing.Point(115, 383);
            this.cbDocNum.Name = "cbDocNum";
            this.cbDocNum.Size = new System.Drawing.Size(228, 21);
            this.cbDocNum.TabIndex = 8;
            // 
            // vtbDepRegion
            // 
            this.vtbDepRegion.Location = new System.Drawing.Point(107, 358);
            this.vtbDepRegion.Name = "vtbDepRegion";
            this.vtbDepRegion.Size = new System.Drawing.Size(122, 20);
            this.vtbDepRegion.StringAutoCorrectionMethod = null;
            this.vtbDepRegion.TabIndex = 7;
            this.vtbDepRegion.ValidateValue = null;
            this.vtbDepRegion.ValueTxt = "";
            // 
            // vtbTaskNum
            // 
            this.vtbTaskNum.Enabled = false;
            this.vtbTaskNum.Location = new System.Drawing.Point(108, 335);
            this.vtbTaskNum.Name = "vtbTaskNum";
            this.vtbTaskNum.Size = new System.Drawing.Size(120, 20);
            this.vtbTaskNum.StringAutoCorrectionMethod = null;
            this.vtbTaskNum.TabIndex = 6;
            this.vtbTaskNum.ValidateValue = null;
            this.vtbTaskNum.ValueTxt = "";
            // 
            // vtbWorkRate
            // 
            this.vtbWorkRate.Location = new System.Drawing.Point(111, 59);
            this.vtbWorkRate.Name = "vtbWorkRate";
            this.vtbWorkRate.Size = new System.Drawing.Size(120, 20);
            this.vtbWorkRate.StringAutoCorrectionMethod = null;
            this.vtbWorkRate.TabIndex = 1;
            this.vtbWorkRate.ValidateValue = null;
            this.vtbWorkRate.ValueTxt = "";
            // 
            // vtbOperNum
            // 
            this.vtbOperNum.Location = new System.Drawing.Point(111, 33);
            this.vtbOperNum.Name = "vtbOperNum";
            this.vtbOperNum.Size = new System.Drawing.Size(245, 20);
            this.vtbOperNum.StringAutoCorrectionMethod = null;
            this.vtbOperNum.TabIndex = 0;
            this.vtbOperNum.ValidateValue = null;
            this.vtbOperNum.ValueTxt = "";
            // 
            // dgOperApply
            // 
            this.dgOperApply.AllowUserToAddRows = false;
            this.dgOperApply.AllowUserToDeleteRows = false;
            this.dgOperApply.AllowUserToResizeColumns = false;
            this.dgOperApply.AllowUserToResizeRows = false;
            this.dgOperApply.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOperApply.Location = new System.Drawing.Point(9, 418);
            this.dgOperApply.MultiSelect = false;
            this.dgOperApply.Name = "dgOperApply";
            this.dgOperApply.ReadOnly = true;
            this.dgOperApply.RowHeadersVisible = false;
            this.dgOperApply.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgOperApply.RowTemplate.Height = 19;
            this.dgOperApply.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOperApply.Size = new System.Drawing.Size(347, 89);
            this.dgOperApply.TabIndex = 10;
            this.dgOperApply.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgOperApply_CellClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 402);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Применяемость:";
            // 
            // gbPlat
            // 
            this.gbPlat.Controls.Add(this.gbLoverCoff);
            this.gbPlat.Controls.Add(this.vtbPreparTimePayNorm);
            this.gbPlat.Controls.Add(this.vtbItemPayNorm);
            this.gbPlat.Controls.Add(this.label13);
            this.gbPlat.Controls.Add(this.label14);
            this.gbPlat.Location = new System.Drawing.Point(18, 232);
            this.gbPlat.Name = "gbPlat";
            this.gbPlat.Size = new System.Drawing.Size(291, 96);
            this.gbPlat.TabIndex = 5;
            this.gbPlat.TabStop = false;
            this.gbPlat.Text = "Плат. норма";
            // 
            // gbLoverCoff
            // 
            this.gbLoverCoff.Controls.Add(this.btLovCofCount);
            this.gbLoverCoff.Controls.Add(this.label17);
            this.gbLoverCoff.Controls.Add(this.vtbLoverCoeff);
            this.gbLoverCoff.Location = new System.Drawing.Point(0, 58);
            this.gbLoverCoff.Name = "gbLoverCoff";
            this.gbLoverCoff.Size = new System.Drawing.Size(285, 33);
            this.gbLoverCoff.TabIndex = 23;
            this.gbLoverCoff.TabStop = false;
            // 
            // btLovCofCount
            // 
            this.btLovCofCount.Location = new System.Drawing.Point(255, 9);
            this.btLovCofCount.Name = "btLovCofCount";
            this.btLovCofCount.Size = new System.Drawing.Size(30, 23);
            this.btLovCofCount.TabIndex = 22;
            this.btLovCofCount.Text = ">>";
            this.btLovCofCount.UseVisualStyleBackColor = true;
            this.btLovCofCount.Click += new System.EventHandler(this.btLovCofCount_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 15);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(111, 13);
            this.label17.TabIndex = 21;
            this.label17.Text = "Пониж. коэф. к ПТН";
            // 
            // vtbLoverCoeff
            // 
            this.vtbLoverCoeff.Location = new System.Drawing.Point(118, 10);
            this.vtbLoverCoeff.Name = "vtbLoverCoeff";
            this.vtbLoverCoeff.Size = new System.Drawing.Size(131, 20);
            this.vtbLoverCoeff.StringAutoCorrectionMethod = null;
            this.vtbLoverCoeff.TabIndex = 0;
            this.vtbLoverCoeff.ValidateValue = null;
            this.vtbLoverCoeff.ValueTxt = "";
            // 
            // vtbPreparTimePayNorm
            // 
            this.vtbPreparTimePayNorm.Location = new System.Drawing.Point(118, 42);
            this.vtbPreparTimePayNorm.Name = "vtbPreparTimePayNorm";
            this.vtbPreparTimePayNorm.Size = new System.Drawing.Size(131, 20);
            this.vtbPreparTimePayNorm.StringAutoCorrectionMethod = null;
            this.vtbPreparTimePayNorm.TabIndex = 1;
            this.vtbPreparTimePayNorm.ValidateValue = null;
            this.vtbPreparTimePayNorm.ValueTxt = "";
            // 
            // vtbItemPayNorm
            // 
            this.vtbItemPayNorm.Location = new System.Drawing.Point(118, 16);
            this.vtbItemPayNorm.Name = "vtbItemPayNorm";
            this.vtbItemPayNorm.Size = new System.Drawing.Size(131, 20);
            this.vtbItemPayNorm.StringAutoCorrectionMethod = null;
            this.vtbItemPayNorm.TabIndex = 0;
            this.vtbItemPayNorm.ValidateValue = null;
            this.vtbItemPayNorm.ValueTxt = "";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(40, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "ПЗВ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(40, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Штучная";
            // 
            // gbRtn
            // 
            this.gbRtn.Controls.Add(this.vtbCoeffCTN);
            this.gbRtn.Controls.Add(this.vtbPreparTimeCTN);
            this.gbRtn.Controls.Add(this.vtbItemCTN);
            this.gbRtn.Controls.Add(this.label6);
            this.gbRtn.Controls.Add(this.label5);
            this.gbRtn.Controls.Add(this.label12);
            this.gbRtn.Location = new System.Drawing.Point(28, 139);
            this.gbRtn.Name = "gbRtn";
            this.gbRtn.Size = new System.Drawing.Size(232, 94);
            this.gbRtn.TabIndex = 4;
            this.gbRtn.TabStop = false;
            this.gbRtn.Text = "    РТН по тех. процессу";
            // 
            // vtbCoeffCTN
            // 
            this.vtbCoeffCTN.Location = new System.Drawing.Point(91, 73);
            this.vtbCoeffCTN.Name = "vtbCoeffCTN";
            this.vtbCoeffCTN.Size = new System.Drawing.Size(131, 20);
            this.vtbCoeffCTN.StringAutoCorrectionMethod = null;
            this.vtbCoeffCTN.TabIndex = 2;
            this.vtbCoeffCTN.ValidateValue = null;
            this.vtbCoeffCTN.ValueTxt = "";
            this.vtbCoeffCTN.Validated += new System.EventHandler(this.vtbCoeffCTN_Validated);
            // 
            // vtbPreparTimeCTN
            // 
            this.vtbPreparTimeCTN.Location = new System.Drawing.Point(91, 51);
            this.vtbPreparTimeCTN.Name = "vtbPreparTimeCTN";
            this.vtbPreparTimeCTN.Size = new System.Drawing.Size(131, 20);
            this.vtbPreparTimeCTN.StringAutoCorrectionMethod = null;
            this.vtbPreparTimeCTN.TabIndex = 1;
            this.vtbPreparTimeCTN.ValidateValue = null;
            this.vtbPreparTimeCTN.ValueTxt = "";
            // 
            // vtbItemCTN
            // 
            this.vtbItemCTN.Location = new System.Drawing.Point(91, 25);
            this.vtbItemCTN.Name = "vtbItemCTN";
            this.vtbItemCTN.Size = new System.Drawing.Size(131, 20);
            this.vtbItemCTN.StringAutoCorrectionMethod = null;
            this.vtbItemCTN.TabIndex = 0;
            this.vtbItemCTN.ValidateValue = null;
            this.vtbItemCTN.ValueTxt = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "ПЗВ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Штучная";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Пов. к-т к РТН";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Код профессии";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Вид оплаты";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Розряд";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(34, 383);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "№ документа";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 357);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Участок";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "№ задания";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "№ операции";
            // 
            // cbKindPay
            // 
            this.cbKindPay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKindPay.FormattingEnabled = true;
            this.cbKindPay.Items.AddRange(new object[] {
            "С",
            "П"});
            this.cbKindPay.Location = new System.Drawing.Point(110, 85);
            this.cbKindPay.Name = "cbKindPay";
            this.cbKindPay.Size = new System.Drawing.Size(117, 21);
            this.cbKindPay.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btSaveAndLimit);
            this.panel1.Controls.Add(this.btSaveFrom);
            this.panel1.Controls.Add(this.btUpdateForm);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btPrev);
            this.panel1.Controls.Add(this.btNext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 561);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 76);
            this.panel1.TabIndex = 15;
            // 
            // btSaveAndLimit
            // 
            this.btSaveAndLimit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSaveAndLimit.Location = new System.Drawing.Point(26, 36);
            this.btSaveAndLimit.Name = "btSaveAndLimit";
            this.btSaveAndLimit.Size = new System.Drawing.Size(125, 36);
            this.btSaveAndLimit.TabIndex = 3;
            this.btSaveAndLimit.TabStop = false;
            this.btSaveAndLimit.Text = "Сохранить с \r\nограничением старой\r\n\r\n";
            this.btSaveAndLimit.UseVisualStyleBackColor = true;
            this.btSaveAndLimit.Click += new System.EventHandler(this.btSaveAndLimit_Click);
            // 
            // btSaveFrom
            // 
            this.btSaveFrom.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSaveFrom.Location = new System.Drawing.Point(165, 36);
            this.btSaveFrom.Name = "btSaveFrom";
            this.btSaveFrom.Size = new System.Drawing.Size(119, 36);
            this.btSaveFrom.TabIndex = 4;
            this.btSaveFrom.TabStop = false;
            this.btSaveFrom.Text = "Сохранить\r\n  новую\r\n";
            this.btSaveFrom.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btSaveFrom.UseVisualStyleBackColor = true;
            this.btSaveFrom.Click += new System.EventHandler(this.btnSaveForm_Click);
            // 
            // btUpdateForm
            // 
            this.btUpdateForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btUpdateForm.Location = new System.Drawing.Point(307, 36);
            this.btUpdateForm.Name = "btUpdateForm";
            this.btUpdateForm.Size = new System.Drawing.Size(125, 36);
            this.btUpdateForm.TabIndex = 5;
            this.btUpdateForm.TabStop = false;
            this.btUpdateForm.Text = "Обновить\r\nтекущую\r\n";
            this.btUpdateForm.UseVisualStyleBackColor = true;
            this.btUpdateForm.Click += new System.EventHandler(this.btUpdateForm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(16, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 33);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Отмена (Esc)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btPrev
            // 
            this.btPrev.Location = new System.Drawing.Point(134, 3);
            this.btPrev.Name = "btPrev";
            this.btPrev.Size = new System.Drawing.Size(91, 33);
            this.btPrev.TabIndex = 1;
            this.btPrev.TabStop = false;
            this.btPrev.Text = "Предыдущая (Page Up)";
            this.btPrev.UseVisualStyleBackColor = true;
            this.btPrev.Click += new System.EventHandler(this.btPrev_Click);
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(231, 3);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(91, 33);
            this.btNext.TabIndex = 2;
            this.btNext.Text = "Следующая (Page Down)";
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // gpAddOperApply
            // 
            this.gpAddOperApply.Controls.Add(this.label16);
            this.gpAddOperApply.Controls.Add(this.label15);
            this.gpAddOperApply.Controls.Add(this.label10);
            this.gpAddOperApply.Controls.Add(this.btLimitedOper);
            this.gpAddOperApply.Controls.Add(this.vtbSeriaTo);
            this.gpAddOperApply.Controls.Add(this.vtbSeriaFrom);
            this.gpAddOperApply.Controls.Add(this.vtbProdName);
            this.gpAddOperApply.Controls.Add(this.btAddOper);
            this.gpAddOperApply.Location = new System.Drawing.Point(9, 511);
            this.gpAddOperApply.Name = "gpAddOperApply";
            this.gpAddOperApply.Size = new System.Drawing.Size(429, 44);
            this.gpAddOperApply.TabIndex = 1;
            this.gpAddOperApply.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(224, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 13);
            this.label16.TabIndex = 25;
            this.label16.Text = "Серия По";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(129, 5);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Серия С ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Изделие";
            // 
            // btLimitedOper
            // 
            this.btLimitedOper.Location = new System.Drawing.Point(325, 5);
            this.btLimitedOper.Name = "btLimitedOper";
            this.btLimitedOper.Size = new System.Drawing.Size(85, 36);
            this.btLimitedOper.TabIndex = 3;
            this.btLimitedOper.Text = "Ограничить";
            this.btLimitedOper.UseVisualStyleBackColor = true;
            this.btLimitedOper.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btLimitedOper_MouseClick);
            // 
            // vtbSeriaTo
            // 
            this.vtbSeriaTo.Location = new System.Drawing.Point(221, 21);
            this.vtbSeriaTo.Name = "vtbSeriaTo";
            this.vtbSeriaTo.Size = new System.Drawing.Size(98, 20);
            this.vtbSeriaTo.StringAutoCorrectionMethod = null;
            this.vtbSeriaTo.TabIndex = 2;
            this.vtbSeriaTo.ValidateValue = null;
            this.vtbSeriaTo.ValueTxt = "";
            // 
            // vtbSeriaFrom
            // 
            this.vtbSeriaFrom.Location = new System.Drawing.Point(117, 21);
            this.vtbSeriaFrom.Name = "vtbSeriaFrom";
            this.vtbSeriaFrom.Size = new System.Drawing.Size(98, 20);
            this.vtbSeriaFrom.StringAutoCorrectionMethod = null;
            this.vtbSeriaFrom.TabIndex = 1;
            this.vtbSeriaFrom.ValidateValue = null;
            this.vtbSeriaFrom.ValueTxt = "";
            // 
            // vtbProdName
            // 
            this.vtbProdName.Location = new System.Drawing.Point(6, 21);
            this.vtbProdName.Name = "vtbProdName";
            this.vtbProdName.Size = new System.Drawing.Size(105, 20);
            this.vtbProdName.StringAutoCorrectionMethod = null;
            this.vtbProdName.TabIndex = 0;
            this.vtbProdName.ValidateValue = null;
            this.vtbProdName.ValueTxt = "";
            // 
            // btAddOper
            // 
            this.btAddOper.Location = new System.Drawing.Point(325, 5);
            this.btAddOper.Name = "btAddOper";
            this.btAddOper.Size = new System.Drawing.Size(85, 36);
            this.btAddOper.TabIndex = 23;
            this.btAddOper.Text = "Добавить";
            this.btAddOper.UseVisualStyleBackColor = true;
            this.btAddOper.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btAddOper_MouseClick);
            // 
            // fmOperationViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 637);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbOperItems);
            this.Controls.Add(this.gpAddOperApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(457, 659);
            this.MinimizeBox = false;
            this.Name = "fmOperationViewer";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Редактор операций";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fmOperationViewer_KeyUp);
            this.gbOperItems.ResumeLayout(false);
            this.gbOperItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOperApply)).EndInit();
            this.gbPlat.ResumeLayout(false);
            this.gbPlat.PerformLayout();
            this.gbLoverCoff.ResumeLayout(false);
            this.gbLoverCoff.PerformLayout();
            this.gbRtn.ResumeLayout(false);
            this.gbRtn.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gpAddOperApply.ResumeLayout(false);
            this.gpAddOperApply.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOperItems;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gbPlat;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox gbRtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbKindPay;
        private ValidatingTextBox vtbOperNum;
        private ValidatingTextBox vtbDepRegion;
        private ValidatingTextBox vtbTaskNum;
        private ValidatingTextBox vtbWorkRate;
        private ValidatingTextBox vtbPreparTimePayNorm;
        private ValidatingTextBox vtbItemPayNorm;
        private ValidatingTextBox vtbCoeffCTN;
        private ValidatingTextBox vtbPreparTimeCTN;
        private ValidatingTextBox vtbItemCTN;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btSaveFrom;
        private System.Windows.Forms.Button btPrev;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.GroupBox gpAddOperApply;
        private System.Windows.Forms.Button btUpdateForm;
        private System.Windows.Forms.Button btSaveAndLimit;
        private ValidatingTextBox vtbProdName;
        private ValidatingTextBox vtbSeriaFrom;
        private ValidatingTextBox vtbSeriaTo;
        private System.Windows.Forms.Button btLimitedOper;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btAddOper;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private ValidatingTextBox vtbLoverCoeff;
        private System.Windows.Forms.GroupBox gbLoverCoff;
        private System.Windows.Forms.Button btLovCofCount;
        private System.Windows.Forms.ComboBox cbDocNum;
        private System.Windows.Forms.DataGridView dgOperApply;
        private ValidatingComboBox vcbProfCode;


    }
}