using MinesweeperLib;
using System.Collections.Generic;
namespace MinesweeperConsole
{
    /// <summary>
    /// マインスイーパーの描画を行う
    /// </summary>
    class Screen
    {
        /// <summary>
        /// フィールドの状態
        /// </summary>
        private char[,] Field { get; set; }

        /// <summary>
        /// ステートごとの文字
        /// </summary>
        private Dictionary<Minesweeper.FieldState, char> FieldStateTable = new Dictionary<Minesweeper.FieldState, char>
        {
            { Minesweeper.FieldState.Unopen, '■' },
            { Minesweeper.FieldState.OpenedEmptyMine, '□' },
            { Minesweeper.FieldState.Opened1Mine, '１' },
            { Minesweeper.FieldState.Opened2Mine, '２' },
            { Minesweeper.FieldState.Opened3Mine, '３' },
            { Minesweeper.FieldState.Opened4Mine, '４' },
            { Minesweeper.FieldState.Opened5Mine, '５' },
            { Minesweeper.FieldState.Opened6Mine, '６' },
            { Minesweeper.FieldState.Opened7Mine, '７' },
            { Minesweeper.FieldState.Opened8Mine, '８' },
            { Minesweeper.FieldState.OpenedMine, '＠' },
            { Minesweeper.FieldState.Flag, '▽' },
        };

        /// <summary>
        /// カーソルを表す文字
        /// </summary>
        private char CursorChar = '＊';

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Screen(int width, int height)
        {
            Field = new char[height, width];
        }

        /// <summary>
        /// 
        /// </summary>
        public void Draw(Minesweeper minesweeper, int cursorX, int cursorY)
        {
            // 値が変更されている部分だけを再描画する
            for (int y = 0; y < Field.GetLength(0); ++y)
            {
                for (int x = 0; x < Field.GetLength(1); ++x)
                {
                    if (Field[y, x] == FieldStateTable[minesweeper.GetFieldState(x, y)]) continue;

                    SetConsoleCursor(x, y);
                    System.Console.Write(FieldStateTable[minesweeper.GetFieldState(x, y)]);
                    Field[y, x] = FieldStateTable[minesweeper.GetFieldState(x, y)];
                }
            }

            // カーソルの表示
            SetConsoleCursor(cursorX, cursorY);
            System.Console.Write(CursorChar);
            
            // 次のフレームで過去のカーソル位置を再描画させるために値を変更する
            Field[cursorY, cursorX] = CursorChar;

            System.Console.CursorLeft = 0;
            System.Console.CursorTop = Field.GetLength(1) + 1;
            System.Console.CursorVisible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void SetConsoleCursor(int x, int y)
        {
            System.Console.CursorLeft = x * 2;
            System.Console.CursorTop = y;
        }
    }
}
