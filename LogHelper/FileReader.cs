using LogHelper.Abstraction;
using System.Collections.Generic;

namespace LogHelper
{
    public class FileReader : IReader
    {
        public IEnumerable<LogElement> Open(string target)
        {
            return new List<LogElement>()
            {
                new LogElement(),
            };
        }

        LogType IReader.GetType()
        {
            return LogType.File;
        }
    }
}
