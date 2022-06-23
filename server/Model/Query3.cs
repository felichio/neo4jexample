

namespace server
{
    public class Query3
    {
        
        public int number_of_transactions {get; set;}
        public long input_total {get; set;}
        public long output_total {get; set;}
        public long fee_total {get; set;}


        // GET query3/{block_id} returns JSON
        public static string query = "MATCH (b:Block)<-[:VALIDATED_FROM]-(t:Transaction) WHERE b.id = $block_id RETURN count(t), b.input_total, b.output_total, b.fee_total;";
        
    }
}
