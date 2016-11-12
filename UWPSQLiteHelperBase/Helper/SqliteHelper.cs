using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;

namespace UWPSQLiteHelperBase.Helper
{
    class SqliteHelper
    {
        #region
        //Predefined string names used for sqlite
        private static String DB_NAME = "SQLiteSample.db";
        private static String TABLE_NAME = "SampleTable";
        private static String SQL_CREATE_TABLE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " (Key TEXT,Value TEXT);";
        private static String SQL_QUERY_VALUE = "SELECT Value FROM " + TABLE_NAME + " WHERE Key = (?);";
        private static String SQL_INSERT = "INSERT INTO " + TABLE_NAME + " VALUES(?,?);";
        private static String SQL_UPDATE = "UPDATE " + TABLE_NAME + " SET Value = ? WHERE Key = ?";
        private static String SQL_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Key = ?";
        #endregion

        

        public void sqlite_createtable(string db,string tablename)
        {
            var _connection = new SQLiteConnection(db);
            using (var statement = _connection.Prepare(create_table_command(tablename)))
            {
                statement.Step();
            }

            //test code
            
        }

        private string create_table_command(string table_name)
        {
            return "CREATE TABLE IF NOT EXISTS " + table_name + " (Key TEXT,Value TEXT);";
        }

     



    }
}
