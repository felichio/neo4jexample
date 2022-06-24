using System;
using System.IO;
using System.Text.Json;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inserter
{

    

    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            int total_days = 31;
            for (int k = 1; k < total_days + 1; k++)
            {
                foreach (Element element in Enum.GetValues(typeof(Element)))
                {
                    IEnumerable<Node> nodes = read(element, k);
                    IEnumerator<Node> en = nodes.GetEnumerator();
                    while (en.MoveNext())
                    {
                        Node n = en.Current;
                        await n.CreateNode();
                    }
                }
            }

            Relationships rel = new Relationships();
            
            var IndexTasks = new List<Task<string>>() 
            {
                rel.CreateTransactionHashIndex(),
                rel.CreateTransactionDateIndex(),
                rel.CreateInputHashIndex(),
                rel.CreateOutputHashIndex(),
                rel.CreateInputRecipientIndex(),
                rel.CreateOutputRecipientIndex()
            };

            var RelsTasks = new List<Task<string>>() 
            {
                rel.CreateBlockTransaction(),
                rel.CreateInputTransaction(),
                rel.CreateOutputTransaction(),
                rel.CreateOutputInput(),
                rel.CreateGuessedMinerBlock()
            };

            foreach (Task<string> task in IndexTasks.Concat(RelsTasks))
            {
                await task;
            }
        }

        private static IEnumerable<Node> read(Element element, int day)
        {
            string[] fileNames = Config.index[element].ToArray();
            
            string filename = fileNames[day - 1];
            Console.WriteLine(filename);
            IEnumerable<Node> nodes = Config.read(filename, element);
            return nodes;
            
        }
    }
}
