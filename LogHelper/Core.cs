using LogHelper.Abstraction;
using System;
using System.Collections.Generic;

namespace LogHelper
{
    public class Core
    {
        private WPF _adapter;
        private DataContainer _dataContainer;
        private ReaderFactory _factory;

        public Core()
        {
            _adapter = new WPF();
            _dataContainer = new DataContainer();
            _factory = new ReaderFactory();
        }

        public void Init(MainWindow window)
        {
            _adapter.Init(window);

            ReaderDescription[] descriptions = _factory.GetDescriptions();
            _adapter.SetAvailableReaders(descriptions);
        }

        public void StartReader(string name)
        {
            IReader reader = _factory.GetReaderByName(name);
            switch (reader.GetType())
            {
                case LogType.File:
                    StartFileReader(reader);
                    break;

                default:
                    throw new Exception("Не распознаный тип ридера");
            }
        }

        private void StartFileReader(IReader reader)
        {
            string file = _adapter.GetFileName("Log File|*.log");
            //if (!File.Exists(file))
            //    throw new Exception("File not exsist");

            IEnumerable<LogElement> toAdd = reader.Open(file);
            _dataContainer.Add(toAdd);
        }
    }
}
