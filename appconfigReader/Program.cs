using System;
using System.Collections.Generic;
using System.Linq;

namespace appconfigReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = Console.ReadLine();
            var parsersToUse = new List<IConfigurationParser> {
                new KeyValueParser(),
                new KeyValueParser("name", "connectionString")
            };

            var provider = new ConfigFileConfigurationProvider(
                file, loadFromFile: true, optional: false, null, parsersToUse);

            provider.Load();
            const string SectionDelimiter = "_";
            var keyValues = provider.GetChildKeys(Enumerable.Empty<string>(), null)
                .Select(key =>
                {
                    provider.TryGet(key, out var value);
                    if (OnlyWhiteSpace(value))
                    {
                        value = "";
                    }
                    return new KeyValuePair<string, string>(key, value);
                });
            foreach (var key in keyValues)
            {
                Console.WriteLine($"\"{key.Key}\": \"{key.Value}\",");
            }

            Console.ReadLine();
        }

        public static bool OnlyWhiteSpace(string input)
        {
            int nonSpace = 0;
            foreach (var character in input.ToArray())
            {
                if (!char.IsWhiteSpace(character))
                {
                    nonSpace++;
                }
            }
            return nonSpace == 0;
        }
    }
}
