using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace Trudoyomkost
{
    class SDFProvider
    {

        private string connStrin = Properties.Settings.Default.TrudoyomkostDBConnectionString;
        List<string> commandList = new List<string> {
            //"CREATE TABLE [Users] ([ID] int NOT NULL IDENTITY  (1 , 1) , [UserName] nvarchar(12) NOT NULL, [UserPass] nvarchar(12) NOT NULL, [UserRole] tinyint NOT NULL);" ,
            //"ALTER TABLE [Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY ([ID]);",
            //"CREATE UNIQUE INDEX [UQ__Users__00000000000006A3] ON [Users] ([UserName] ASC);",
            //"CREATE UNIQUE INDEX [UQ__Users__00000000000006B8] ON [Users] ([ID] ASC);",
            //"INSERT INTO [Users] ([UserName] ,[UserPass] ,[UserRole])  VALUES ('admin','admin' ,1)",
            // "INSERT INTO [Users] ([UserName] ,[UserPass] ,[UserRole])  VALUES ('user' ,'user', 2)",
            //"INSERT INTO [Users] ([UserName] ,[UserPass] ,[UserRole])  VALUES ('pdbuser', 'pdbuser', 3)",
             "UPDATE infDet SET  PKP = 'Д' WHERE (PKP = 'Д ')",
        };
         
       
         public void UpdatePKP()
         {
            try
            {
             SqlCeConnection sq = new SqlCeConnection(connStrin);
             sq.Open();
             foreach (var item in commandList)
             {
                 SqlCeCommand currentCommand = new SqlCeCommand(item, sq);
                 
                 currentCommand.ExecuteNonQuery();
             }
             sq.Close();
             }
             catch(SqlCeException ex)
                 {
                     return;
                 }
         }

    }

}
