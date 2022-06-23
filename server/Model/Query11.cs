using System;
using System.Collections.Generic;

namespace server
{
    public class Query11
    {
        
        public class Input
        {
            public string start {get; set;}
            public string end {get; set;}
        }

        public string hash {get; set;}
        public List<string> recipients {get; set;}
        public double fee {get; set;}
        public string time {get; set;}


        // POST query11 returns JSON
        public static string query = "with apoc.date.parse($start, 'ms', 'yyyy-MM-dd HH:mm:ss') as start, apoc.date.parse($end, 'ms', 'yyyy-MM-dd HH:mm:ss') as end match (i:Input)-[:FEEDS]->(t:Transaction)-[:PAYS]->(o:Output) where t.time > datetime({epochmillis: start}) and t.time <= datetime({epochmillis: end}) with collect(distinct i.recipient) + collect(distinct o.recipient) as recipients, t with recipients, t.hash as hash, t.fee as fee, t.time as time, t.weight as weight order by weight desc limit 1 return hash, recipients, fee, time;";
        
    }
}
