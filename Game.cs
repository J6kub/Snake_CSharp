using System.Timers;

namespace ConSnek;

public class Game
{
    private int Score { get; set; }
    public Snake _Snake { get; set; }
    public Apple _Apple { get; set; }
    public System.Timers.Timer timer { get; set; }
    private int TimerCount { get; set; }
    
    
    public Game(int Speed)
    {
        DrawFrame();
        Thread.Sleep(550);
        
        Score = 0;
        _Snake = new Snake(2,2);
        _Snake.AddBlock();
        TimerCount = 4;
        timer = new System.Timers.Timer(Speed);
        timer.Elapsed += GameFrame;
        timer.Enabled = true;
        timer.AutoReset = true;

        SpawnApple();
    }
    public int getScore() => Score;
    public void addScore() => Score++;

    private void GameFrame(object source, ElapsedEventArgs e)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        
        TimerCount++;
        //if (TimerCount % 5 == 0) DrawFrame();
        Console.CursorTop = 0;
        Console.CursorLeft = 0;
        
        _Snake.Draw(clean:true);
        _Snake.UpdatePos();
        _Snake.Draw();
        _Apple.Draw();

        if (CheckBlockCollision(_Apple, _Snake.Blocks[0]))
        {
            SpawnApple();
            _Snake.AddBlock();
            addScore();
            
            DrawScore();
        }
        
        Console.CursorLeft = 0;
        Console.CursorTop = 0;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.BackgroundColor = ConsoleColor.Green;
    }

    public void GetInput()
    {
        char input = Console.ReadKey().KeyChar;
        _Snake.UpdateDirection(input);
        Console.WriteLine(_Snake.GetDirection());
    }

    private void DrawScore()
    {
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.CursorTop = 0;
        Console.CursorLeft = 3;
        Console.Write($"Score: {Score}");
    }
    private void DrawFrame()
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Green;

        for (int i = 0; i < SnekGame.GameSize; i++)
        {
            Console.CursorLeft = i*2;
            Console.CursorTop = SnekGame.GameSize - 1;
            Console.Write("  ");
            Console.CursorLeft = i*2;
            Console.CursorTop = 0;
            Console.Write("  ");
            
            Thread.Sleep(10);
            
            Console.CursorLeft = 0;
            Console.CursorTop = i;
            Console.Write(" ");
            Console.CursorLeft = SnekGame.GameWidth - 1;
            Console.CursorTop = i;
            Console.Write(" ");
        }
    }
    
    private bool CheckBlockCollision(Apple apple, Block block)
    {
        if (apple.X == block.X && apple.Y == block.Y)
        {
            return true;
        }
        return false;
    }
    private bool CheckCollision(Apple apple, Snake snake)
    {
        foreach (var block in snake.Blocks)
        {
            if (CheckBlockCollision(apple, block))
            {
                return true;
            }
        }

        return false;
    }

    private void SpawnApple()
    {
        Random rnd = new Random();
        
        Apple apple = new Apple(rnd.Next(1, SnekGame.GameWidth - 1), rnd.Next(1, SnekGame.GameSize - 1));
        while (CheckCollision(apple, _Snake))
        {
            apple = new Apple(rnd.Next(1, SnekGame.GameWidth - 1), rnd.Next(1, SnekGame.GameSize - 1));
        }
        _Apple = apple;
            
    }
    
}