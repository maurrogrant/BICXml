using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using System.Data.SQLite;

namespace BICXml
{
    public class SqlDbase
    {
        public void CreateDbase()
        {
            //SQLiteConnection con;
            //SQLiteCommand cmd;

            //if (!File.Exists("BICdata.sqlite"))
            //{
            //    SQLiteConnection.CreateFile("BICdata.sqlite");

            //    string sql = @"CREATE TABLE BIC_DATA(
            //                   ID INTEGER PRIMARY KEY AUTOINCREMENT ,
            //                   BIC           INTEGER      NOT NULL,
            //                   ORG_NAME      TEXT       NOT NULL,
            //                   ADRESS        TEXT
            //                );";
            //    con = new SQLiteConnection("Data Source=BICdata.sqlite;Version=3;");
            //    con.Open();
            //    cmd = new SQLiteCommand(sql, con);
            //    cmd.ExecuteNonQuery();
            //    con.Close();

            //}
            //else
            //{
            //    con = new SQLiteConnection("Data Source=BICdata.sqlite;Version=3;");
            //    con.Open();

            //    string InsertSql = @"INSERT INTO BIC_DATA 
            //            (ID, BIC, ORG_NAME, ADRESS) 
            //            values ($id, $bic, $NameP, $Adress)";

            //    int i = 0;

            //    foreach (var item in BICListCollection)
            //    {
            //        SQLiteCommand InsertCom = new SQLiteCommand(InsertSql, con);
            //        InsertCom.Parameters.Add("$id", DbType.Int32).Value = i++;
            //        InsertCom.Parameters.Add("$bic", DbType.Int32).Value = item.BIC_num;
            //        InsertCom.Parameters.Add("$NameP", DbType.String).Value = item.OgranizationName;
            //        InsertCom.Parameters.Add("$Adress", DbType.String).Value = item.Adress;
            //        InsertCom.ExecuteNonQuery();
            //    }
            //    con.Close();
            //}
        }
    }
}
