using System;
using System.Collections.Generic;

namespace server
{
    public class Query4
    {
        
        public class Input
        {
            public string day {get; set;}
            public int N {get; set;}
        }

        public string recipient {get; set;}
        public int times {get; set;}
        public List<string> transaction_hashes {get; set;}


        // POST query4 returns JSON
        public static string query = "with date(datetime({epochmillis: apoc.date.parse($day, 'ms', 'yyyy-MM-dd')})) as start match (t:Transaction)-[:PAYS]->(o:Output) where date(t.time) = start with o.recipient as recipient, count(*) as hits, collect(t.hash) as hashes where hits > $N return recipient, hits, hashes;";
        
    }
}
