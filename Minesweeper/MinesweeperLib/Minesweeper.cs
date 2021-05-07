using System;

namespace MinesweeperLib
{
    /// <summary>
    /// マインスイーパークラス
    /// </summary>
    public class Minesweeper
    {
        /// <summary>
        /// マインスイーパーの構成
        /// </summary>
        public class Configuration
        {
            /// <summary>
            /// フィールドの幅
            /// </summary>
            public int FieldWidth { get; set; }

            /// <summary>
            /// フィールドの高さ
            /// </summary>
            public int FieldHeight { get; set; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Minesweeper(int width, int height)
        {
            Field = new FieldValue[height, width];

            for (int h = 0; h < Field.GetLength(0); ++h)
            {
                for (int w = 0; w < Field.GetLength(1); ++w)
                {
                    Field[h, w] = FieldValue.Empty;
                }
            }
        }

        /// <summary>
        /// ゲームを作成する
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static Minesweeper CreateGame(Configuration config)
        {
            var minesweeper = new Minesweeper(config.FieldWidth, config.FieldHeight);
            return minesweeper;
        }

        /// <summary>
        /// 
        /// </summary>
        private enum FieldValue
        {
            Empty,
            Mine
        }

        /// <summary>
        /// 
        /// </summary>
        private FieldValue[,] Field;
    }
}
