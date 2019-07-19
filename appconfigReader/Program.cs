using System;
using System.Collections.Generic;

namespace appconfigReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "path/to/config/file/web.config";
            var parsersToUse = new List<IConfigurationParser> {
                new KeyValueParser(),
                new KeyValueParser("name", "connectionString")
            };

            // create the provider
            var provider = new ConfigFileConfigurationProvider(
                file, loadFromFile: true, optional: false, null, parsersToUse);

            // Read and parse the file
            provider.Load();

        }
    }
}
