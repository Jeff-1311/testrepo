using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace Trudoyomkost
{
    static class ImportAnTables
    {

        public static string conectionString = Trudoyomkost.Properties.Settings.Default.ODLDB210410ConnectionString;

        public static DataSet FillData()
        {
            using (OleDbConnection connection = new OleDbConnection())
            {
                connection.ConnectionString = conectionString;

               
                OleDbDataAdapter da = new OleDbDataAdapter();
                
                DataSet myData = new DataSet();
                List<string> tableNames = new List<string>() {  
                                                                "AN148_" + TrudoyomkostSettings.DepNum.ToString(), 
                                                                "AN158_"+ TrudoyomkostSettings.DepNum.ToString(),
                                                                "AN178_"+ TrudoyomkostSettings.DepNum.ToString()
                                                                };
                try
                {
                    foreach (var item in tableNames)
                    {
                        var cmd = connection.CreateCommand();
                        cmd.CommandText = "SELECT *FROM " + item;
                        da.SelectCommand = cmd;
                        da.FillSchema(myData, SchemaType.Source, item);
                        da.Fill(myData, item);
                    }
                }
                catch (OleDbException ex)
                {
                    return myData;
                    connection.Close();
                }

                return myData;
                connection.Close();
            }

        }
  
    }
}
