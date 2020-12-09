using LogHelper.Abstraction;

namespace LogHelper
{
    internal class ReaderFactory
    {
        internal ReaderDescription[] GetDescriptions()
        {
            return new ReaderDescription[] {
                new ReaderDescription() { Key="Test", Label="Тестовый" },
            };
        }

        internal IReader GetReaderByName(string name)
        {
            return new FileReader();
        }
    }
}
