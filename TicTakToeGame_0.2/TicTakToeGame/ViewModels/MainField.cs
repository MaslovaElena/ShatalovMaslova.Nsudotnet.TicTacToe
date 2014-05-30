using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTakToeGame.Common;
using MessageBox = System.Windows.MessageBox;

namespace TicTakToeGame.ViewModels
{
    public class MainField : ViewModelBase
    {
     
        private Player1 player1 = new Player1() {Name = "Игрок 1"};

   
        private Player2 player2 = new Player2() {Name = "Игрок 2"};

    
        private Player currentPlayer = null;

        private bool gameOver = false;

        public ObservableCollection<GameField> GameFields { get; set; }


        public bool GameOver
        {
            get
            {
                return this.gameOver;
            }
            private set
            {
                this.gameOver = true;
                this.GameFields.ToList().ForEach(gf=>gf.GameOver = true);
                //this.OnPropertyChanged("GameOver");
                this.NotifyOnPropertyChange(() => GameOver);
            }
        }

        public Player CurrentPlayer
        {
            get { return this.currentPlayer; }
            set
            {
                this.currentPlayer = value;
                //this.OnPropertyChanged("CurrentPlayer");
                this.NotifyOnPropertyChange(() => CurrentPlayer);
            }
        }

        public MainField()
        {

            this.GameFields = new ObservableCollection<GameField>();

            this.StartGameCommand = new ActionCommand(this.StartGameCanExecute, this.StartGameExecute);
        }

        private bool CheckIfIsGameWinner(Player player)
        {
 
            var result = this.CheckInNumbers(player, 0, 4, 8) || this.CheckInNumbers(player, 2, 4, 6) ||
                         this.CheckInNumbers(player, 0, 1, 2) ||
                         this.CheckInNumbers(player, 3, 4, 5) || this.CheckInNumbers(player, 6, 7, 8) ||
                         this.CheckInNumbers(player, 0, 3, 6) ||
                         this.CheckInNumbers(player, 1, 4, 7) || this.CheckInNumbers(player, 2, 5, 8);

            return result;
        }

        private bool CheckInNumbers(Player player, params int[] nums)
        {
            var result = nums.ToList().All(n => this.GameFields[n].Winners.Contains(player));

            return result;
        }

        #region StartGameCommmand

        // Комманда старта игры
        public ActionCommand StartGameCommand { get; set; }

        // Инициализация начала игры
        private void StartGameExecute(object parameter)
        {
            this.GameFields.Clear();

            // Устанавливаем игрока который будет ходить первым
            this.CurrentPlayer = this.player1;

            for (int i = 0; i < 9; i++)
            {
                var gameField = new GameField() { Index = i };

                //Добавляем событие, что пользователь сделал шаг
                gameField.UserMadeAcion += (sender, args) =>
                {
                    //Проверяем, если все ячейки в маленьком поле заполнены
                    if (!this.GameFields[args.CellIndex].IsFilled)
                    {
                        //Закрываем все поля, и открываем то, которе соответствует индексу маленькой ячейки
                        this.GameFields.ToList().ForEach(gf => gf.ActionAllowed = false);
                        this.GameFields[args.CellIndex].ActionAllowed = true;
                    }
                    else
                    {
                        //Если сходить больше некуда, открываем все
                        this.GameFields.ToList().ForEach(gf => gf.ActionAllowed = true);
                    }

                    if (this.CheckIfIsGameWinner(this.CurrentPlayer))
                    {
                        MessageBox.Show(string.Format("Игрок: {0} - выиграл!", this.CurrentPlayer.Name));
                        this.GameOver = true;
                    }
                    // Меняем пользователя
                    this.CurrentPlayer = this.CurrentPlayer is Player1 ? this.player2 : (Player)this.player1;
                    // Устанавливаем текущего пользователя во все игровые поля
                    this.GameFields.ToList().ForEach(gf => gf.CurrentPlayer = this.CurrentPlayer);
                };

                gameField.CurrentPlayer = this.CurrentPlayer;
                this.GameFields.Add(gameField);
            }
        }

        private bool StartGameCanExecute(object parameter)
        {
            return true;
        }

        #endregion
    }
}
