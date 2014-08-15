using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using DevExpress.Charts.Native;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Data.OleDb;
using System.Data.SqlServerCe;
using ErikEJ.SqlCe;
using DataTable = System.Data.DataTable;

namespace Trudoyomkost.Classes
{
    class AddTarrif
    {
        public DataTable ReturnDataTable(string zapros)
        {
            string connStr = Properties.Settings.Default.TrudoyomkostDBConnectionString;
            

            SqlCeConnection coon = new SqlCeConnection(connStr);
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = coon;
            cmd.CommandText = zapros;
            SqlCeDataAdapter adapter = new SqlCeDataAdapter(cmd);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            

            coon.Close();
            ////////////////////////

            return dataset.Tables[0];
        }



        public DataTable ReturnDataTable(string fileNameVsPathSdf ,string zapros)
        {
            string connStr = "Data Source= " +fileNameVsPathSdf;


            SqlCeConnection coon = new SqlCeConnection(connStr);
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = coon;
            cmd.CommandText = zapros;
            SqlCeDataAdapter adapter = new SqlCeDataAdapter(cmd);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);


            coon.Close();
            ////////////////////////

            return dataset.Tables[0];
        }

        public double CurrentTariff(string zapros)
        {
            try
            {
                string connStr = Properties.Settings.Default.TrudoyomkostDBConnectionString;


                SqlCeConnection coon = new SqlCeConnection(connStr);
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.Connection = coon;
                cmd.CommandText = zapros;
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);


                coon.Close();
                ////////////////////////

                return double.Parse(dataset.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception)
            {

                return 0;
            }
           
        }

        public void deleteFromDB(string fileNameVsPathSdf, string zapros)
        {
            string connStr = "Data Source= " + fileNameVsPathSdf;


            SqlCeConnection coon = new SqlCeConnection(connStr);
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = coon;
            cmd.CommandText = zapros;
            coon.Open();
            cmd.ExecuteNonQuery();


            coon.Close();
            ////////////////////////

        }

        public void deleteFromDB(string zapros)
        {
            string connStr = Properties.Settings.Default.TrudoyomkostDBConnectionString;


            SqlCeConnection coon = new SqlCeConnection(connStr);
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = coon;
            cmd.CommandText = zapros;
            coon.Open();
            cmd.ExecuteNonQuery();


            coon.Close();
            ////////////////////////

        }

      
    }

    class ObrabotkaTarrif

    {
        public DataTable NewTariff(double a1, double a2, double a3, double a4, double a5, double a6, string nomerTarifnoiSetki,string vidOplati)
        {

            List<double> ListWorkRate = new List<double>();
            ListWorkRate.Add(1);
            for (int g = 0; g < 50; g++)
            {
                ListWorkRate.Add(  ListWorkRate[g] + 0.1  );
            }



           List<double> mainList = new List<double>();
            mainList = VozvratZnachenii(a1, a2, mainList);
            mainList = VozvratZnachenii(a2, a3, mainList);
            mainList = VozvratZnachenii(a3, a4, mainList);
            mainList = VozvratZnachenii(a4, a5, mainList);
            mainList = VozvratZnachenii(a5, a6, mainList);




            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("TariffNetNum", typeof(string)));
            dt.Columns.Add(new DataColumn("KindPay", typeof(string)));
            dt.Columns.Add(new DataColumn("WorkerRate", typeof(string)));
            dt.Columns.Add(new DataColumn("HourCost", typeof(string)));

            ListWorkRate.Add(6);
            mainList.Add(a6);

            for (int i = 0; i < 51; i++)
            {
                DataRow row = dt.NewRow();
                row["TariffNetNum"] = nomerTarifnoiSetki;
                row["KindPay"] = vidOplati;
                row["WorkerRate"] = ListWorkRate[i];
                row["HourCost"] = mainList[i];
                dt.Rows.Add(row);

            }

            

            return dt;

        }

        private List<double> VozvratZnachenii(double first, double second, List<double> localList)
        {

            double tempFloat = (second - first) / 10;
            localList.Add(first);
            localList.Add(first + tempFloat);
            localList.Add(first + 2 * tempFloat);
            localList.Add(first + 3 * tempFloat);
            localList.Add(first + 4 * tempFloat);
            localList.Add(first + 5 * tempFloat);
            localList.Add(first + 6 * tempFloat);
            localList.Add(first + 7 * tempFloat);
            localList.Add(first + 8 * tempFloat);
            localList.Add(first + 9 * tempFloat);
            return localList;

        }






    }
}
