using System;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ADOX;
using ACCESS = Microsoft.Office.Interop.Access;  
using DAO = Microsoft.Office.Interop.Access.Dao;
namespace Trudoyomkost
{
    class MDBProvider
    {

        private string FilePath = TrudoyomkostSettings.MdbFileDir + "\\db" + TrudoyomkostSettings.DepNum.ToString() + "_Для_ОАСУП.mdb";
        ADOX.CatalogClass MDB = new ADOX.CatalogClass();
        private String GetStrConnMDB()
        {
            String StrConnMDB = String.Format(""
                                                          + "Provider=Microsoft.Jet.OLEDB.4.0; "
                                                          + "Data Source={0}; "
                                                          + "Jet OLEDB:Engine Type=5"
                                            + "", FilePath);
            return StrConnMDB;
        }
        public void CreateDB()
        {
            String StrConnMDB = GetStrConnMDB();
            
            // Удалим если есть старый файл
            if (System.IO.File.Exists(FilePath))
                System.IO.File.Delete(FilePath);

            // Создадим новый файл
            MDB.Create(StrConnMDB);
          
        }



        public void CreateTNTable(string nameTable, DataTable inputTable)
        {
            StringBuilder tempStr = new StringBuilder();
            tempStr.AppendFormat("CREATE TABLE {0}(", nameTable);

            foreach (DataColumn column in inputTable.Columns)
            {

                Type t = column.DataType;
                PropertyInfo[] pis = t.GetProperties();

                tempStr.Append(column.ColumnName + " " + t.Name );
                if(column.DataType ==typeof(string))
                   tempStr.Append("("+column.MaxLength+")");
                tempStr.Append(", ");
            }
            tempStr.Remove(tempStr.Length - 2, 1);
            tempStr.Append(");");

            Querry(tempStr.ToString(), FilePath);
        
        }
        public void InsertTNTable(string nameTable, DataTable inputTable)
        {
            StringBuilder insertSQL = new StringBuilder("INSERT INTO TEMP (" + nameTable + "(");
        }

        public void Querry(string Que, string DataBasePath)
        {
            String StrConnMDB = GetStrConnMDB();
            OleDbConnection connect = new OleDbConnection(StrConnMDB);
            connect.Open();
            using (OleDbCommand command = new OleDbCommand(Que, connect))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show("Произошла ошибка при создании таблицы\n" + ex.Message);
                    
                }
            }
            connect.Close();
        }

        #region експорт в Аксес

        public void bulkInsert(DataTable inputTable)
        {
            DAO.DBEngine dbEngine = new DAO.DBEngine();
            DAO.Database db = dbEngine.OpenDatabase(FilePath);
            DAO.Recordset rs = db.OpenRecordset(inputTable.TableName);
            DAO.Field[] myFields = new DAO.Field[inputTable.Columns.Count];
            int k = 0;
            foreach (DataColumn column in inputTable.Columns)
            {
                myFields[k] = rs.Fields[column.ColumnName];
                k++;
            }
            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                rs.AddNew();
                k = 0;
                foreach (DataColumn column in inputTable.Columns)
                {
                    Type t = column.DataType;
                    if (t == typeof(string))
                        myFields[k].Value = inputTable.Rows[i].Field<string>(k);
                    else
                        myFields[k].Value = inputTable.Rows[i].Field<Single>(k);
                    k++;
                }
                rs.Update();
            }
            rs.Close();
            db.Close();
        }

        public void simpleInsert(DataTable inputTable)
        {
            string path = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+FilePath+";Persist Security Info=True";
            OleDbConnection thisConnection = new OleDbConnection(path);
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter();
            thisAdapter.InsertCommand = new OleDbCommand();
          
            thisAdapter.InsertCommand.CommandText = "INSERT INTO " + inputTable.TableName+" (";
            foreach (DataColumn column in inputTable.Columns)
            {
                if (inputTable.Columns.IndexOf(column.ColumnName) != (inputTable.Columns.Count-1))
                    thisAdapter.InsertCommand.CommandText += column.ColumnName + ", ";
                else
                {
                    thisAdapter.InsertCommand.CommandText += column.ColumnName + ") VALUES ";
                }

            }
            thisConnection.Open();
            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                string temp = thisAdapter.InsertCommand.CommandText;

                thisAdapter.InsertCommand.CommandText += "(";
               int k = 0;
                foreach (DataColumn column in inputTable.Columns)
                {
                    

                    Type t = column.DataType;
                    if (t == typeof(string))
                    {
                        thisAdapter.InsertCommand.CommandText += "'" + inputTable.Rows[i].Field<string>(k) + "'";
                    }
                    else
                    {
                        string test = inputTable.Rows[i].Field<Single>(k).ToString();
                        thisAdapter.InsertCommand.CommandText += "'"+ inputTable.Rows[i].Field<Single>(k)+ "'";
                    }
                    k++;
                    if (inputTable.Columns.IndexOf(column.ColumnName) != (inputTable.Columns.Count - 1))
                        thisAdapter.InsertCommand.CommandText += ",";
                   
                }
                    thisAdapter.InsertCommand.CommandText += ");";

                    thisAdapter.InsertCommand.Connection = thisConnection;

                    thisAdapter.InsertCommand.ExecuteNonQuery();


                thisAdapter.InsertCommand.CommandText = temp;

            }
           
            thisConnection.Close();
        }
#endregion

    }
}