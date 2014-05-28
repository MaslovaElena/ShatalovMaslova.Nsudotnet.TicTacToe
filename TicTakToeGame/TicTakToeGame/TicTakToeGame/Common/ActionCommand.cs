using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using TicTakToeGame.ViewModels;

namespace TicTakToeGame.Common
{
    public class ActionCommand:ICommand 
    {
        private Func<object, bool> canExecute;
        private Action<object> execute;

        public ActionCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.canExecute = canExecute;
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

    }
}
