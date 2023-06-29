using BattleShip.Engine.ExtensionMethods.FontExtentionMethods;
using BattleShip.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace BattleShip.Game
{
    public class Round : IDrawable
    {
        public Maps maps;
        public RenderWindow scene;

        public Player player1;
        public Player player2;

        private Text player1ScoreText;
        private Text player2ScoreText;
        private Text TurnText;
        private Font font;

        private int shipsCount;

        private uint textSize = 20;

        public Round(int _shipsCount, RenderWindow _scene)
        {
            scene = _scene;
            shipsCount = _shipsCount;
            
            font = font.LoadFromFile("arial.ttf");

            player1ScoreText = new Text("Player 1: 0", font, textSize);
            player1ScoreText.Position = new Vector2f(20, 500);

            player1ScoreText = new Text("Player 2: 0", font, textSize);
            player1ScoreText.Position = new Vector2f(20, 530);
            
            TurnText = new Text("Current turn: Player1", font, textSize);
            TurnText.Position = new Vector2f(400, 515);

            maps = new Maps(shipsCount, _scene);
            maps.Start();
        }

        public void RecreateMaps()
        {
            maps.Start();
            
        }

        public void Turn()
        {
            Player currentPlayer = CalculateCurrentPlayer();
            currentPlayer.Update();
            currentPlayer.hisTurn = false;

            if (player1.hisTurn && !player1.IsAI && player2.IsAI)
            {
                maps.HideShips(player2.playerCells);
                maps.RevielShips(player1.playerCells);
            }
            else if (player2.hisTurn && !player1.IsAI && player2.IsAI)
            {
                //nothing
            }
            else if (!player1.IsAI && !player2.IsAI)
            {
                if (player1.hisTurn)
                {
                    maps.HideShips(player2.playerCells);
                    maps.RevielShips(player1.playerCells);
                }
                else
                {
                    maps.HideShips(player1.playerCells);
                    maps.RevielShips(player2.playerCells);
                }
            }
            else
            {
                maps.RevielShips(player1.playerCells);
                maps.RevielShips(player2.playerCells);
            }
        }

        private Player CalculateCurrentPlayer()
        {
            if (player1.hisTurn)
            {
                player2.hisTurn = true;
                return player1;
            }
            else if (player2.hisTurn)
            {
                player1.hisTurn = true;
                return player2;
            }
            return player2;
        }

        public void WritePlayerTurn(bool player1turn)
        {
            TurnText.DisplayedString = "Current turn:";
            if (player1turn)
            {
                TurnText.DisplayedString += "Player 1";
            }
            else
            {
                TurnText.DisplayedString += "Player 2";
            }
        }

        public void WritePlayerScore()
        {
            player1ScoreText.DisplayedString = $"Player 1 score: {player1.score}";

            player1ScoreText.DisplayedString = $"Player 2 score: {player2.score}";

        }
        public bool SomebodyWon()
        {
            if (player1.score >= shipsCount)
            {
                player1.Won();
                return true;
            }
            else if (player2.score >= shipsCount)
            {
                player2.Won();
                return true;
            }
            return false;
        }
        public void Draw()
        {
            scene.Draw(player1ScoreText);
            scene.Draw(player2ScoreText);
        }
    }
}
