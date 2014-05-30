using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TicTakToeGame.ViewModels;

namespace TicTakToeGame.Common
{
    public  interface IIndexable
    {
        int CurrentIndex { get; set; }
    }
}
