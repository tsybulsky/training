using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    class Game
    {
        public enum MoveState
        {
            Continue,
            GameOver,
            Invalid,            
            Restart,
            Winned
        };

        private const int Princess = 11;
        private char[,] gameField = new char[10, 10];
        private ConsoleColor[,] backColors = new ConsoleColor[10, 10];
        private ConsoleColor[,] foreColors = new ConsoleColor[10, 10];
        private string symbols = " 123456789AP";
        private int restScore = 0;
        private int currentColumn = 0;
        private int currentRow = 0;

        void InitializeField()
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    gameField[i, j] = ' ';
                    backColors[i, j] = ConsoleColor.Black;
                    foreColors[i, j] = ConsoleColor.Black;
                }
            }
            var rand = new Random();
            int column = rand.Next(0, 10);
            gameField[column, 9] = 'P';
            foreColors[column, 9] = ConsoleColor.White;
            currentColumn = rand.Next(0,10);
            backColors[currentColumn, 0] = ConsoleColor.Green;
            currentRow = 0;
            int restItems = 10;
            while (restItems > 0)
            {
                column = rand.Next(0, 10);
                int row = rand.Next(0, 10);
                if ( (!((column == currentColumn)&&(row == 0)))&&(gameField[column, row] == ' '))
                {
                    gameField[column, row] = symbols[rand.Next(1, 10)];
                    restItems--;
                }
            }
        }

        public void DrawField()
        {            
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\u250C");
            for (var i = 0; i < 10; i++)
                Console.Write("\u2500");
            Console.Write("\u2510\r\n");
            for (var row = 0; row < 10; row++)
            {
                Console.Write("\u2502");
                for (var column = 0; column < 10; column++)
                {
                    if ((column == currentColumn)&&(row == currentRow))
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.BackgroundColor = backColors[column, row];
                    }
                    Console.ForegroundColor = foreColors[column, row];
                    Console.Write(gameField[column, row]);                    
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\u2502\r\n");
            }
            Console.Write("\u2514");
            for (var i = 0; i < 10; i++)
            {
                Console.Write("\u2500");
            }
            Console.WriteLine("\u2518");
            string suffix = "";
            if (restScore == 1)
            {
                suffix = "о  ";
            }
            else if ((restScore >= 2) && (restScore <= 4))
            { 
                suffix = "а  ";
            }
            else
            {
                suffix = "ов ";
            }
            Console.WriteLine($"Осталось {((restScore < 0)?0:restScore)} очк{suffix}");
        }

        MoveState CheckCell(int column, int row)
        {
            if ((column >= 0) && (column < 10) && (row >= 0) && (row < 10))
            {
                int cellValue = symbols.IndexOf(gameField[column, row]);
                if (cellValue <= 0)
                {
                    backColors[column, row] = ConsoleColor.Green;
                    foreColors[column, row] = ConsoleColor.White;
                    return MoveState.Continue;
                }
                if ((cellValue > 0) && (cellValue <= 10)) 
                {
                    restScore -= cellValue;
                    backColors[column, row] = ConsoleColor.Red;
                    foreColors[column, row] = ConsoleColor.White;
                    return (restScore <= 0) ? MoveState.GameOver : MoveState.Continue;
                }
                if (cellValue == Princess)
                {
                    return MoveState.Winned;
                }
                else
                {
                    backColors[column, row] = ConsoleColor.Green;
                    foreColors[column, row] = ConsoleColor.White;
                    return MoveState.Continue;
                }
            }
            else
                return MoveState.Invalid;
        }

        public MoveState MoveUp()
        {
            MoveState state = CheckCell(currentColumn, currentRow - 1);
            if (state != MoveState.Invalid)
            {
                currentRow -= 1;                
            }
            DrawField();
            return state;
        }

        public MoveState MoveLeft()
        {
            MoveState state = CheckCell(currentColumn - 1, currentRow);
            if (state != MoveState.Invalid)
            {
                currentColumn -= 1;                
            }
            DrawField();
            return state;
        }

        public MoveState MoveRight()
        {
            MoveState state = CheckCell(currentColumn + 1, currentRow);
            if (state != MoveState.Invalid)
            {
                currentColumn += 1;
               
            }
            DrawField();
            return state;
        }

        public MoveState MoveDown()
        {
            MoveState state = CheckCell(currentColumn, currentRow + 1);
            if (state != MoveState.Invalid)
            {
                currentRow += 1;                
            }
            return state;
        }

        public void MakeAllCellsVisible()
        {
            for (var row = 0; row < 10; row++)
            {
                for (var column = 0; column < 10; column++)
                {
                    foreColors[column, row] = ConsoleColor.White;
                }
            }
        }

        public Game()
        {
            restScore = 10;
            InitializeField();            
            DrawField();            
        }
    }

    class Program
    { 

        static void Main(string[] args)
        {            
            while (true)
            {
                Console.Clear();
                Game.MoveState state = Game.MoveState.Invalid;
                var game = new Game();                
                while (state != Game.MoveState.Restart)
                {                    
                    var key = Console.ReadKey();
                    switch(key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                state = game.MoveUp();                                
                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                state = game.MoveDown();
                                break;
                            }
                        case ConsoleKey.RightArrow:
                            {
                                state = game.MoveRight();
                                break;
                            }
                        case ConsoleKey.LeftArrow:
                            {
                                state = game.MoveLeft();
                                break;
                            }
                        case ConsoleKey.Escape:
                            {
                                return;
                            }
                        case ConsoleKey.F2:
                            {
                                state = Game.MoveState.Restart;
                                break;
                            }                            
                    }
                    switch (state)
                    {
                        case Game.MoveState.Winned:
                        case Game.MoveState.GameOver:
                            {
                                game.MakeAllCellsVisible();
                                game.DrawField();
                                if (state == Game.MoveState.Winned)
                                {
                                    Console.WriteLine("Мои поздравления!!! Вы выиграли");
                                }
                                else
                                {
                                    Console.WriteLine("К сожалению вы проиграли");
                                }
                                Console.Write("Еще раз сыграем? [Y - да, N - нет]: ");
                                do
                                {
                                    key = Console.ReadKey();
                                } while ((key.Key != ConsoleKey.N) && (key.Key != ConsoleKey.Y));
                                if (key.Key == ConsoleKey.N)
                                {
                                    return;
                                }
                                if (key.Key == ConsoleKey.Y)
                                {
                                    state = Game.MoveState.Restart;
                                }
                                break;
                            }                       
                        case Game.MoveState.Continue:
                            {
                                game.DrawField();
                                break;
                            }
                    }
                }
            }
        }
    }
}
