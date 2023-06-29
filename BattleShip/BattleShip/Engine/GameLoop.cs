global using Time = BattleShip.Engine.Time;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using BattleShip.Engine.Interfaces;
using BattleShip.Engine.ExtensionMethods.PathExtentionMethods;
using BattleShip.Engine.Input;
using System.Collections.Generic;
using System.IO;
using BattleShip.Game;

namespace BattleShip.Engine
{
    class GameLoop
    {
        private int foodVolume;
        private int shipAmount;

        private RenderWindow scene;

        public static List<IUpdatable> updatableObjects= new();
        public static List<IDrawable> drawableObjects = new();

        public Vector2u mapSize;

        private Game.BattleShip game;

        private InputHandler input;
        public AudioSystem audioSystem;

        private GameLoop()
        {
            foodVolume = 200;
            shipAmount = 6;

            mapSize = new Vector2u(800, 800);
        }

        public void Run()
        {
            Start();
            while (scene.IsOpen)
            {
                Render();
                CheckInput();
                Update();
            }
        }
        private void Start()
        {
            audioSystem = new();
            scene = new RenderWindow(new VideoMode(mapSize.X, mapSize.Y), "Game window");
            scene.Closed += (sender, e) => scene.Close();

            input = new InputHandler(scene);

            game = new Game.BattleShip(shipAmount, scene);

            scene.DispatchEvents();

            Time.Start();
        }
        private void Update()
        {
            Time.Update();

            foreach (IUpdatable updatable in updatableObjects)
            {
                updatable.Update();
            }

            //game.Update();

            scene.DispatchEvents();
        }
        private void Render()
        {
            scene.Clear(Color.White);

            foreach (IDrawable drawable in drawableObjects)
            {
                drawable.Draw();
            }

            scene.Display();
        }
        private void CheckInput()
        {
            input.CheckInput();
        }

        public static GameLoop NewGameLoop()
        {
            GameLoop gameloop = new GameLoop();

            gameloop.LoadInformationFromFile();

            return gameloop;
        }
        public void LoadInformationFromFile()
        {
            string currentDirectory = PathExtentionMethods.GetCurrentDirectory();
            string filePath = currentDirectory + "/Engine/" + @"congifg.cfg";

            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    StreamReader reader = new StreamReader(fs);

                    string data;
                    for (data = "1"; data != null; data = reader.ReadLine())
                    {
                        string[] dataSplit = data.Split(':');

                        switch (dataSplit[0])
                        {
                            case "mapSize":
                                if (uint.TryParse(dataSplit[1], out uint x))
                                    mapSize.X = x;
                                if (uint.TryParse(dataSplit[2], out uint y))
                                    mapSize.Y = y;
                                break;
                            case "shipAmount":
                                if (int.TryParse(dataSplit[1], out int newPlayerAmount))
                                    shipAmount = newPlayerAmount;
                                break;
                        }
                    }
                }
            }
        }
    }
}
