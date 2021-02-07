using LogHelper.Abstraction;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace LogHelper
{
    internal class ReaderFactory
    {
        private readonly Dictionary<string, Director> projects;

        public ReaderFactory(IConfigurationSection configuration)
        {
            projects = GetProjects(configuration);
        }

        internal ReaderDescription[] GetDescriptions()
        {
            ReaderDescription[] descriptions = new ReaderDescription[projects.Count];
            int i = 0;
            foreach (KeyValuePair<string, Director> progect in projects)
                descriptions[i++] = new ReaderDescription() { Key = progect.Key, Label = progect.Value.ToString() };

            return descriptions;
        }

        internal Director GetDirectorByName(string name)
        {
            if (!projects.ContainsKey(name))
                throw new Exception("Запрошен несушествующий ридер.");
            return projects[name];
        }

        #region BuildReaders
        internal IReader BuildReader(FileDirector director)
        {
            return new FileReader(director.RegEx);
        }
        #endregion

        private Dictionary<string, Director> GetProjects(IConfigurationSection configuration)
        {
            var direcrors = new Dictionary<string, Director>();

            foreach (var part in configuration.GetChildren())
            {
                try
                {
                    switch (part.GetSection("type").Value)
                    {
                        case "File":
                            direcrors.Add(part.GetSection("id").Value, new FileDirector(part.GetSection("description").Value, part.GetSection("regEx").Value, part.GetSection("extension").Value));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log($"FileReader.GetReaderByName {ex.Message}\n {ex.StackTrace}");
                }
            }
            return direcrors;
        }
    }
}
