using LogHelper.Abstraction;
using System;
using System.Collections.Generic;

namespace LogHelper
{
    public class Core
    {
        private readonly WPF _adapter;
        private readonly DataContainer _dataContainer;
        private readonly ReaderFactory _factory;

        public Core()
        {
            Logger.Log("Core start");
            _adapter = new WPF();
            _dataContainer = new DataContainer();
            _factory = new ReaderFactory();

            Logger.Log("Core done");
        }

        public void Init(MainWindow window)
        {
            Logger.Log("Core.Init start");
            _adapter.Init(window);

            ReaderDescription[] descriptions = _factory.GetDescriptions();
            _adapter.SetAvailableReaders(descriptions);

            BindData();
            Logger.Log("Core.Init done");
        }

        public void StartReader(string name)
        {
            Logger.Log($"Core.StartReader start {name}");
            IReader reader = _factory.GetReaderByName(name);
            switch (reader.GetType())
            {
                case LogType.File:
                    StartFileReader(reader);
                    break;

                default:
                    throw new Exception("Не распознаный тип ридера");
            }
            Logger.Log("Core.StartReader done");
        }

        private void StartFileReader(IReader reader)
        {
            Logger.Log($"Core.StartFileReader start");
            string file = _adapter.GetFileName("Log File|*.log");
            //if (!File.Exists(file))
            //    throw new Exception("File not exsist");
            try
            {
                IEnumerable<LogElement> toAdd = reader.Open(file);
                _dataContainer.Add(toAdd);
                Logger.Log($"Core.StartFileReader done");
            }
            catch (Exception ex) 
            {
                Logger.Log(ex.Message);
            }
        }

        private void BindData()
        {
            Logger.Log($"Core.BindData start");
            _adapter.BindData(new List<LogElement>()
            {
                new LogElement(DateTime.Now, "Tag_1", "Message_1"),
                new LogElement(DateTime.Now, "Tag_2", "Message_2"),
            });
            Logger.Log($"Core.BindData done");
        }
    }
}
