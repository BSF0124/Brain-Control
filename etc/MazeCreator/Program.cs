using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("미로 크기 입력(x y) : ");
            String[] str = Console.ReadLine().Split();
            int x = int.Parse(str[0]);
            int y = int.Parse(str[1]);

            Board board = new Board();
            board.Initialize(y, x);

            // 커서의 깜빡거림을 끔
            Console.CursorVisible = false;

            // 커서의 위치를 0,0 지점으로 옮김
            Console.SetCursorPosition(0,0);
            board.Render();

            while (true)
            {
                if (Console.KeyAvailable) // 키 입력이 있는 경우
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true); // 키 입력을 받음

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.R: // R을 입력한 경우
                            Console.Clear(); // 콘솔을 지움
                            board.Initialize(y, x); // 미로를 다시 생성
                            Console.SetCursorPosition(0,0);
                            board.Render(); // 미로를 다시 그림
                            break;
                        case ConsoleKey.Q: // Q를 입력한 경우
                            return; // 프로그램 종료
                    }
                }
            }
        }
    }
}