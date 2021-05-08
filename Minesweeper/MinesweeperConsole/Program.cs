using MinesweeperLib;
using System.Collections.Generic;

namespace MinesweeperConsole
{
    class Program
    {
        static void Main(string[] args)
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
                        System.Console.Write("＠");
                    }
                    else
                    {
                        System.Console.Write(state[minesweeper.GetFieldState(x, y)]);
                    }
                }
                System.Console.WriteLine();
            }
        }
    }
}
