using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TicTakToeGame.ViewModels;

namespace TicTakToeGame.UserControls
{
    public class TemplateSelector:DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;
            var cell = item as Cell;
            if (cell !=null && element != null)
            {
                if (cell.Player is Player1)
                {
                    return element.FindResource("Toe") as DataTemplate;
                }
                if (cell.Player is Player2)
                {
                    return element.FindResource("Cross") as DataTemplate;
                }
                if (cell.Player is Player1Winner)
                {
                    return element.FindResource("ToeWinner") as DataTemplate;
                }
                if (cell.Player is Player2Winner)
                {
                    return element.FindResource("CrossWinner") as DataTemplate;
                }
                if (cell.Player is Player)
                {
                    return element.FindResource("Default") as DataTemplate;
                }

            }
            return base.SelectTemplate(item, container);
        }
    }
}
