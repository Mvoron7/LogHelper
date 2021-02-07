using LogHelper.Abstraction;
using System;
using System.Collections.Generic;

namespace LogHelper
{
    public class Core : ICallBack
    {
        private readonly WPF _adapter;
        private readonly DataContainer _dataContainer;
        private readonly ReaderFactory _factory;

        internal Core(WPF adapter, DataContainer dataContainer, ReaderFactory factory)
        {
            Logger.Log("Core start");
            _adapter = adapter;
            _dataContainer = dataContainer;
            _factory = factory;

            ReaderDescription[] descriptions = _factory.GetDescriptions();
            _adapter.SetAvailableReaders(descriptions);

            Logger.Log("Core done");
        }

        #region ICallBack
        public void StartReader(string name)
        {
            Logger.Log($"Core.StartReader start {name}");

            Director director = _factory.GetDirectorByName(name);
            switch (director.Type)
            {
                case LogType.File:
                    StartFileReader(director as FileDirector);
                    break;

                default:
                    throw new Exception("Не распознаный тип ридера");
            }
            Logger.Log("Core.StartReader done");
        }
        #endregion

        #region Readers
        private void StartFileReader(FileDirector director)
        {
            Logger.Log($"Core.StartFileReader start");

            string file = _adapter.GetFileName(director.Extension);
            //if (!File.Exists(file))
            //    throw new Exception("File not exsist");
            IReader reader = _factory.BuildReader(director);
            try
            {
                IEnumerable<LogElement> toAdd = reader.Open(file);
                _dataContainer.Add(toAdd);
                _adapter.BindData(_dataContainer);
                Logger.Log($"Core.StartFileReader done");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
        }
        #endregion
    }
}
