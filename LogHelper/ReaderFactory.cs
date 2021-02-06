using LogHelper.Abstraction;
using System;
using System.Collections.Generic;

namespace LogHelper
{
    internal class ReaderFactory
    {
        private readonly Dictionary<string, Director> projects;

        public ReaderFactory(string[] rawDirecrors)
        {
            projects = GetProjects(rawDirecrors);
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

        private Dictionary<string, Director> GetProjects(string[] rawDirectors)
        {
            var direcrors = new Dictionary<string, Director>();

            int i = 0;
            while (i < rawDirectors.Length)
            {
                try
                {
                    direcrors.Add(rawDirectors[i], new FileDirector(rawDirectors[i + 1], rawDirectors[i + 2], rawDirectors[i + 3]));
                }
                catch (Exception ex)
                {
                    Logger.Log($"FileReader.GetReaderByName {ex.Message}\n {ex.StackTrace}");
                }
                i += 4;
            }

            return direcrors;
        }

        
    }
}
