using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Facebook;
using Facebook.Models;
using Facebook.Requests;
using Newtonsoft.Json;
using Pagination.Primitives;

namespace OxfessAnalytica
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IEnumerable<OxfessAsResult> posts = await new OxfessAsFetcher().GetPosts();
            WriteJson(posts, "../data/posts.json");
            WriteCsv(posts, "../data/posts.csv");
        }

        private static IEnumerable<T> ReadJson<T>(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            using (var jsonTextReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<IEnumerable<T>>(jsonTextReader);
            }
        }
        
        private static void WriteJson<T>(IEnumerable<T> results, string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };
                serializer.Serialize(writer, results);
            }
        }
        
        private static void WriteCsv<T>(IEnumerable<T> results, string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                var serializer = new CsvWriter(writer);
                serializer.WriteRecords(results);
            }
        }
    }
}
