namespace ConSnek;

public class Snake
{
    public List<Block> Blocks { get; set; }
    private Block LastBlock { get; set; }
    private char Direction { get; set; }
    private bool _AddBlock { get; set; }
    public Snake(int x, int y)
    {
        Blocks = new List<Block> { new Block(x,y) };
        LastBlock = new Block(2, 3);
        Direction = 's';
    }

    public void UpdateDirection(char direction)
    {
        List<char> directions = new List<char> { 'w', 'a', 's', 'd' };
        if (directions.Contains(direction))
        {
            if (direction == 'w' && Direction == 's') return;
            if (direction == 's' && Direction == 'w') return;
            if (direction == 'a' && Direction == 'd') return;
            if (direction == 'd' && Direction == 'a') return;
            
            Direction = direction;
        }
        if (direction == 'r') AddBlock(); 
    }
    public void UpdatePos()
    {
        // Set new head position based on direction
        Block Head = Blocks[0];
        
        if (Direction == 'w') Blocks[0].Y--;
        else if (Direction == 's') Blocks[0].Y++;
        else if (Direction == 'd') Blocks[0].X++;
        else if (Direction == 'a') Blocks[0].X--;
        
        // Check out of bounds
        if (Head.X < 1 || Head.X >= SnekGame.GameWidth - 1 || Head.Y < 1 || Head.Y >= SnekGame.GameSize - 1)
        {
            SnekGame.GameOver();
        }
        CheckSelfCollision();
        
        // Update each block to position to next
        for (int i = Blocks.Count - 1; i > 0; i--)
        {
            Blocks[i].CopyOf(Blocks[i - 1]);      
        }

        if (_AddBlock)
        {
            Blocks.Add(new Block(LastBlock.X, LastBlock.Y));
            _AddBlock = false;
        }
        LastBlock.CopyOf(Blocks[Blocks.Count - 1]);
    }

    public void AddBlock()
    {
        _AddBlock = true;
    }
    public char GetDirection() => Direction;

    private void CheckSelfCollision()
    {
        if (Blocks.Count > 2)
        {
            for (int i = 2; i < Blocks.Count; i++)
            {
                if (Blocks[0].X == Blocks[i].X && Blocks[0].Y == Blocks[i].Y)
                {
                    SnekGame.GameOver();
                }
            }
        }
        
    }
    public void Draw(bool debug = false, bool clean = false)
    {
        if (Blocks.Count > 0 && !clean)
        { 
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < Blocks.Count; i++)
            {
                //Thread.Sleep(5);
                Console.CursorLeft = Blocks[i].X;
                Console.CursorTop = Blocks[i].Y;
                Console.Write(" ");
                
                Console.CursorTop = 0;
                Console.CursorLeft = 0;
            }

            if (debug)
            {
                for (int i = 0; i < Blocks.Count; i++)
                {
                    Console.CursorLeft = 10;
                    Console.CursorTop = i;
                    Console.Write($"B{i} x{Blocks[i].X} y{Blocks[i].Y}");
                }
            }
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            
            Console.CursorLeft = LastBlock.X;
            Console.CursorTop = LastBlock.Y;
            Console.Write(" ");
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }
    }
}

public class Block
{
    public int X { get; set; }
    public int Y { get; set; }

    public Block(int x, int y)
    {
        X = x;
        Y = y;
    }
    public void CopyOf(Block block)
    {
        X = block.X;
        Y = block.Y;
    }
}