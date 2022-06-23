using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4j.Driver;

namespace inserter
{
    public abstract class Node
    {
        public static IDriver _driver = GraphDatabase.Driver($"neo4j://{Config.settings().Item1}:{Config.settings().Item2}", AuthTokens.Basic(
                Config.settings().Item3,
                Config.settings().Item4
        ));

        public abstract Task<string> CreateNode();

        

        

        
    }
}
