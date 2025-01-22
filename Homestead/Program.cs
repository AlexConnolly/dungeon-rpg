using Homestead;
using LDG;

Console.WriteLine("Loading...");

var game = new LDGGame(startScene: new Game());

game.Run();