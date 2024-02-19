using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] _tile;
        public int _width, _height;

        public enum TileType
        {
            Empty,
            Wall,
        }

        public void Initialize(int width, int height)
        {
            // Exception
            if (width % 2 == 0 || height % 2 == 0)
                return;

            _tile = new TileType[height, width];
            _width = width;
            _height = height;

            GenerateByBinaryTree();
        }

        void GenerateByBinaryTree()
        {
            // 초기 맵 설정
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }

            // Random 길뚫기
            Random rand = new Random();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    // 보완 //
                    if (y == _height - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == _width - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }
                    // 보완 //

                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        _tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor; 

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Console.ForegroundColor = GetTileColor(_tile[y, x]);                  
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor; 
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
