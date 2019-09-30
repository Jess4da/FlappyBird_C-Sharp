using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlappyBird
{
    class Program
    {
        // Initialize Screen Resolution
        const int ScreenW = 100, ScreenH = 40;
        static bool Active = true;

        // Initialize Random
        static Random Rd = new Random();

        struct Bird
        {
            private int _x, _y, _h, _w, _lives, _score;

            public int X
            {
                get { return _x; }
                set
                {
                    if (value < 0)
                    {
                        _x = 0;
                    }
                    else if (value + _w > ScreenW)
                    {
                        _x = ScreenW + _w - 1;
                    }
                    else
                    {
                        _x = value;
                    }
                }
            }
            public int Y
            {
                get { return _y; }
                set
                {
                    if (value < 0)
                    {
                        _y = 0;
                    }
                    else if (value + _h > ScreenH - 1)
                    {
                        _y = ScreenH - _h;
                    }
                    else
                    {
                        _y = value;
                    }
                }
            }
            public int Lives
            {
                get { return _lives; }
                set
                {
                    if (value < 0)
                    {
                        _lives = 0;
                    }
                    else if (value > 1)
                    {
                        _lives = 1;
                    }
                    else
                    {
                        _lives = value;
                    }
                }
            }
            public int H
            {
                get { return _h; }
                set { _h = value; }
            }
            public int W
            {
                get { return _w; }
                set { _w = value; }
            }
            public int Score
            {
                get { return _score; }
                set
                {
                    if (value < 0)
                    {
                        _score = 0;
                    }
                    else
                    {
                        _score = value;
                    }
                }
            }
            public void Draw()
            {
                Console.SetCursorPosition(_x, _y);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" *** ");
                Console.SetCursorPosition(_x, _y + 1);
                Console.Write("=v=");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("o");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(">");
                Console.SetCursorPosition(_x, _y + 2);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" === ");
            }
            public void Move()
            {

                if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    Y -= 4;
                }
            }
        }
        struct Pipe
        {
            private int _x, _h;
            public int W, WP;
            public int X
            {
                get { return _x; }
                set
                {
                    if (value <= 3)
                    {
                        _x = 3;
                    }
                    else
                    {
                        _x = value;
                    }
                }
            }
            public int H
            {
                get { return _h; }
                set { _h = value; }
            }
            public void Draw()
            {
                int HT = H - WP;
                int HB = H + WP;
                if (X > 3)
                {
                    /// Top =================================================
                    for (int i = 0; i < HT - 1; i++)
                    {
                        for (int j = 0; j < W; j++)
                        {
                            Console.SetCursorPosition(X + j, i);
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(' ');
                        }
                    }
                    for (int j = -2; j < W + 2; j++)
                    {
                        Console.SetCursorPosition(X + j, HT - 1);
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(' ');
                    }
                    for (int j = -3; j < W + 3; j++)
                    {
                        Console.SetCursorPosition(X + j, HT);
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(' ');
                    }

                    /// Buttom ==============================================

                    for (int i = -3; i < W + 3; i++)
                    {
                        Console.SetCursorPosition(X + i, HB);
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(' ');
                    }
                    for (int i = -2; i < W + 2; i++)
                    {
                        Console.SetCursorPosition(X + i, HB + 1);
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(' ');
                    }
                    for (int i = HB + 2; i < ScreenH; i++)
                    {
                        for (int j = 0; j < W; j++)
                        {
                            Console.SetCursorPosition(X + j, i);
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(' ');
                        }
                    }
                    Console.ResetColor();
                }
            }
        }
        static int RunGame()
        {

            int count = 0;
            List<Pipe> PipeList = new List<Pipe>();



            Bird aBird = new Bird();
            aBird.X = (ScreenW / 2) - 20;
            aBird.Y = (ScreenH / 2) - 2;
            aBird.H = 3;
            aBird.W = 5;
            aBird.Lives = 1;
            aBird.Score = 0;

            bool Running = true;



            LogoDraw();
            Console.SetCursorPosition((ScreenW / 2) - 15, ScreenH / 2);
            Console.WriteLine("===Press Space bar to start===");
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                Console.SetCursorPosition(0, ScreenH / 2 + 10);
                Console.Write("====================================================================================================");
                Console.SetCursorPosition(0, ScreenH / 2 + 12);
                Console.Write("====================================================================================================");
                Console.SetCursorPosition(0, ScreenH / 2 + 11);
                for (int i = 0; i <= 100; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("#");
                    Thread.Sleep(10);
                }
                Console.Clear();


                // Game loop
                while (Running)
                {
                    if (count % 40 == 0)
                    {
                        //Update Pipe
                        Pipe aPipe = new Pipe();
                        aPipe.X = ScreenW - 8;
                        aPipe.H = Rd.Next(9, ScreenH - 10);
                        aPipe.W = 6;
                        aPipe.WP = Rd.Next(6, 8);
                        PipeList.Add(aPipe);
                    }
                    //Move Pipe
                    List<Pipe> NewPipeList = new List<Pipe>();
                    for (int i = 0; i < PipeList.Count; i++)
                    {
                        Pipe OldPipe = PipeList[i];

                        if (OldPipe.X > 3)
                        {
                            Pipe MovedPipe = new Pipe();
                            MovedPipe.X = OldPipe.X - 1;
                            MovedPipe.H = OldPipe.H;
                            MovedPipe.W = OldPipe.W;
                            MovedPipe.WP = OldPipe.WP;

                            NewPipeList.Add(MovedPipe);
                        }
                    }
                    PipeList = NewPipeList;
                    //collide check
                    for (int i = 0; i < PipeList.Count; i++)
                    {
                        if ((PipeList[i].X - 3 <= aBird.X + aBird.W && aBird.Y <= PipeList[i].H - PipeList[i].WP + 1 && PipeList[i].X + 3 >= aBird.X)
                            || (PipeList[i].X - 3 <= aBird.X + aBird.W && aBird.Y + aBird.H >= PipeList[i].H + PipeList[i].WP && PipeList[i].X + 3 >= aBird.X))
                        {
                            if (aBird.Lives > 0)
                            {
                                aBird.Lives--;
                            }
                            if (aBird.Lives == 0)
                            {
                                Running = false;
                                break;
                            }
                        }

                        //update score
                        if (PipeList[i].X < aBird.X + aBird.W && PipeList[i].X + 1 >= aBird.X + aBird.W)
                        {
                            aBird.Score++;
                        }
                    }

                    //Move Bird
                    if (Console.KeyAvailable)
                    {
                        aBird.Move();
                    }


                    aBird.Y++;

                    // Clear
                    Console.Clear();

                    //Draw Pipe
                    for (int i = 0; i < PipeList.Count; i++)
                    {
                        Pipe ThisPipe = PipeList[i];
                        ThisPipe.Draw();
                    }

                    //Draw Bird
                    aBird.Draw();

                    //Show Score
                    Console.SetCursorPosition(1, 1);
                    Console.WriteLine("Score : {0}", aBird.Score);



                    //Count 
                    count++;

                    //SetCurSor to 0,0
                    Console.SetCursorPosition(0, 0);

                    Thread.Sleep(50);
                }
            }
            return aBird.Score;
        }
        static void Main(string[] args)
        {
            // Initialize Scene
            Console.BufferWidth = Console.WindowWidth = ScreenW;
            Console.BufferHeight = Console.WindowHeight = ScreenH;

            while (Active)
            {
                int Score = RunGame();

                while (true)
                {
                    Console.ResetColor();
                    Console.SetCursorPosition((ScreenW / 2) - 8, (ScreenH / 2) - 4);
                    Console.WriteLine("===Game Over===");
                    Console.SetCursorPosition((ScreenW / 2) - 11, (ScreenH / 2) - 2);
                    Console.WriteLine("===Your point is {0}===", Score);
                    Console.SetCursorPosition((ScreenW / 2) - 14, (ScreenH / 2));
                    Console.WriteLine("===Do you want to restart===");
                    Console.SetCursorPosition((ScreenW / 2) - 8, (ScreenH / 2) + 2);
                    Console.WriteLine("===[Y] / [N]===");

                    if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        Active = false;
                        break;
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        break;
                    }
                    Console.ResetColor();
                }
                Console.Clear();
                Console.ResetColor();
            }
            Console.Clear();
            Console.ResetColor();
        }
        static void LogoDraw()
        {
            Console.SetCursorPosition((ScreenW / 2) - 20, 2);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("      ");
            Console.SetCursorPosition((ScreenW / 2) - 20, 3);
            Console.Write("  ");
            Console.SetCursorPosition((ScreenW / 2) - 16, 3);
            Console.Write("  ");
            Console.SetCursorPosition((ScreenW / 2) - 20, 4);
            Console.Write("  ");
            Console.SetCursorPosition((ScreenW / 2) - 18, 4);
            Console.Write("    ");
            for (int i = 5; i <= 10; i++)
            {
                Console.SetCursorPosition((ScreenW / 2) - 16, i);
                Console.Write("  ");
            }
            Console.SetCursorPosition((ScreenW / 2) - 16, 11);
            Console.Write("          ");

            for (int i = 2; i <= 10; i++)
            {
                Console.SetCursorPosition((ScreenW / 2) - 8, i);
                Console.Write("  ");
            }

            Console.SetCursorPosition((ScreenW / 2) - 7, 9);
            Console.Write("     ");
            Console.SetCursorPosition((ScreenW / 2) - 7, 11);
            Console.Write("     ");
            Console.SetCursorPosition((ScreenW / 2) - 4, 10);
            Console.Write("  ");


            Console.SetCursorPosition((ScreenW / 2) + 2, 2);
            Console.Write("          ");
            for (int i = 3; i <= 4; i++)
            {
                Console.SetCursorPosition((ScreenW / 2) + 1, i);
                Console.Write("  ");
            }
            Console.SetCursorPosition((ScreenW / 2) + 2, 4);
            Console.Write("    ");
            Console.SetCursorPosition((ScreenW / 2) + 6, 5);
            Console.Write("  ");
            Console.SetCursorPosition((ScreenW / 2) + 2, 6);
            Console.Write("    ");
            for (int i = 7; i <= 11; i++)
            {
                Console.SetCursorPosition((ScreenW / 2) + 1, i);
                Console.Write("  ");
            }
            for (int i = 3; i <= 11; i++)
            {
                Console.SetCursorPosition((ScreenW / 2) + 12, i);
                Console.Write("  ");
            }

            Console.SetCursorPosition((ScreenW / 2) - 2, 6);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ร");



            Console.ResetColor();
        }
    }
}