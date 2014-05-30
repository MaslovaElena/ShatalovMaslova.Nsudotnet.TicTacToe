using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Odbc;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TicTakToeGame.ViewModels
{
    public abstract class ViewModelBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// Метод определяюший имя по Lambda выражению
        public void NotifyOnPropertyChange<T>(Expression<Func<T>> expression)
        {
            /// Получаем тело выражения, из которого берем имя свойства
            dynamic l = expression.Body;
            string propertyName = l.Member.Name;
            OnPropertyChanged(propertyName);
        }

        public void OnPropertyChanged(string propertyName)
        {
            Console.WriteLine("Attempt to change " + propertyName);

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
