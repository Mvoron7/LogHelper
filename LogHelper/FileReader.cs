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

#if DEBUG
        public FileReader()
        {
            _pattern = "<date[ ]?=[ ]?\"([\\d]{4}-[\\d]{2}-[\\d]{2} [\\d]{2}:[\\d]{2}:[\\d]{2}.[\\d]{4})\"[ ]?Tag[ ]?=[ ]?\"([^\"]*)\"[ ]?Message[ ]?=[ ]?\"([^\"]*)\"[ ]?>";
#else
        public FileReader(string pattern)
        {
            _pattern = pattern;
#endif
        }

        public IEnumerable<LogElement> Open(string target)
        {
            var answer = new List<LogElement>();
            using (StreamReader stream = new StreamReader(target))
            {
                string file = stream.ReadToEnd();
#if DEBUG
                file = "<date=\"2020-01-01 00:00:00.0000\" Tag = \"ClassName\" Message=\"MessageBody\"><date=\"2020-01-01 00:00:00.0000\" Tag = \"ClassName\" Message=\"MessageBody\"><date=\"2020-01-01 00:00:00.0000\" Tag = \"ClassName\" Message=\"MessageBody\">";
#endif

                Regex regex = new Regex(_pattern, RegexOptions.Compiled);
                MatchCollection matches = regex.Matches(file);
                foreach (Match t in matches)
                {
                    try
                    {
                        answer.Add(new LogElement(DateTime.Parse(t.Groups[1].Value), t.Groups[2].Value, t.Groups[3].Value));
                    }
                    catch (Exception ex) {
                        Logger.Log(ex.Message);
                    }
                }

            }
            return answer;
        }

        LogType IReader.GetType()
        {
            return LogType.File;
        }
    }
}
