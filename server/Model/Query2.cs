using System;
using System.Collections.Generic;


namespace server
{
    public class Query2
    {
        public class Input
        {
            public string start {get; set;}
            public string end {get; set;}
        }
        
        public List<string> recipients {get; set;}
        public long total_value {get; set;}
        public double total_value_usd {get; set;}
        


        // POST query2 returns JSON
        public static string query = "WITH apoc.date.parse($start, 'ms', 'yyyy-MM-dd HH:mm:ss') AS start, apoc.date.parse($end, 'ms', 'yyyy-MM-dd HH:mm:ss') AS end MATCH (t:Transaction)-[:PAYS]->(o:Output) WHERE t.time >= datetime({epochmillis: start}) AND t.time <= datetime({epochmillis: end}) RETURN collect(o.recipient) AS recipients, sum(o.value) AS total_value, sum(o.value_usd) AS total_value_usd;";
        
    }
}
