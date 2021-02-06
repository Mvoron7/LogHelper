using LogHelper.Abstraction;

namespace LogHelper
{
    internal class FileDirector : Director
    {
        public FileDirector(string description, string regEx)
        : base(description, regEx) { }

        public override LogType GetReaderType()
        {
            return LogType.File;
        }
    }
}
