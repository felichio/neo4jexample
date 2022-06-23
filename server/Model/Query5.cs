using System;
using System.Collections.Generic;

namespace server
{
    public class Query5
    {
        
        public class Input
        {
            public string day {get; set;}
            public string recipient {get; set;}
        }

        public string recipient {get; set;}
        public double total_value_usd {get; set;}
        


        // POST query5 returns JSON
        public static string query = "with date(datetime({epochmillis: apoc.date.parse($day, 'ms', 'yyyy-MM-dd')})) as start match (i:Input) where date(i.spending_time) = start and i.recipient = $recipient return i.recipient as recipient, sum(i.value_usd) as total_value_usd;";
        
    }
}
