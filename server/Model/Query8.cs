using System;
using System.Collections.Generic;

namespace server
{
    public class Query8
    {
        
        public class Input
        {
            public long block_id {get; set;}
        }

        public string recipient {get; set;}
        public double rate {get; set;}
        public long total_satoshis {get; set;}
        public double total_amount_in_dollars {get; set;}


        // POST query8 returns JSON
        public static string query = "match (i:Input)-[:FEEDS]->(:Transaction)-[:VALIDATED_FROM]->(b:Block) where b.id = $block_id with i.recipient as recipient, sum(i.value_usd)/sum(i.value) as rate, sum(i.value) as total_satoshis, sum(i.value_usd) as total_amount_in_dollars order by rate desc limit 1 return recipient, rate, total_satoshis, total_amount_in_dollars;";
        
    }
}
