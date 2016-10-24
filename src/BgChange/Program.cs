// 1. 定位到程序根目录 执行  dotnet run 
// 2. 定位到 bin 下的 debug文件夹 /release 文件夹 /发布到的路径  dotnet bgchange.dll


/*
 * 平台 ： .Net Core
 *  OS  ： Windows 7&7+
 * 简介 ： 调用Bing每日图片API，实现每天更换一张壁纸的小工具
 */

using System;
using System.Runtime.InteropServices;
using static System.Console;
namespace BgChange
{

    public class Program
    {
        /// <summary>
        /// 用来设置壁纸的WinAPI
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int SystemParametersInfo(
           int uAction,
           int uParam,
           string lpvParam,
           int fuWinIni
           );


        public static void Main(string[] args)
        {
            WriteLine("Please Wait.");

            InputEncoding = System.Text.Encoding.UTF8;
            OutputEncoding = System.Text.Encoding.UTF8;

            // 文件存放路径
            var filePath = System.IO.Directory.GetCurrentDirectory() + "\\img\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";

            // 下载文件完成后的回调方法
            Action callBack = () =>
            {
                // 设置桌面壁纸 
                SystemParametersInfo(20, 0, filePath, 0x1);

                WriteLine("Setting successful!");
                LoadAnimation.Stop();
            };

            // 异常处理
            Action<Exception> errorHandler = (e) =>
            {
                LoadAnimation.Stop();
                WriteLine("Setting Failed! " + e.Message);
            };


            var bingImg = new BingPerDayImg();
            bingImg.GetImg(filePath, callBack, errorHandler);

            // 光标转动
            var cursorLeft = CursorLeft;
            var cursorTop = CursorTop;
            LoadAnimation.Start(cursorLeft, CursorTop, 200);


            // 调试环境下 等待用户输入，以查看输出信息
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                Read();
            }



        }
    }
}
