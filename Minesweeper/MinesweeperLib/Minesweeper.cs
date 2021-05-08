using System;
using System.Collections.Generic;

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

            /// <summary>
            /// 乱数シード
            /// </summary>
            public int RandomSeed;

            /// <summary>
            /// フィールド数における地雷の比率
            /// </summary>
            public double MinesRate;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Configuration()
            {
                FieldWidth = FieldHeight = 24;
                RandomSeed = 0;
                MinesRate = 0.1f;
            }
        }

        /// <summary>
        /// フィールドの状態
        /// </summary>
        public enum FieldState
        {
            /// <summary>
            /// 選択なし
            /// </summary>
            Unopen,

            /// <summary>
            /// 選択済み地雷なし確定
            /// </summary>
            OpenedEmptyMine,

            /// <summary>
            /// 選択済み地雷が周囲に存在（1）
            /// </summary>
            Opened1Mine,

            /// <summary>
            /// 選択済み地雷が周囲に存在（2）
            /// </summary>
            Opened2Mine,

            /// <summary>
            /// 選択済み地雷が周囲に存在（3）
            /// </summary>
            Opened3Mine,

            /// <summary>
            /// 選択済み地雷が周囲に存在（4）
            /// </summary>
            Opened4Mine,

            /// <summary>
            /// 選択済み地雷が周囲に存在（5）
            /// </summary>
            Opened5Mine,

            /// <summary>
            /// 選択済み地雷が周囲に存在（6）
            /// </summary>
            Opened6Mine,

            /// <summary>
            /// 選択済み地雷が周囲に存在（7）
            /// </summary>
            Opened7Mine,

            /// <summary>
            /// 選択済み地雷が周囲に存在（8）
            /// </summary>
            Opened8Mine,

            /// <summary>
            /// フラグ
            /// </summary>
            Flag,

            /// <summary>
            /// 地雷
            /// </summary>
            OpenedMine,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Minesweeper(Configuration config)
        {
            FieldMineLayer = new bool[config.FieldHeight, config.FieldWidth];
            FieldStateLayer = new FieldState[config.FieldHeight, config.FieldWidth];

            for (int y = 0; y < FieldMineLayer.GetLength(0); ++y)
            {
                for (int x = 0; x < FieldMineLayer.GetLength(1); ++x)
                {
                    FieldMineLayer[y, x] = false;
                    FieldStateLayer[y, x] = FieldState.Unopen;
                }
            }

            Config = config;
            SetMines();
        }

        /// <summary>
        /// フィールドを開く
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public FieldState Open(int x, int y)
        {
            if (x < 0 || y < 0 || Config.FieldWidth <= x || Config.FieldHeight <= y)
            {
                return FieldState.Unopen;
            }

            if (FieldMineLayer[y, x])
            {
                FieldStateLayer[y, x] = FieldState.OpenedMine;
            }
            else
            {
                var offsets = new int[,] 
                {
                    { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 },
                    { 1, 1 }, { 1, -1 }, { -1, -1 }, { -1, 1 },
                };

                int mine = 0;
                for (int index = 0; index < offsets.GetLength(0); ++index)
                {
                    if (CheckMine(x + offsets[index, 0], y + offsets[index, 1]))
                    {
                        ++mine;
                    }
                }

                if (mine == 0)
                {
                    FieldStateLayer[y, x] = FieldState.OpenedEmptyMine;

                    for (int index = 0; index < offsets.GetLength(0); ++index)
                    {
                        if (CanOpen(x + offsets[index, 0], y + offsets[index, 1]))
                        {
                            Open(x + offsets[index, 0], y + offsets[index, 1]);
                        }
                    }
                }
                else
                {
                    FieldStateLayer[y, x] = FieldState.OpenedEmptyMine + mine;
                }
            }

            return FieldStateLayer[y, x];
        }

        /// <summary>
        /// フラッグ状態を入れ替える
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool SwitchFlag(int x, int y)
        {
            if (x < 0 || y < 0 || Config.FieldWidth <= x || Config.FieldHeight <= y)
            {
                return false;
            }

            switch (FieldStateLayer[y, x])
            {
                case FieldState.Unopen:
                    FieldStateLayer[y, x] = FieldState.Flag;
                    return true;

                case FieldState.Flag:
                    FieldStateLayer[y, x] = FieldState.Unopen;
                    return true;
            }

            return false;
        }

        /// <summary>
        /// フィールドの状態を取得
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public FieldState GetFieldState(int x, int y)
        {
            return FieldStateLayer[y, x];
        }

        /// <summary>
        /// 地雷があるかを判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckMine(int x, int y)
        {
            if (x < 0 || y < 0 || Config.FieldWidth <= x || Config.FieldHeight <= y) return false;

            return FieldMineLayer[y, x];
        }

        /// <summary>
        /// 開けることができるかを判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool CanOpen(int x, int y)
        {
            if (x < 0 || y < 0 || Config.FieldWidth <= x || Config.FieldHeight <= y) return false;

            return !FieldMineLayer[y, x] && FieldStateLayer[y, x] == FieldState.Unopen;
        }

        /// <summary>
        /// 地雷を埋める
        /// </summary>
        private void SetMines()
        {
            var random = new Random(Config.RandomSeed);

            var mines = (int)(Config.FieldWidth * Config.FieldHeight * Config.MinesRate);

            for (int i = 0; i < mines; ++i)
            {
                int x = random.Next(Config.FieldWidth);
                int y = random.Next(Config.FieldHeight);
                FieldMineLayer[y, x] = true;
            }
        }

        /// <summary>
        /// ゲームを作成する
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static Minesweeper CreateGame(Configuration config)
        {
            return new Minesweeper(config);
        }

        /// <summary>
        /// フィールド中の地雷を表す配列
        /// </summary>
        private bool[,] FieldMineLayer;

        /// <summary>
        /// フィールドの状態を表す配列
        /// </summary>
        private FieldState[,] FieldStateLayer;

        /// <summary>
        /// ゲーム設定
        /// </summary>
        private Configuration Config;
    }
}
