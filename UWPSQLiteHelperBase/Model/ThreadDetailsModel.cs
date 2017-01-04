using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSQLiteHelperBase.Model
{
    public class ThreadDetailsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        virtual internal protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private int id;

        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("ID");
                    }
        }

        private string guid;

        public string Guid
        {
            get { return guid; }
            set { guid = value;
                OnPropertyChanged("Guid");
            }
        }

        private string owner;

        public string Owner
        {
            get { return owner; }
            set { owner = value;
                OnPropertyChanged("Owner");
            }
        }

        private string threadurl;

        public string ThreadURL
        {
            get { return threadurl; }
            set { threadurl = value;
                OnPropertyChanged("ThreadURL"); }
        }
        //Silly mistake...Here my OnPropertyChanged name is Uri... I don't know why I set it and I used 2days to debug
        //Next time I should first navigate to this property to see whether it is triggered....
        private string threadtitle;

        public string ThreadTitle
        {
            get { return threadtitle; }
            set { threadtitle = value;
                OnPropertyChanged("ThreadTitle");
            }
        }

        private string casetype;

        public string Casetype
        {
            get { return casetype; }
            set { casetype = value;
                OnPropertyChanged("Casetype");
            }
        }

        private string isanswered;

        public string IsAnswered
        {
            get { return isanswered; }
            set { isanswered = value; }
        }



    }
}
