using System;
using System.Collections.Generic;

namespace server
{
    public class Query7
    {
        
        public class Input
        {
            public string start {get; set;}
            public string end {get; set;}
            public int hops {get; set;}
            public int K {get; set;}
        }

        public string recipientA {get; set;}
        public string recipientB {get; set;}
        public double total_value_usd {get; set;}
        


        // POST query7 returns JSON
        public static Func<int, string> createParametarizedQuery = (int hops) => $"with apoc.date.parse($start, 'ms', 'yyyy-MM-dd HH:mm:ss') as start, apoc.date.parse($end, 'ms', 'yyyy-MM-dd HH:mm:ss') as end match (i:Input)-[:FEEDS]->(t:Transaction)-[*3..{hops}]->(tt:Transaction)-[:PAYS]->(o:Output) where t.time > datetime({{epochmillis: start}}) and tt.time < datetime({{epochmillis: end}}) with i.recipient as recipientA, o.recipient as recipientB, sum(i.value_usd + o.value_usd) as total order by total desc return recipientA, recipientB, total limit $K;";
    }
}
