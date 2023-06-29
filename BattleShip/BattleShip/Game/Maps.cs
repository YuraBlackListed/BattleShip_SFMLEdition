using BattleShip.Engine;
using BattleShip.Engine.ExtensionMethods.FontExtentionMethods;
using BattleShip.Engine.Interfaces;
using BattleShip.Game.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace BattleShip.Game
{
    public class Maps : GameObject ,IDrawable
    {
        private int gridSize = 10;
        private int cellSize = 30;
        private int shipsCount = 7;
        private int textOffset = 20;
        private uint textSize = 20;
        private int mapOffset = 100;

        public Cell[,] player1Cells;
        public Cell[,] player2Cells;
        private Random random = new Random();
        private RenderWindow scene;

        private Text[,] player1Cellstext;
        private Text[,] player2Cellstext;
        private Font font;

        public Maps(int _shipsCount, RenderWindow _scene) : base(_scene)
        {
            shipsCount = _shipsCount;
            scene = _scene;

            player1Cells = new Cell[gridSize, gridSize];
            player2Cells = new Cell[gridSize, gridSize];

            player1Cellstext = new Text[gridSize, gridSize];
            player2Cellstext = new Text[gridSize, gridSize];
            font = font.LoadFromFile("arial.ttf");

            

            GameLoop.drawableObjects.Add(this);
        }
        public void Start()
        {
            GenerateField(0, 0, player1Cells);
            GenerateText(player1Cellstext, player1Cells);
            GenerateShips(player1Cells);

            GenerateField(500, 0, player2Cells);
            GenerateText(player2Cellstext, player2Cells);
            GenerateShips(player2Cells);
            RevielShips(player1Cells);


        }


        private void GenerateField(int _x, int _y, Cell[,] cells)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    cells[x, y] = new Cell(new Vector2f(x * cellSize + _x + mapOffset, y * cellSize + _y + mapOffset), new Vector2f(cellSize, cellSize), scene);
                }
            }
        }
        public void GenerateShips(Cell[,] cells)
        {
            for (int i = 0; i < shipsCount; i++)
            {
                int x = random.Next(0, gridSize);
                int y = random.Next(0, gridSize);
                cells[x, y].CreateShip();
            }
        }
        private void GenerateText(Text[,] _gridText, Cell[,] cells)
        {
            for (int i = 0; i < gridSize; i++)
            {
                string text = i.ToString();
                _gridText[i, 0] = new Text(text, font, textSize)
                {
                    FillColor = Color.Black,
                    Position = new Vector2f(i * cellSize + cells[0, 0].Position.X, cells[0, 0].Position.Y - textOffset - 10)
                    
                };
                text = i.ToString();
                _gridText[0, i] = new Text(text, font, textSize)
                {
                    FillColor = Color.Black,
                    Position = new Vector2f(cells[0, 0].Position.X - textOffset - 10, i * cellSize + cells[0, 0].Position.Y)
                };

                _gridText[0, 0].Position = new Vector2f(cells[0, 0].Position.X - textOffset - 10, cells[0, 0].Position.Y - textOffset - 10);
            }
        }
        public void Draw()
        {
            for (int i = 0; i < gridSize; i++)
            {
                scene.Draw(player1Cellstext[i, 0]);
                scene.Draw(player1Cellstext[0, i]);
                scene.Draw(player2Cellstext[i, 0]);
                scene.Draw(player2Cellstext[0, i]);
            }
        }
        public void RevielShips(Cell[,] cells)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (cells[i, j] != null)
                    {
                        cells[i, j].ShowShip();
                    }
                }
            }
        }
        public void HideShips(Cell[,] cells)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (cells[i, j] != null)
                    {
                        cells[i, j].HideShip();
                    }
                }
            }
        }
    }
}