using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTakToeGame.Common;

namespace TicTakToeGame.ViewModels
{
    public class Cell:ViewModelBase
    {
        private Player player;

        public Player Player
        {
            get { return this.player; }
            set
            {
                this.player = value;
                //this.OnPropertyChanged("Player");
                this.NotifyOnPropertyChange(() => Player);
            }
        }

        public int Index { get; set; }

        public ActionCommand UserStepAction { get; set; }

    }
}
