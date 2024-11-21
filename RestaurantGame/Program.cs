using LDG;
using RestaurantGame;
using RestaurantGame.Scenes.Game;

Console.WriteLine("Hello, world!");

var gameClient = new LDGGame(new GameScene());

gameClient.Run();