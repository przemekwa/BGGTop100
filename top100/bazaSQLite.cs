using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;


namespace top100
{
    class bazaSQLite
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        public bazaSQLite()
        {
            sql_con = new SQLiteConnection("Data Source=top100H.db;Version=3;New=False;Compress=True;");

 
        }
    }
}
