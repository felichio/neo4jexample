using System;
using System.Collections.Generic;

namespace server
{
    public class Query12
    {
        
        public class Input
        {
            public string start {get; set;}
            public string end {get; set;}
            public int K {get; set;}
        }

        public string miner_name {get; set;}
        public long total_reward {get; set;}
        public int number_of_blocks {get; set;}

        // POST query12 returns JSON
        public static string query = "with apoc.date.parse($start, 'ms', 'yyyy-MM-dd HH:mm:ss') as start, apoc.date.parse($end, 'ms', 'yyyy-MM-dd HH:mm:ss') as end match (b:Block)-[:GUESSED_FROM]->(g:GuessedMiner) where b.time > datetime({epochmillis: start}) and b.time <= datetime({epochmillis: end}) with sum(b.reward) as total_reward, count(b) as number_of_blocks, g.name as miner_name order by total_reward desc limit $K return miner_name, total_reward, number_of_blocks;";
        
    }
}
