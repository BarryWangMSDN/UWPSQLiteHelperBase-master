﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteHelperBase.Model;
using UWPSQLiteHelperBase.Helper;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace UWPSQLiteHelperBase.ViewModel
{
   public class ThreadDetailsVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Helper Class
        SqliteHelper2 helper = new SqliteHelper2();
        #endregion
        #region property for binding
        //left panel listview model
        private ObservableCollection<ThreadDetailsModel> threadsmodel = new ObservableCollection<ThreadDetailsModel>();
        public ObservableCollection<ThreadDetailsModel> Threadsmodel { get { return this.threadsmodel; } }

        //right panel model
        private ThreadDetailsModel textmodel = new ThreadDetailsModel();
        public ThreadDetailsModel TextModel
        {
            get { return this.textmodel;              
            }
            set { textmodel = value;
                    }
        }

        private ObservableCollection<ThreadType> threadsubstatus = new ObservableCollection<ThreadType>();
        public ObservableCollection<ThreadType> Threadsubstatus { get { return this.threadsubstatus; } }

        private ThreadType testthreadtype;
        public ThreadType Testthreadtype
        {
            get { return testthreadtype; }
            set { testthreadtype = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Testthreadtype"));
            }
        }

        #endregion
        #region commands
        //Insert Record Command
        private ICommand insertCommand;
      
        public ICommand InsertCommand
        {
            get
            {
                return insertCommand ?? (insertCommand = new RelayCommand(p=> insertrecordcommand(p)));
            } 
        }
        public void insertrecordcommand(object detail)
        {
            var x = ((ThreadDetailsVM)detail).TextModel;
            x.Casetype = Testthreadtype.Substatus;//here should handle null value
            threadsmodel.Add(x);
            try
            {
                helper.InsertorReplaceThreadTable(x);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Insert to Database failed with the following reason"+ex.Message);
            }
        }


        private ICommand deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(p => deleterecordCommand(p)));
            }
        }

        public void deleterecordCommand(object detail)
        {
            var x = ((ThreadDetailsVM)detail).TextModel;
            var GuidToDelete = x.Guid;
            var y = ((ThreadDetailsVM)detail).threadsmodel;
              for(int i=0;i<y.Count;i++)
            {
                if(y[i].Guid==GuidToDelete)
                {
                    //delete from sqlite
                    helper.DeleteFromThreadTable(y[i]);
                    //delete from collection
                    y.RemoveAt(i);
                   
                }
            }
           
           

        }

        private ICommand pasteCommand;

        public ICommand PasteCommand
        {
            get
            {
                return pasteCommand ?? (pasteCommand = new RelayCommand(p => PasteTextCommand(p)));
            }
        }

        public void PasteTextCommand(object change)
        {

        }

        #endregion

        #region Events
        public void guidtxt_LostFocus(object sender, RoutedEventArgs e)
        {
            var x = e.OriginalSource;
            textmodel.Guid = ((TextBox)sender).Text;
            textmodel.ThreadURL = "https://social.msdn.microsoft.com/Forums/windowsapps/en-US/" + ((TextBox)sender).Text;
        }

        public void threadslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThreadDetailsModel x =(sender as ListView).SelectedItem as ThreadDetailsModel;
            textmodel.Guid = x.Guid;
            textmodel.ThreadURL = x.ThreadURL;
            textmodel.Owner = x.Owner;
            textmodel.ThreadTitle = x.ThreadTitle;
            textmodel.Casetype = x.Casetype;
        }
        #endregion
        public ThreadDetailsVM()
        {
            //ListView data refersh
           
            helper.CreateThreadsTable();        
            List<ThreadDetailsModel> peoplelist = helper.ReadThredsTable();
            peoplelist.ForEach(p => threadsmodel.Add(p));
            //Textbox data initialization(test data)
            textmodel.Guid = "dc505f68-d120-43e3-a9e1-d7c77746d588";
            textmodel.ThreadURL = "https://social.msdn.microsoft.com/Forums/windowsapps/en-US/dc505f68-d120-43e3-a9e1-d7c77746d588";
            textmodel.Owner = "v-barryw";
            textmodel.ThreadTitle = "[UWP][Desktop Bridge]unplated taskbar icons in Desktop Bridge apps";
            //ThreadStatus initialization
            threadsubstatus.Add(new ThreadType { Substatus = "SA" });
            threadsubstatus.Add(new ThreadType { Substatus = "EscalationFTE" });
            threadsubstatus.Add(new ThreadType { Substatus = "EscalationNonFTE" });
            threadsubstatus.Add(new ThreadType { Substatus = "Following" });
            threadsubstatus.Add(new ThreadType { Substatus = "BadRequirement" });
        }
    }
}
