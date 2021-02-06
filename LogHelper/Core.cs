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

        // Это строка должна получаться из файла настроек.
        string[] directors = { "File", "Тестовый", "<date[ ]?=[ ]?\"([\\d]{4}-[\\d]{2}-[\\d]{2} [\\d]{2}:[\\d]{2}:[\\d]{2}.[\\d]{4})\"[ ]?Tag[ ]?=[ ]?\"([^\"]*)\"[ ]?Message[ ]?=[ ]?\"([^\"]*)\"[ ]?>" };

        public Core()
        {
            Logger.Log("Core start");
            _adapter = new WPF();
            _dataContainer = new DataContainer();
            _factory = new ReaderFactory(directors);

            Logger.Log("Core done");
        }

        public void Init(StartWindow window)
        {
            Logger.Log("Core.Init start");
            _adapter.Init(window);

            ReaderDescription[] descriptions = _factory.GetDescriptions();
            _adapter.SetAvailableReaders(descriptions);

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
                BindData();
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
            _adapter.BindData(_dataContainer);
            Logger.Log($"Core.BindData done");
        }
    }
}
