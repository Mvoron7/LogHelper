using LogHelper.Abstraction;

namespace LogHelper
{
    internal class FileDirector : Director
    {
        public FileDirector(string description)
        {
            Description = description;
        }

        public override LogType GetReaderType()
        {
            return LogType.File;
        }
    }
}
