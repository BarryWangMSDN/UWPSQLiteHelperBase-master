using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Collections.ObjectModel;
using UWPSQLiteHelperBase.Model;

namespace UWPSQLiteHelperBase.Helper
{
    class SqliteHelper1
    {
        #region
        //Predefined string names used for sqlite
        private static String DB_NAME = "test.db";
        private static String TABLE_NAME = "SampleTable";
        private static String SQL_CREATE_TABLE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " (Key TEXT,Value TEXT);";
        private static String SQL_QUERY_VALUE = "SELECT Value FROM " + TABLE_NAME + " WHERE Key = (?);";
        private static String SQL_INSERT = "INSERT INTO " + TABLE_NAME + " VALUES(?,?);";
        private static String SQL_UPDATE = "UPDATE " + TABLE_NAME + " SET Value = ? WHERE Key = ?";
        private static String SQL_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Key = ?";
        #endregion

        SQLiteConnection _connection;



        private static string dbPath = string.Empty;
        private static string DbPath
        {
            get
            {
                if (string.IsNullOrEmpty(dbPath))
                {
                    dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Sqlite.db");
                }

                return dbPath;
            }
        }

        public void sqlite_createtable(string tablename)
        {           
             _connection = new SQLiteConnection(DB_NAME);
            using (var statement = _connection.Prepare(create_table_command(tablename)))
            {
                statement.Step();
            }
    
        }

        private string create_table_command(string table_name)
        {
            return "CREATE TABLE IF NOT EXISTS " + table_name + "(Id INT,Alias TEXT,Name TEXT,Level INT);";
        }

        
        


    }
}
