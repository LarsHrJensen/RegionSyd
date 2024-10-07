using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RegionSyd.Model.Command
{
    public class RelayCommand : ICommand
    {
        Predicate<object> MethodCanExecute { get; set; }
        Action<object> MethodExecute { get; set; }

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            MethodCanExecute = canExecute;
            MethodExecute = execute;
        } 

        // Raise all elements this is bound to
        // Button for one
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return MethodCanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            MethodExecute(parameter);
        }

    }
}
