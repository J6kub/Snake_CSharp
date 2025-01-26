namespace ConSnek;

public class Apple
{
    public int X { get; set; }
    public int Y { get; set; }
    
    
    public Apple(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Draw()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.CursorTop = Y;
        Console.CursorLeft = X;
        Console.Write("*");
    }
}