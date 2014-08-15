using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing.Charts;
using ErikEJ.SqlCe;
using Trudoyomkost.Classes;
using DataTable = System.Data.DataTable;
using System.Data.DataSetExtensions;
namespace Trudoyomkost
{
    public partial class dobavlenieNorm : Form
    {

        private FormOkTariff formOkTariff;

        private mainForm mf;
    
        
        public dobavlenieNorm(mainForm mainF)
        {
            InitializeComponent();
            mf = mainF;

           

            //Выводим то что было
            AddTarrif addTarif = new AddTarrif();
            
            DataTable tempDT = new DataTable();
            tempDT = addTarif.ReturnDataTable(@"SELECT * FROM infTariff ORDER BY TariffNetNum,KindPay, WorkerRate  ");
            dataGridViewTariff.DataSource = tempDT;
            labelTariff.Text = tempDT.Rows.Count.ToString();

            //В комбобокс заносим возможные варианты сетки
            DataTable dt = new DataTable();
            dt = addTarif.ReturnDataTable(@"SELECT DISTINCT TariffNetNum FROM infTariff");
            for (int i = 0; i < dt.Rows.Count; i++)
                comboBoxForTariffNetNum.Items.Add(dt.Rows[i]["TariffNetNum"].ToString());

            
            comboBoxForKindPay.Items.Add("С");//добовляем варианты в комбобокс для типа оплаты(Повременно/сдельно) внутри буквы на русском
            comboBoxForKindPay.Items.Add("П");


        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (
                (textBox1.Text != "") && (textBox2.Text != "") 
                && (textBox3.Text != "") && (textBox4.Text != "") 
                && (textBox5.Text != "") && (textBox6.Text != "")
                && (comboBoxForTariffNetNum.Text != "") && (comboBoxForKindPay.Text != "")  
                )
            {
             AddTarrif addTarif2 = new AddTarrif(); 
            DataTable dt2 = new DataTable();
            

            double a1 =double.Parse(textBox1.Text);
            double a2 =double.Parse(textBox2.Text); 
            double a3 =double.Parse(textBox3.Text); 
            double a4 =double.Parse(textBox4.Text); 
            double a5 =double.Parse(textBox5.Text); 
            double a6 = double.Parse(textBox6.Text);


            string variableForTariffNetNum = comboBoxForTariffNetNum.Text;
            string variableForKindPay = comboBoxForKindPay.Text; 


            ObrabotkaTarrif ot = new ObrabotkaTarrif(); //создаем обект для работы с класом который все делает
            
            dt2 = ot.NewTariff(a1, a2, a3, a4, a5, a6, variableForTariffNetNum, variableForKindPay);//в дататейбл заносим новую тарифную сетку


            
            deleteFromBD(variableForTariffNetNum, variableForKindPay); //удаляем старое с бд
            writteToBD(dt2);//записываем все в бд

            dataGridViewTariffVuvod.DataSource = dt2;

            DataTable tempDT = new DataTable();
            tempDT = addTarif2.ReturnDataTable(@"SELECT * FROM infTariff");
            dataGridViewTariff.DataSource = tempDT;
            labelTariff.Text = tempDT.Rows.Count.ToString();

            labelTariffVuvod.Text = dt2.Rows.Count.ToString();
            }
            else
                MessageBox.Show("заполните все поля");
            



        }


        private void deleteFromBD(string variableForTariffNetNum,string variableForKindPay)
        {
            AddTarrif addTarif = new AddTarrif();
            addTarif.deleteFromDB(@"DELETE FROM infTariff WHERE TariffNetNum = '" + variableForTariffNetNum + "' " + "AND" + " KindPay = '" + variableForKindPay + "'");
        }

        private void writteToBD(DataTable dt3)
        {
            String connString = Properties.Settings.Default.TrudoyomkostDBConnectionString;
            SqlCeBulkCopy bulkInsert = new SqlCeBulkCopy(connString);
            bulkInsert.DestinationTableName = "infTariff";
            bulkInsert.WriteToServer(dt3);
        }

        private void DobavlenieNorm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           // comboBoxForTariffNetNum.Enabled = false;
           // comboBoxForTariffNetNum.Visible = false;
            // FormOkTariff formOkTariff = new FormOkTariff();
           // formOkTariff.Show();
            formOkTariff = new FormOkTariff(this);
            formOkTariff.Show();
           

        }


        
        


        

        public void addItemList(string text)
        {

            comboBoxForTariffNetNum.Items.Add(text);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            buttonAllowAccess.Visible = true;
            textBoxForTariffPass2.Visible = true;
            
        }

        private void buttonAllowAccess_Click(object sender, EventArgs e)
        {
            if (textBoxForTariffPass2.Text == Properties.Settings.Default.TariffPass2)
            {

                button3.Visible = true;
                button3.Enabled = false;
                buttonAllowAccess.Visible = false;
                textBoxForTariffPass2.Visible = false;

                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                comboBoxForTariffNetNum.Enabled = true;
                    comboBoxForKindPay.Enabled = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
               


            }
            else
            {
                MessageBox.Show("Неверный пароль");
                textBoxForTariffPass2.Text = "";
                button3.Visible = true;
                buttonAllowAccess.Visible = false;
                textBoxForTariffPass2.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog().ToString() == "OK")
            {
                
           
                string tarifFile = openFileDialog1.FileName;

                AddTarrif at = new AddTarrif();

                DataTable tarifKind = at.ReturnDataTable(tarifFile, @"SELECT DISTINCT TariffNetNum, KindPay FROM infTariff ORDER BY TariffNetNum, KindPay");

                for (int i = 0; i < tarifKind.Rows.Count; i++)
                {
                    
                    DataTable tarifNet = new DataTable();
                    tarifNet = at.ReturnDataTable(tarifFile, @"SELECT * FROM infTariff WHERE TariffNetNum = '" + tarifKind.Rows[i]["TariffNetNum"].ToString() + "' AND KindPay = '" + tarifKind.Rows[i]["KindPay"].ToString() + "' ORDER BY TariffNetNum, KindPay, WorkerRate");

                    ObrabotkaTarrif ot = new ObrabotkaTarrif();
                    tarifNet = ot.NewTariff
                        (
                           double.Parse(tarifNet.Rows[0]["HourCost"].ToString()),
                           double.Parse(tarifNet.Rows[1]["HourCost"].ToString()),
                           double.Parse(tarifNet.Rows[2]["HourCost"].ToString()),
                           double.Parse(tarifNet.Rows[3]["HourCost"].ToString()),
                           double.Parse(tarifNet.Rows[4]["HourCost"].ToString()),
                           double.Parse(tarifNet.Rows[5]["HourCost"].ToString()),
                           tarifNet.Rows[0]["TariffNetNum"].ToString(),
                         tarifNet.Rows[0]["KindPay"].ToString()
                            
                        );//в дататейбл заносим новую тарифную сетку

                    at.deleteFromDB(@"DELETE FROM infTariff WHERE TariffNetNum = '" + tarifKind.Rows[i]["TariffNetNum"].ToString() + "' AND KindPay = '" + tarifKind.Rows[i]["KindPay"].ToString() + "'");

                //    at.deleteFromDB(tarifFile, @"DELETE FROM infTariff WHERE TariffNetNum = '" + tarifKind.Rows[i]["TariffNetNum"].ToString() + "' AND KindPay = '" + tarifKind.Rows[i]["KindPay"].ToString() + "'");

                    writteToBD(tarifNet);//записываем все в бд

                    dataGridViewTariffVuvod.DataSource = tarifNet;
                }

            }

        }


       
        



        private void button5_Click(object sender, EventArgs e)
        {

            this.Enabled = false;
            mf.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
          


            


        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {





            AddTarrif at = new AddTarrif(); // создаем обект аддтариф
            DataTable dt = new DataTable();
            string zapros = " SELECT * FROM LabourNorm ORDER BY ID";//селектим все кроме ID - оно будет автоматически ставится, и Valuation и ValPreparTime заместо них будут новые значения
            dt = at.ReturnDataTable(zapros); //Присваеваем дата тейблу содержание таблици 



            at.deleteFromDB("EXEC sp_rename 'LabourNorm', 'LabourNorm " + dt.Rows[0]["DocNum"] + "'");//переименовуем LabourNorm

            int lastID = (int)dt.Rows[dt.Rows.Count - 1]["ID"]; //Последний ID(Самый большой)


            for (int id = 0; id != lastID; id++)
            {



                if (id + 1 != (int)dt.Rows[id]["ID"])
                {

                    DataRow _ravi = dt.NewRow();
                    _ravi["ID"] = id + 1;
                    _ravi["infDetID"] = -1;
                    _ravi["OperNum"] = "---";
                    _ravi["DepRegion"] = -1;
                    _ravi["ProfCode"] = -1;
                    _ravi["NameKindWork"] = "";
                    _ravi["KindPay"] = "П";
                    _ravi["TariffNetNum"] = 1;
                    _ravi["WorkerRate"] = 1;
                    _ravi["ItemCTN"] = -1;
                    _ravi["PreparTimeCTN"] = -1;
                    _ravi["ItemPayNorm"] = -1;
                    _ravi["PreparTimePayNorm"] = -1;
                    _ravi["Valuation"] = -1;
                    _ravi["ValPreparTime"] = -1;
                    _ravi["CoeffCTN"] = -1;
                    _ravi["DocNum"] = "---";
                    _ravi["Date"] = "01.01.1980 00:00:00";
                    _ravi["TaskNumber"] = "---";
                    dt.Rows.InsertAt(_ravi, id);




                }


            }

            


            DataTable dataTableForTariff = new DataTable();
            dataTableForTariff = at.ReturnDataTable(" SELECT * FROM infTariff");


          






            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tariffNetNum = dt.Rows[i]["TariffNetNum"].ToString();
                string kindPay = dt.Rows[i]["KindPay"].ToString();
                string workerRate = dt.Rows[i]["WorkerRate"].ToString();
                double coef = (double.Parse(dt.Rows[i]["CoeffCTN"].ToString()) == 0) ? 1 : double.Parse(dt.Rows[i]["CoeffCTN"].ToString());
                double itemPayNorm = double.Parse(dt.Rows[i]["ItemPayNorm"].ToString());

                if (itemPayNorm == 0.4051)
                {
                    
                }


                var results = from DataRow myRow in dataTableForTariff.Rows
                              where (myRow["TariffNetNum"].ToString() == tariffNetNum) && (myRow["KindPay"].ToString() == kindPay) && (myRow["WorkerRate"].ToString() == workerRate)
                              select myRow;

                double currTarif;
                try
                {
                    currTarif = double.Parse(results.CopyToDataTable().Rows[0]["HourCost"].ToString());
                }
                catch 
                {
                    currTarif = 0;
                }
                
                    
                
                
               


                double preparTimePayNorm = double.Parse(dt.Rows[i]["PreparTimePayNorm"].ToString());






                string g = (itemPayNorm * currTarif).ToString("N" + Properties.Settings.Default.RoundNum);

                dt.Rows[i]["Valuation"] = (itemPayNorm * currTarif).ToString("N" + Properties.Settings.Default.RoundNum);
                dt.Rows[i]["ValPreparTime"] = (preparTimePayNorm * currTarif).ToString("N" + Properties.Settings.Default.RoundNum);



                dt.Rows[i]["DocNum"] = textBoxDataPrikaz.Text;
            }


       
            at.deleteFromDB("CREATE TABLE [LabourNorm]([ID] INT NOT NULL IDENTITY (1,1),[infDetID] INT NOT NULL,[OperNum] NVARCHAR(100),[DepRegion] FLOAT NOT NULL,[ProfCode] INT NOT NULL,[NameKindWork] NVARCHAR(50),[KindPay] NVARCHAR(1) NOT NULL,[TariffNetNum] TINYINT NOT NULL,[WorkerRate] FLOAT NOT NULL,[ItemCTN] FLOAT NOT NULL,[PreparTimeCTN] FLOAT NOT NULL,[ItemPayNorm] FLOAT NOT NULL,[PreparTimePayNorm] FLOAT NOT NULL,[Valuation] FLOAT NOT NULL,[ValPreparTime] FLOAT,[CoeffCTN] FLOAT NOT NULL,[DocNum] NVARCHAR(100),[Date] DATETIME NOT NULL,[TaskNumber] NVARCHAR(30));"); //очишаем таблицу перед записью
            String connString = Properties.Settings.Default.TrudoyomkostDBConnectionString;
            SqlCeBulkCopy bulkInsert = new SqlCeBulkCopy(connString);
            bulkInsert.DestinationTableName = "LabourNorm";
            bulkInsert.WriteToServer(dt);

            at.deleteFromDB("DELETE FROM LabourNorm WHERE infDetID ='-1' AND ProfCode = '-1'");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            mf.Enabled = true;

            this.Close();
            mf.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddTarrif at = new AddTarrif(); // создаем обект аддтариф
            DataTable dt = new DataTable();
            string zapros = " SELECT * FROM LabourNorm ORDER BY ID";//селектим все кроме ID - оно будет автоматически ставится, и Valuation и ValPreparTime заместо них будут новые значения
            dt = at.ReturnDataTable(zapros); //Присваеваем дата тейблу содержание таблици 

            int lastID = (int)dt.Rows[dt.Rows.Count - 1]["ID"]; //Последний ID(Самый большой)




            for (int id = 0; id != lastID; id++)
            {



                if (id + 1 != (int)dt.Rows[id]["ID"])
                {

                    DataRow _ravi = dt.NewRow();
                    _ravi["ID"] = id + 1;
                    _ravi["infDetID"] = -1;
                    _ravi["OperNum"] = "---";
                    _ravi["DepRegion"] = -1;
                    _ravi["ProfCode"] = -1;
                    _ravi["NameKindWork"] = "";
                    _ravi["KindPay"] = "П";
                    _ravi["TariffNetNum"] = 1;
                    _ravi["WorkerRate"] = 1;
                    _ravi["ItemCTN"] = -1;
                    _ravi["PreparTimeCTN"] = -1;
                    _ravi["ItemPayNorm"] = -1;
                    _ravi["PreparTimePayNorm"] = -1;
                    _ravi["Valuation"] = -1;
                    _ravi["ValPreparTime"] = -1;
                    _ravi["CoeffCTN"] = -1;
                    _ravi["DocNum"] = "---";
                    _ravi["Date"] = "01.01.1980 00:00:00";
                    _ravi["TaskNumber"] = "---";
                    dt.Rows.InsertAt(_ravi, id );

                    
                    

                }


            }

            dataGridViewTariffVuvod.DataSource = dt;

        }





    }
}

