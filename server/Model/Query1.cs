

namespace server
{
    public class Query1
    {
        
        public string recipient {get; set;}
        public long value {get; set;}
        public double value_usd {get; set;}


        // GET query1/{hash} returns JSON
        public static string query = "MATCH (i:Input)-[:FEEDS]->(t:Transaction) WHERE t.hash = $hash RETURN i.recipient, i.value, i.value_usd";
        
    }
}
