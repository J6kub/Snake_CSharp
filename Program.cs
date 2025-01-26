// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Security.Principal;
using ConSnek;


class SnekGame
{
    public static Game game;
    public static int GameSize = 14;
    public static int GameWidth = (GameSize * 2);
    private static bool GamePlaying = true;
    
    public static void Main()
    {
        Console.WindowWidth = GameWidth;
        Console.WindowHeight = GameSize;
        Console.BufferWidth = GameWidth;
        Console.BufferHeight = GameSize;
        Console.CursorVisible = false;
        Console.Title = "Snek Game";
        StartScreen();
        //Console.WriteLine("Welcome to the Snek Game!");
        game = new Game(150);
        while (true)
        {
            while (GamePlaying)
            {
                game.GetInput();    
            }
        }
    }

    private static void StartScreen()
    {
        char[] WelcomeStringArray = "Welcome to snakey <3".ToCharArray();

        foreach (char c in WelcomeStringArray)
        {
            Console.Write(c);
            Thread.Sleep(35);
        }
        Console.WriteLine();
        char[] SecondWelcomeStringArray = "Press any key to start".ToCharArray();

        foreach (char c in SecondWelcomeStringArray)
        {
            Console.Write(c);
            Thread.Sleep(20);
        }
        
        Console.ReadKey();
        Console.Clear();
    }

    public static void GameOver()
    {
        game.timer.Close();
        GamePlaying = false;
        Thread.Sleep(50);
        Console.CursorTop = 0;
        Console.CursorLeft = 0;
        Console.Clear();
        Console.CursorTop = 0;
        Console.CursorLeft = 0;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Game Over!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Thread.Sleep(250);
        game = new Game(150);
        GamePlaying = true;
    }
}