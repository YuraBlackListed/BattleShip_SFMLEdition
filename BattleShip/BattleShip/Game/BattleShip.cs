using SFML.Graphics;

namespace BattleShip.Game
{
    public class BattleShip
    {
        public Player player1 { get; private set; }
        public Player player2 { get; private set; }
        public Round round { get; private set; }
        private RenderWindow scene;

        private int roundCount = 1;

        public bool Finished { get; private set; } = false;

        private int gameModeIndex;

        public int winnerIndex { get; private set; }

        int shipsCount = 7;

        public BattleShip(int _shipsCount, RenderWindow _scene)
        {
            scene = _scene;
            shipsCount = _shipsCount;

            gameModeIndex = ReceiveGameMode();
            StartNewRound();
        }

        public void Update()
        {
            if (roundCount <= 3)
            {
                if (round.SomebodyWon())
                {
                    StartNewRound();
                }
                else
                {
                    round.Turn();
                }
            }
            else
            {
                winnerIndex = CalculateWinner();
                Finished = true;
            }
        }
        private void LoadMenu()
        {
            Console.WriteLine("Type 1 to play PvP");
            Console.WriteLine("Type 2 to play PvAI");
            Console.WriteLine("Type 3 to play AIvAI");
        }
        private int ReceiveGameMode()
        {
            LoadMenu();
            int value;
            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out value) && value >= 1 && value <= 3)
                {
                    Console.SetCursorPosition(0, 14);
                    Console.Write("                             ");
                    Console.SetCursorPosition(0, 3);
                    break;
                }
                else
                {
                    Console.SetCursorPosition(0, 14);
                    Console.Write("Invalid index, try again");
                    Console.SetCursorPosition(0, 3);
                }
            }

            return value;
        }
        private void SetGameMode(int value)
        {
            (bool firstAI, bool secondAI) = value switch
            {
                1 => (false, false),
                2 => (false, true),
                3 => (true, true),
            };

            player1 = new Player(round.maps.player2Cells, round.maps.player1Cells, firstAI);
            player2 = new Player(round.maps.player1Cells, round.maps.player2Cells, secondAI);

            player1.hisTurn = true;
        }
        private int CalculateWinner()
        {
            if (player1.roundWins >= 3)
            {
                return 1;
            }
            else if (player2.roundWins >= 3)
            {
                return 2;
            }

            return 1;
        }
        private void StartNewRound()
        {
            roundCount = 0;
            round = new Round(shipsCount, scene);
            SetGameMode(gameModeIndex);
            round.player1 = player1;
            round.player2 = player2;
        }
        
    }
}
