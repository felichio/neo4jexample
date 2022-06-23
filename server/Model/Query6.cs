using System;
using System.Collections.Generic;

namespace server
{
    public class Query6
    {
        
        public class Input
        {
            public string start {get; set;}
            public string end {get; set;}
            public int K {get; set;}
        }

        public string recipient {get; set;}
        public string day {get; set;}
        public int hits {get; set;}
        public List<string> outputs {get; set;}


        // POST query6 returns JSON
        public static string query = "with date(datetime({epochmillis: apoc.date.parse($start, 'ms', 'yyyy-MM-dd')})) as start, date(datetime({epochmillis: apoc.date.parse($end, 'ms', 'yyyy-MM-dd')})) as end match (i:Input)-[:FEEDS]->(t:Transaction)-[p:PAYS]->(o:Output) where date(t.time) >= start and date(t.time) <=end with i.recipient as recipient, t.hash as hash, date(t.time) as day, count(p) as hits, collect(o.recipient) as outputs order by hits desc return recipient, day, hits, outputs limit $K;";
        
    }
}
