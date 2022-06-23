using System;
using System.Collections.Generic;

namespace server
{
    public class Query10
    {
        
        public class Input
        {
            public string start {get; set;}
            public string end {get; set;}
            public int K {get; set;}
        }

        public string recipient {get; set;}
        public string day {get; set;}
        public double average {get; set;}


        // POST query10 returns JSON
        public static string query = "with date(datetime({epochmillis: apoc.date.parse($start, 'ms', 'yyyy-MM-dd')})) as start, date(datetime({epochmillis: apoc.date.parse($end, 'ms', 'yyyy-MM-dd')})) as end match (o:Output) where date(o.time) >= start and date(o.time) <= end with o.recipient as recipient, date(o.time) as day, avg(o.value_usd) as average order by average desc limit $K return recipient, day, average;";
        
    }
}
