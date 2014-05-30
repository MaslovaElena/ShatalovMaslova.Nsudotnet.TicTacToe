using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTakToeGame.Common;

namespace TicTakToeGame.ViewModels
{
    public class GameField:ViewModelBase
    {
      
        private readonly List<Player> winners = new List<Player>(); 

        
        public ObservableCollection<Cell> Cells { get; set; }


        public Player CurrentPlayer { get; set; }

        public Boolean IsFilled
        {
            get { return this.Cells.All(c => c.Player is Player1 || c.Player is Player2 || c.Player is Player1Winner || c.Player is Player2Winner); }
        }


        public IList<Player> Winners
        {
            get { return this.winners; }
        }


        public bool ActionAllowed { get; set; }

        public int Index { get; set; }

        public bool GameOver { get; set; }


        public event EventHandler<UserActionEventArgs> UserMadeAcion;

    
        private void OnMadeAction(int cellIndex)
        {
            if (this.UserMadeAcion != null)
            {
                
                this.UserMadeAcion(this, new UserActionEventArgs(cellIndex));
            }
        }

        public GameField()
        {
            this.ActionAllowed = true;
            this.GameOver = false;
            this.UserStepAction = new ActionCommand(p => (this.ActionAllowed && !this.GameOver), this.UserStepActionExecute);
            this.Cells = new ObservableCollection<Cell>();
            var defaultPlayer = new Player();
            for (int i = 0; i < 9; i++)
            {
                this.Cells.Add(new Cell() {Player = defaultPlayer, Index = i, UserStepAction = this.UserStepAction});
            }

        }


        public ActionCommand UserStepAction { get; set; }

    
        private void UserStepActionExecute(object parameter)
        {
            if (parameter != null && parameter is Cell)
            {
            
                var index = (parameter as Cell).Index;
             
                this.Cells[index] = new Cell() {Index = index, Player = this.CurrentPlayer};

                if (this.CheckIfIsWinner(this.CurrentPlayer) && !this.winners.Contains(this.CurrentPlayer))
                {
               
                    this.winners.Add(this.CurrentPlayer);    
                }

                this.OnMadeAction(index);
            }

        }

    
        private bool CheckIfIsWinner(Player player)
        {
            // Checked
            // 0 1 2
            // 3 4 5
            // 6 7 8 
            var result = this.CheckInNumbers(player, 0, 4, 8) || this.CheckInNumbers(player, 2, 4, 6) ||
                         this.CheckInNumbers(player, 0, 1, 2) ||
                         this.CheckInNumbers(player, 3, 4, 5) || this.CheckInNumbers(player, 6, 7, 8) ||
                         this.CheckInNumbers(player, 0, 3, 6) ||
                         this.CheckInNumbers(player, 1, 4, 7) || this.CheckInNumbers(player, 2, 5, 8);

            return result;
        }

        private bool CheckInNumbers(Player player, params int[] nums)
        {
           
            var result = nums.ToList().All(n=>this.Cells[n].Player == player);

            if (result)
            {
              
                nums.ToList().ForEach(n=>this.Cells[n] = new Cell(){ Player = player is Player1 ? new Player1Winner() : (Player) new Player2Winner()});
            }
            return result;
        }
    }
}
