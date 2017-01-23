using System;
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
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace UWPSQLiteHelperBase.ViewModel
{
   public class ThreadDetailsVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Helper Class
        SqliteHelper2 sqlhelper = new SqliteHelper2();
        CSVParser parser = new CSVParser();
        CSVParserComponent.CsvParser parser2 = new CSVParserComponent.CsvParser();
        CommonHelper helper = new CommonHelper();
        #endregion
        #region property
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

        public string csvtext { get; set; }
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
        public async void insertrecordcommand(object detail)
        {
            var x = ((ThreadDetailsVM)detail).TextModel;
            if (Testthreadtype.Substatus != null)
            {
                x.Casetype = Testthreadtype.Substatus;//here should handle null value
                threadsmodel.Add(x);
            }
           else
            {
             await helper.ShowMessage("SubStatus is null, please check choose it");
            }  
           
            try
            {
                sqlhelper.InsertorReplaceThreadTable(x);
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
                    sqlhelper.DeleteFromThreadTable(y[i]);
                    //delete from collection
                    y.RemoveAt(i);                 
                }
            }         

        }

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(p => SaveRecordsCommand()));
            }
        }

        public void SaveRecordsCommand()
        {
            foreach(var item in threadsmodel)
            {
                 sqlhelper.InsertorReplaceThreadTable(item);
            }
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
            if((sender as ListView).SelectedItem!=null)
            {
                ThreadDetailsModel x = (sender as ListView).SelectedItem as ThreadDetailsModel;
                textmodel.Guid = x.Guid;
                textmodel.ThreadURL = x.ThreadURL;
                textmodel.Owner = x.Owner;
                textmodel.ThreadTitle = x.ThreadTitle;
                textmodel.Casetype = x.Casetype;
            }
           
        }

        public async void FilterReport_Click(object sender, RoutedEventArgs e)
        {
            //query Barry Answered Volume
            var query = threadsmodel.Count(p => p.IsAnswered == "Yes"&&p.Owner=="Barry Wang");
            
        }


        public async void LoadFileBtn_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker filepicker = new FileOpenPicker();
            filepicker.ViewMode = PickerViewMode.Thumbnail;
            filepicker.SuggestedStartLocation = PickerLocationId.Desktop;
            filepicker.FileTypeFilter.Add(".csv");
            StorageFile file = await filepicker.PickSingleFileAsync();
            if (file != null)
            {
                var csvtext=await helper.ReadStringFromFile(file);
                //parser.RawText = csvtext;               
                //parser.HasHeaderRow = true;
                //List<Dictionary<string, string>> parserresult =parser.Parse();
                parser2.RawText = csvtext;
                var testresult = await parser2.Parse();
                //parserresult.RemoveAt(parserresult.Count-1);
                //try
                //{
                    foreach (var result in testresult)
                    {
                        ThreadDetailsModel recorditem = new ThreadDetailsModel();                  
                        recorditem.Casetype = result["Sub Status"];
                        recorditem.Guid = result["External ID (Thread)"];
                        recorditem.IsAnswered = result["Is Answered (Thread)"];
                        recorditem.Owner = result["Owner"];
                        recorditem.ThreadTitle = result["Title"];
                        recorditem.ThreadURL = result["URL (Thread)"];
                        //add to collection
                        threadsmodel.Add(recorditem);
                    //insert to database   
                    
                }
              
                //}
                //catch(Exception ex)
                //{
                //    Debug.Write(ex.Message.ToString());
                //}

            }
        }
        #endregion
        public ThreadDetailsVM()
        {
            //ListView data refersh
           
            sqlhelper.CreateThreadsTable();        
            List<ThreadDetailsModel> peoplelist = sqlhelper.ReadThredsTable();
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
