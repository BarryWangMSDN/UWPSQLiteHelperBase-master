using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.IO;
using Windows.Storage;
using UWPSQLiteHelperBase.Model;
using System.Diagnostics;


namespace UWPSQLiteHelperBase.Helper
{
    public class SqliteHelper2
    {
        #region HelperMethod
        /// <summary> 
        /// Writes SQLite.NET trace to the Debug window. 
        /// </summary> 
        public class DebugTraceListener : ITraceListener
        {
            public void Receive(string message)
            {
                Debug.WriteLine(message);
            }
        }
        #endregion
        private static SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection(
                    new SQLitePlatformWinRT(),
                    Path.Combine(ApplicationData.Current.LocalFolder.Path, "test.db"));
            }
        }

        public List<PeopleModel> ReadPeopleTable()
        {
            using (var db = DbConnection)
            {
                db.TraceListener = new DebugTraceListener();
                List<PeopleModel> people= (from p in db.Table<PeopleModel>()
                                           select p).ToList();
                return people;
            }
        }

        internal void CreateThreadsTable()
        {
            using (var db = DbConnection)
            {
                var createtableresult = db.CreateTable<ThreadDetailsModel>();
                var info = db.GetMapping(typeof(ThreadDetailsModel));
            }
              
        }

        public List<ThreadDetailsModel> ReadThredsTable()
        {
            using (var db = DbConnection)
            {
                db.TraceListener = new DebugTraceListener();
                List<ThreadDetailsModel> threads = (from p in db.Table<ThreadDetailsModel>()
                                            select p).ToList();
                return threads;
            }
        }


    }
}
