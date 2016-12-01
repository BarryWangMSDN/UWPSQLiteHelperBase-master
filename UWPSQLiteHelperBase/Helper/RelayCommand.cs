using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UWPSQLiteHelperBase.Helper
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _handler;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _handler(parameter);
        }

        public RelayCommand(Action<object> action)
        {
            _handler = action;
        }
    }
}
