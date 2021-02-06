namespace LogHelper.Abstraction
{
    public abstract class Director
    {
        public readonly string Description;

        public readonly string RegEx;

        protected Director(string description, string regEx)
        {
            Description = description;
            RegEx = regEx;
        }

        public abstract LogType GetReaderType();

        public override string ToString()
        {
            return Description;
        }
    }
}
