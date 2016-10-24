using System;
using System.Threading;

namespace BgChange
{
    /// <summary>
    /// 正在加载的动画效果  即 光标转动
    /// </summary>
    public class LoadAnimation
    {
        private static string cursorState = @"-\|/";
        private static bool isDo = true;

        public static void Start(int cursorLeft, int cursorTop, int rate)
        {
            while (isDo)
            {
                foreach (var cyrrentCursorState in cursorState)
                {
                    if (isDo)
                    {
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Console.WriteLine(cyrrentCursorState);
                        Thread.Sleep(rate);
                    }
                }
            }
        }

        public static void Stop()
        {
            isDo = false;
        }

    }
}
