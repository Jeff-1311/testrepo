using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ErikEJ.SqlCe;

namespace Trudoyomkost


{
    public static class InsertIntoDb
    {
        private static String connString = Properties.Settings.Default.TrudoyomkostDBConnectionString;

        public static void InsertDt<T>(ref T dtInput, string tableName)
            where T: DataTable
        {
            SqlCeBulkCopy bulkInsert = new SqlCeBulkCopy(connString);
            DataTable tmptable = dtInput;

            if (tmptable.Rows.Count > 0)
            {
                bulkInsert.DestinationTableName = tableName;
                bulkInsert.WriteToServer(tmptable);
            }
            dtInput.Clear();
            bulkInsert.Close();
            bulkInsert.Dispose();
            tmptable.Dispose();
        }
    }
}
