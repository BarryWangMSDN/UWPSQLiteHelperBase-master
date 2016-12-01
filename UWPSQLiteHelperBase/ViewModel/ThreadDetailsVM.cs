using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteHelperBase.Model;
using UWPSQLiteHelperBase.Helper;
using System.Windows.Input;

namespace UWPSQLiteHelperBase.ViewModel
{
   public class ThreadDetailsVM
    {
        private ObservableCollection<ThreadDetailsModel> threadsmodel = new ObservableCollection<ThreadDetailsModel>();
        public ObservableCollection<ThreadDetailsModel> Threadsmodel { get { return this.threadsmodel; } }


        private ThreadDetailsModel textmodel = new ThreadDetailsModel();
        public ThreadDetailsModel TextModel { get { return this.textmodel; } }

       

        private ICommand insertCommand;
        public ICommand InsertCommand
        {
            get
            {
                return insertCommand ?? (insertCommand = new RelayCommand(p=>testmethod(p)));
            } 
        }

        public void testmethod(object detail)
        {
            threadsmodel.Add((ThreadDetailsModel)detail);
        }

        public ThreadDetailsVM()
        {
            //ListView data refersh
            SqliteHelper2 helper = new SqliteHelper2();
            helper.CreateThreadsTable();        
            List<ThreadDetailsModel> peoplelist = helper.ReadThredsTable();
            peoplelist.ForEach(p => threadsmodel.Add(p));
            //Textbox data initialization
            textmodel.Guid = "dc505f68-d120-43e3-a9e1-d7c77746d588";
            textmodel.ThreadURL = "https://social.msdn.microsoft.com/Forums/windowsapps/en-US/dc505f68-d120-43e3-a9e1-d7c77746d588";
            textmodel.Owner = "v-barryw";
            textmodel.ThreadTitle = "[UWP][Desktop Bridge]unplated taskbar icons in Desktop Bridge apps";
        }
    }
}
