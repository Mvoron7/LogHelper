using System.Collections.Generic;

namespace LogHelper.Abstraction
{
    public interface IReader
    {
        /// <summary>Читает лог из источника</summary>
        /// <param name="target">Указатель на источник</param>
        /// <returns>Коллекция элементов</returns>
        IEnumerable<LogElement> Open(string target);

        /// <summary>Получает тип лога</summary>
        /// <returns>Тип лога</returns>
        LogType GetType();
    }
}
