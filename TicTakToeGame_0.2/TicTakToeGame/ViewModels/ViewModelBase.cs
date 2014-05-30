using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Odbc;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TicTakToeGame.ViewModels
{
    public abstract class ViewModelBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// Метод определяюший имя по Lambda выражению
        public void NotifyOnPropertyChange<T>(Expression<Func<T>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null) throw new ArgumentException("Invalid lambda expression.", "expression");
            var property = memberExpression.Member as MemberInfo;
            if (property == null) throw new ArgumentException("Invalid lambda expression. Expression must be a property.", "expression");


            string propertyName = memberExpression.Member.Name;
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
