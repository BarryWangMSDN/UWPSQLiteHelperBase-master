using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteHelperBase.Common;

namespace UWPSQLiteHelperBase.ViewModel
{
    class HostViewModel: BindableBase
    {
        private string nextButtonText;

        public HostViewModel()
        {
            this.NextButtonText = "Next";
        }

        public string NextButtonText
        {
            get { return this.nextButtonText; }
            set { this.SetProperty(ref this.nextButtonText, value); }
        }

    }
}
