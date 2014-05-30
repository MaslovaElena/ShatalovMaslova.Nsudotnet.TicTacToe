using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTakToeGame.Common
{
    public class UserActionEventArgs:EventArgs
    {

        public UserActionEventArgs(int index)
        {
            this.CellIndex = index;
        }

        public int CellIndex { get; set; }
    }
}
