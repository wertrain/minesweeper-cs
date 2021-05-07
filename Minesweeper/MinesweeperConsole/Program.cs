using MinesweeperLib;

namespace MinesweeperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Minesweeper.Configuration();
            config.FieldWidth = 24;
            config.FieldHeight = 24;
            var minesweeper = Minesweeper.CreateGame(config);
        }
    }
}
