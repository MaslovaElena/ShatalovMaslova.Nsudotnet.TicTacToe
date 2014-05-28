using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTakToeGame.Common;

namespace TicTakToeGame.ViewModels
{
    public class Player
    {
        public virtual string Name { get; set; }
    
        public int Score { get; set; }
    
    }
}
