using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteHelperBase.Model;

namespace UWPSQLiteHelperBase.ViewModel
{

    public class ManagementVM
    {
        private ObservableCollection<PeopleModel> defaultpeoplemodel = new ObservableCollection<PeopleModel>();

        public ObservableCollection<PeopleModel> DefaultPeopleModel { get { return this.defaultpeoplemodel ; } }



        public ManagementVM()
        {
            //Here we can try fill data in this VM
            this.defaultpeoplemodel.Add(new PeopleModel { ID = 1, Alias = "v-barryw", Name = "Barry Wang", PermissionLevel = 1 });
            this.defaultpeoplemodel.Add(new PeopleModel { ID = 2, Alias = "v-doxie", Name =  "Dongwei Xie", PermissionLevel = 1 });
            this.defaultpeoplemodel.Add(new PeopleModel { ID = 3, Alias = "v-qianfu", Name = "Fu Qiang", PermissionLevel = 2 });
            this.defaultpeoplemodel.Add(new PeopleModel { ID = 4, Alias = "v-zhenma", Name = "Ma ZhengGang", PermissionLevel = 2 });
            this.defaultpeoplemodel.Add(new PeopleModel { ID = 5, Alias = "v-zhenwu", Name = "Zhengdong Wei", PermissionLevel = 2 });

        }
           
    }
}
