using System.Collections.Generic;

namespace LogHelper
{
    public class DataContainer
    {
        public List<LogElement> AllElements { get; private set; }
        public Dictionary<string, List<LogElement>> Module { get; private set; }

        public DataContainer()
        {
            AllElements = new List<LogElement>();
            Module = new Dictionary<string, List<LogElement>>();
        }

        /// <summary>Распределяет элементы по спискам</summary>
        /// <param name="elements">Коллекция элементов для добавления</param>
        public void Add(IEnumerable<LogElement> elements)
        {
            Logger.Log("DataContainer.Add start");

            foreach (LogElement element in elements)
            {
                AllElements.Add(element);

                if (!Module.ContainsKey(element.Tag))
                    Module.Add(element.Tag, new List<LogElement>());
                Module[element.Tag].Add(element);
            }

            Logger.Log("DataContainer.Add done");
        }
    }
}
