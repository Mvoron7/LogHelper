using System;
using System.IO;

namespace LogHelper
{
    // Отладочное логирования
    public static class Logger
    {
        private static StreamWriter writer = new StreamWriter(@"I:\logs\LogHelper.txt", true) { AutoFlush = true, };

        private static string getData() 
        {
            return DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.ffff");
        }

        public static void Log(string log)
        {
            string s = $"{getData()} {log}";
            writer.WriteLine(s);
        }
    }
}
