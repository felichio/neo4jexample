using System;
using System.Collections.Generic;

namespace server
{
    public class Query9
    {
        
        public class Input
        {
            public int K {get; set;}
        }

        public string recipient {get; set;}
        public int consecutive_days {get; set;}


        // POST query9 returns JSON
        public static string query = "match (o:Output)-[:SPENDS]->(i:Input) with o.recipient as recipient, o.time as day order by day asc with recipient, collect(distinct date(day)) as days with recipient, reduce(acc = [], k in range(0, size(days) -2) | case WHEN (duration.inDays(days[k], days[k + 1]).days) = 1 THEN acc + 1 ELSE acc + 0 END) as seq call apoc.coll.split(seq, 0) yield value as seqs with recipient, max(size(seqs)) as longest_streak where longest_streak >=$K return recipient, longest_streak + 1 as consecutive_days;";
        
    }
}
