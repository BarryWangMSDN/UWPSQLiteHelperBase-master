using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSQLiteHelperBase.Model
{
    public class PeopleModel
    {
        #region properties
        private int id;


        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;


        [MaxLength(64)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string alias;

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        private int permissionlevel;

        public int PermissionLevel
        {
            get { return permissionlevel; }
            set { permissionlevel = value; }
        }
        //1 means admin of this group


        #endregion


    }
}
