using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteHelperBase.Model;
using UWPSQLiteHelperBase.Helper;

namespace UWPSQLiteHelperBase.ViewModel
{
   public class ThreadDetailsVM
    {
        private ObservableCollection<ThreadDetailsModel> threadsmodel = new ObservableCollection<ThreadDetailsModel>();

        public ObservableCollection<ThreadDetailsModel> Threadsmodel { get { return this.threadsmodel; } }

        public ThreadDetailsVM()
        {
            SqliteHelper2 helper = new SqliteHelper2();
            helper.CreateThreadsTable();
        }
    }
}
