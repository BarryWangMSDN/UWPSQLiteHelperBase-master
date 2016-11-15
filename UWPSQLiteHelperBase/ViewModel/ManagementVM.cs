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

    public class ManagementVM
    {
        #region ManagementProperties

        private ObservableCollection<PeopleModel> defaultpeoplemodel = new ObservableCollection<PeopleModel>();

        public ObservableCollection<PeopleModel> DefaultPeopleModel { get { return this.defaultpeoplemodel ; } }


        #endregion

        public ManagementVM()
        {
            manuallyfilldata();
        }

        private void manuallyfilldata()
        {
            //Here we manually fill data in this VM
            //this.defaultpeoplemodel.Add(new PeopleModel { ID = 1, Alias = "v-barryw", Name = "Barry Wang", PermissionLevel = 1 });
            //this.defaultpeoplemodel.Add(new PeopleModel { ID = 2, Alias = "v-doxie", Name = "Dongwei Xie", PermissionLevel = 1 });
            //this.defaultpeoplemodel.Add(new PeopleModel { ID = 3, Alias = "v-qianfu", Name = "Fu Qiang", PermissionLevel = 2 });
            //this.defaultpeoplemodel.Add(new PeopleModel { ID = 4, Alias = "v-zhenma", Name = "Ma ZhengGang", PermissionLevel = 2 });
            //this.defaultpeoplemodel.Add(new PeopleModel { ID = 5, Alias = "v-zhenwu", Name = "Zhengdong Wei", PermissionLevel = 2 });
            //this.defaultpeoplemodel.Add(new PeopleModel { ID = 6, Alias = "v-wngwei", Name = "Wang Wei", PermissionLevel = 2 });
            //this.defaultpeoplemodel.Add(new PeopleModel { ID = 7, Alias = "v-liuson", Name = "Liu Song", PermissionLevel = 2 });
            //this.defaultpeoplemodel.Add(new PeopleModel { ID = 8, Alias = "n/a", Name = "MingHao Zhu", PermissionLevel = 2 });
            //this.defaultpeoplemodel.Add(new PeopleModel { ID = 9, Alias = "n/a", Name = "Xiaoxiao Ma", PermissionLevel = 2 });
            SqliteHelper2 helper = new SqliteHelper2();
            List<PeopleModel> peoplelist=helper.readtable("test.db");
            peoplelist.ForEach(p => defaultpeoplemodel.Add(p));//transfer List<T> to ObservableCollection
        }
           
    }
}
