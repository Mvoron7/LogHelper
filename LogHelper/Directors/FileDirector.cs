using LogHelper.Abstraction;

namespace LogHelper
{
    internal class FileDirector : Director
    {
        public readonly string Extension;

        public FileDirector(string description, string regEx, string extension)
        : base(description, regEx, LogType.File) 
        {
            Extension = extension;
        }

        public override LogType GetReaderType()
        {
            return LogType.File;
        }
    }
}
