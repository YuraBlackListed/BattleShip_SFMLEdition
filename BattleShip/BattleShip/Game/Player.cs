using BattleShip.Engine;
using BattleShip.Engine.Interfaces;
using BattleShip.Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Game
{
    public class Player : IUpdatable
    {
        public int score { get; private set; } = 0;
        public int roundWins { get; private set; } = 0;

        public bool IsAI { get; private set; } = false;

        public Cell[,] playerCells;
        private Cell[,] enemyCells;

        private bool hitLastTime = false;

        public bool hisTurn = false;

        public Player(Cell[,] enemycells, Cell[,] playercells, bool isAi)
        {
            playerCells = playercells;
            enemyCells = enemycells;
            IsAI = isAi;
            GameLoop.updatableObjects.Add(this);
        }
        (int, int) lastCoords;

        Random ramndom = new();

        public void CheckInput()
        {
            if (!IsAI && hisTurn)
            {
                lastCoords = ((int)ReadCoordinate('x'), (int)ReadCoordinate('y'));
            }
        }
        public void Update()
        {
            Shoot();
            if (!hitLastTime)
            {
                hisTurn = false;
            }
        }

        private int ReadCoordinate(char vertexName)
        {
            Console.SetCursorPosition(0, 15);
            Console.Write($"Type in {vertexName} coordinate:      ");
            Console.SetCursorPosition(21, 15);

            int value = 0;
            string strValue = Console.ReadLine();

            if (int.TryParse(strValue, out value) && value >= 0 && value <= 9)
            {
                Console.SetCursorPosition(0, 14);
                Console.Write("                             ");

                return value;
            }
            else
            {
                Console.SetCursorPosition(0, 14);
                Console.Write("Invalid coordinate, try again");

                value = ReadCoordinate(vertexName);
            }
            return value;
        }

        private void Shoot()
        {
            (int x, int y) = (0, 0);
            do
            {
                (x, y) = IsAI ? BotShoot() : PlayerShoot();
            } while (enemyCells[x, y].IsBombed);


            if (enemyCells[x, y].IsShip)
            {
                enemyCells[x, y].ShowShip();
                score++;
            }

            hitLastTime = enemyCells[x, y].IsShip;

            enemyCells[x, y].DestroyCell();

        }
        private (int, int) PlayerShoot()
        {
            CheckInput();
            return lastCoords;
        }
        private (int, int) BotShoot()
        {
            int x = ramndom.Next(0, enemyCells.Length / 10);
            int y = ramndom.Next(0, enemyCells.Length / 10);
            (int, int) randomCoordinates = (x, y);

            Thread.Sleep(1500);

            return randomCoordinates;
        }

        public void Won()
        {
            score = 0;
            roundWins++;
        }
    }
}
