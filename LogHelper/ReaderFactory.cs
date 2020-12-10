using LogHelper.Abstraction;
using System;
using System.Collections.Generic;

namespace LogHelper
{
    internal class ReaderFactory
    {
        private readonly Dictionary<string, Director> projects;

        public ReaderFactory()
        {
            projects = GetProjects();
        }

        internal ReaderDescription[] GetDescriptions()
        {
            var descriptions = new ReaderDescription[projects.Count];
            int i = 0;
            foreach(KeyValuePair<string, Director> progect in projects)
                descriptions[i++] = new ReaderDescription() { Key = progect.Key, Label = progect.Value.ToString() };

            return descriptions;
        }

        internal IReader GetReaderByName(string name)
        {
            if (!projects.ContainsKey(name))
                throw new Exception("Запрошен несушествующий ридер.");

            IReader reader;
            switch (projects[name].GetReaderType())
            {
                case LogType.File:
                    reader = BuildReader(projects[name]);
                    break;
                default:
                    throw new Exception("Запрошен ридер несуществующего типа");
            }

            return reader;
        }

        private Dictionary<string, Director> GetProjects()
        {
            var projects = new Dictionary<string, Director>();

            projects.Add("Test", new FileDirector("Тестовый"));

            return projects;
        }

        private IReader BuildReader(Director director)
        {
            return new FileReader();
        }
    }
}
