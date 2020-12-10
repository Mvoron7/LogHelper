namespace LogHelper.Abstraction
{
    public abstract class Director
    {
        public string Description;

        public abstract LogType GetReaderType();

        public override string ToString()
        {
            return Description;
        }
    }
}
