using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RegionSyd.Model.Command
{
    public class NavigationCommand : ICommand
    {

        Action MethodExecute { get; set; }
        public event EventHandler CanExecuteChanged;

        public NavigationCommand(Action execute)
        {
            MethodExecute = execute;
        }

        // this is literally useless, considering canexecute always returns true
        // but intellisense will literally shit my pants if this is not here
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        // can always navigate
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MethodExecute();
        }
    }
}
