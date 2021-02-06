using LogHelper.Abstraction;
using System;
using System.Collections.Generic;

namespace LogHelper
{
    internal class ReaderFactory
    {
        private readonly Dictionary<string, Director> projects;

        public ReaderFactory(string[] rawDirectors)
        {
            projects = GetProjects(rawDirectors);
        }

        internal ReaderDescription[] GetDescriptions()
        {
            ReaderDescription[] descriptions = new ReaderDescription[projects.Count];
            int i = 0;
            foreach (KeyValuePair<string, Director> progect in projects)
                descriptions[i++] = new ReaderDescription() { Key = progect.Key, Label = progect.Value.ToString() };

            return descriptions;
        }

        internal IReader GetReaderByName(string name)
        {
            if (!projects.ContainsKey(name))
                throw new Exception("Запрошен несушествующий ридер.");
            IReader reader = (projects[name].GetReaderType()) switch
            {
                LogType.File => BuildReader(projects[name]),
                _ => throw new Exception("Запрошен ридер несуществующего типа"),
            };
            return reader;
        }

        private Dictionary<string, Director> GetProjects(string[] rawDirectors)
        {
            var directors = new Dictionary<string, Director>();

            int i = 0;
            while (i < rawDirectors.Length)
            {
                try
                {
                    directors.Add(rawDirectors[i], new FileDirector(rawDirectors[i + 1], rawDirectors[i + 2]));
                }
                catch (Exception ex)
                {
                    Logger.Log($"FileReader.GetReaderByName {ex.Message}\n {ex.StackTrace}");
                }
                i += 3;
            }

            return directors;
        }

        private IReader BuildReader(Director director)
        {
            return new FileReader(director.RegEx);
        }
    }
}
