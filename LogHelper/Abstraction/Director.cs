namespace LogHelper.Abstraction
{
    public abstract class Director
    {
        public readonly string Description;

        public readonly string RegEx;

        public readonly LogType Type;

        protected Director(string description, string regEx, LogType type)
        {
            Description = description;
            RegEx = regEx;
            Type = type;
        }

        public abstract LogType GetReaderType();

        public override string ToString()
        {
            return Description;
        }
    }
}
