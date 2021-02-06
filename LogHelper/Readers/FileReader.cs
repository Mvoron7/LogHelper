using LogHelper.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace LogHelper
{
    //TODO Перевести FileReader на работу с потоком
    public class FileReader : IReader
    {
        private readonly string _pattern;

        public FileReader(string pattern)
        {
            _pattern = pattern;
        }

        public IEnumerable<LogElement> Open(string target)
        {
            Logger.Log($"FileReader.Open start");
#if DEBUG
            target = @"i:\Projects\LogHelper\Test.log";
#endif
            List<LogElement> answer = new List<LogElement>();
            using (StreamReader stream = new StreamReader(target))
            {
                string file = stream.ReadToEnd();
                Logger.Log($"FileReader.Open file loaded");

                Regex regex = new Regex(_pattern, RegexOptions.Compiled);
                MatchCollection matches = regex.Matches(file);
                Logger.Log($"FileReader.Open regex done");
                foreach (Match t in matches)
                {
                    try
                    {
                        answer.Add(new LogElement(DateTime.Parse(t.Groups[1].Value), t.Groups[2].Value, t.Groups[3].Value));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message);
                    }
                }
            }

            Logger.Log($"FileReader.Open done");
            return answer;
        }

        LogType IReader.GetType()
        {
            return LogType.File;
        }
    }
}
