using BattleShip.Engine;
using BattleShip.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Game.GameObjects
{
    public class Cell : GameObject, IUpdatable,IDrawable
    {
        public bool IsShip { get; set; } = false;
        public bool IsAttacked { get; private set; } = false;
        public bool IsBombed { get; private set; } = false;
        public bool OnPlayerSide { get; private set; } = false;
        public RectangleShape Shape { get; private set; }
        private Color shipColor = Color.Black;
        private Color defaultColor = Color.Blue;
        private Color destroyedColor = Color.Cyan;
        private Color destroyedShipColor = Color.Red;
        public Cell(Vector2f position, Vector2f size, RenderWindow scene) : base(scene)
        {
            Shape = new RectangleShape(size)
            {
                Position = position,
                FillColor = defaultColor,
            };
            Mesh = Shape;
            Position = position;

            GameLoop.updatableObjects.Add(this);
        }
        public void Update()
        {
            Mesh = Shape;
        }
        public void Draw()
        {
            scene.Draw((Shape)Mesh);
        }
        public void CreateShip()
        {
            IsShip = true;

            Shape.FillColor = shipColor;
        }

        public void DestroyCell()
        {
            if (IsShip)
            {
                Shape.FillColor = destroyedShipColor;

                IsShip = false;
            }
            else Shape.FillColor = destroyedColor;

            IsBombed = true;
        }
        public void ShowShip()
        {
            if (IsShip)
            {
                Shape.FillColor = shipColor;
            }
        }
        public void HideShip()
        {
            if (Shape.FillColor != destroyedColor && Shape.FillColor != destroyedShipColor)
            {
                Shape.FillColor = defaultColor;
            }

        }
    }
}
