using System;

namespace LogHelper
{
    public class LogElement
    {
        public DateTime Date { get; set; }

        public string Tag { get; set; }

        public string Message { get; set; }

        /// <summary>Возврашет значение будет ли отображаться объект</summary>
        public bool Visible { get; set; }

        public LogElement(DateTime date, string tag, string message)
        {
            Date = date;
            Tag = tag;
            Message = message;
            Visible = true;
        }
    }
}