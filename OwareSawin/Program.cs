using System;
namespace OwareNsvisEdition;

class Program
{
    static void Main()
    {
        GameLoop game = new GameLoop();
        game.Looper();
    }
}