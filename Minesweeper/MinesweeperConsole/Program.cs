using MinesweeperLib;
using System.Collections.Generic;

namespace MinesweeperConsole
{
    class Program
    {
        static void Test()
        {
            var config = new Minesweeper.Configuration();
            var minesweeper = Minesweeper.CreateGame(config);

            minesweeper.Open(1, 1);

            var state = new Dictionary<Minesweeper.FieldState, string>
            {
                { Minesweeper.FieldState.Unopen, "■" },
                { Minesweeper.FieldState.OpenedEmptyMine, "□" },
                { Minesweeper.FieldState.Opened1Mine, "１" },
                { Minesweeper.FieldState.Opened2Mine, "２" },
                { Minesweeper.FieldState.Opened3Mine, "３" },
                { Minesweeper.FieldState.Opened4Mine, "４" },
                { Minesweeper.FieldState.Opened5Mine, "５" },
                { Minesweeper.FieldState.Opened6Mine, "６" },
                { Minesweeper.FieldState.Opened7Mine, "７" },
                { Minesweeper.FieldState.Opened8Mine, "８" },
                { Minesweeper.FieldState.OpenedMine, "＠" },
                { Minesweeper.FieldState.Flag, "▽" },
            };

            for (int y = 0; y < config.FieldHeight; ++y)
            {
                for (int x = 0; x < config.FieldWidth; ++x)
                {
                    if (minesweeper.CheckMine(x, y))
                    {
                        System.Console.Write(state[Minesweeper.FieldState.OpenedMine]);
                    }
                    else
                    {
                        System.Console.Write(state[minesweeper.GetFieldState(x, y)]);
                    }
                }
                System.Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            var config = new Minesweeper.Configuration();
            var minesweeper = Minesweeper.CreateGame(config);

            var screen = new Screen(config.FieldWidth, config.FieldHeight);

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            int cursorX = 0, cursorY = 0;
            do
            {
                Input.Instance.Update();

                if (Input.Instance.IsKeyTrigger(Input.KeyCode.Z))
                {
                    minesweeper.Open(cursorX, cursorY);
                }
                else if (Input.Instance.IsKeyTrigger(Input.KeyCode.X))
                {
                    minesweeper.SwitchFlag(cursorX, cursorY);
                }
                else if (Input.Instance.IsKeyPress(Input.KeyCode.Up))
                {
                    if (--cursorY < 0)
                    {
                        cursorY = 0;
                    }
                }
                else if (Input.Instance.IsKeyPress(Input.KeyCode.Down))
                {
                    if (++cursorY > config.FieldHeight - 1)
                    {
                        cursorY = config.FieldHeight - 1;
                    }
                }
                else if (Input.Instance.IsKeyPress(Input.KeyCode.Left))
                {
                    if (--cursorX < 0)
                    {
                        cursorX = 0;
                    }
                }
                else if (Input.Instance.IsKeyPress(Input.KeyCode.Right))
                {
                    if (++cursorX > config.FieldWidth - 1)
                    {
                        cursorX = config.FieldWidth - 1;
                    }
                }

                screen.Draw(minesweeper, cursorX, cursorY);

                System.Threading.Thread.Sleep(1000 / 10);
            }
            while (!Input.Instance.IsKeyPress(Input.KeyCode.S));

            stopwatch.Stop();
        }
    }
}
