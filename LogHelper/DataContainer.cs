using System.Collections.Generic;

namespace LogHelper
{
    public class DataContainer
    {
        public List<LogElement> Total { get; private set; }

        public DataContainer()
        {
            Total = new List<LogElement>();
        }

        /// <summary>Распределяет элементы по спискам</summary>
        /// <param name="elements">Коллекция элементов для добавления</param>
        public void Add(IEnumerable<LogElement> elements)
        {
            foreach (LogElement element in elements)
            {
                Total.Add(element);
            }
        }
    }
}
