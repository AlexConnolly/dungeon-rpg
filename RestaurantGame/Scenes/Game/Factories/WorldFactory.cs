﻿using LDG;
using LDG.Components.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantGame.Scenes.Game.Factories
{
    internal class WorldFactory
    {
        public GameObject CreateWorld(Scene scene)
        {
            var worldObject = scene.AddGameObject();

            var tilemap = worldObject.AddComponent<Tilemap>();

            tilemap.Layers = new List<TilemapLayer>()
            {
                new TilemapLayer()
                {
                    Tiles = new List<TilemapItem>()
                    {

                    }
                },
                new TilemapLayer()
                {
                    Tiles = new List<TilemapItem>()
                    {

                    }
                }
            };

            return worldObject;
        }
    }
}