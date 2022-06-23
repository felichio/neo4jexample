using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4j.Driver;

namespace inserter
{
    public class Input : Node
    {
        public long block_id {get; set;}
        public string transaction_hash {get; set;}
        public int index {get; set;}
        public DateTime time {get; set;}
        public long value {get; set;}
        public double value_usd {get; set;}
        public string recipient {get; set;}
        public string type {get; set;}
        public string script_hex {get; set;}
        public bool is_from_coinbase {get; set;}
        public bool is_spendable {get; set;}
        public long spending_block_id {get; set;}
        public string spending_transaction_hash {get; set;}
        public int spending_index {get; set;}
        public DateTime spending_time {get; set;}
        public double spending_value_usd {get; set;}
        public long spending_sequence {get; set;}
        public string spending_signature_hex {get; set;}
        public string spending_witness {get; set;}
        public long lifespan {get; set;}
        public double cdd {get; set;}

       
        public Input()
        {

        }

        public override async Task<string> CreateNode()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    var reader = await tx.RunAsync("WITH apoc.date.parse($time, 'ms', 'yyyy-MM-dd HH:mm:ss') as ms, apoc.date.parse($spending_time, 'ms', 'yyyy-MM-dd HH:mm:ss') as ms2 CREATE (i:Input {block_id: $block_id, transaction_hash: $transaction_hash, index: $index, value: $value, value_usd: $value_usd, recipient: $recipient, type: $type, script_hex: $script_hex, is_from_coinbase: $is_from_coinbase, is_spendable: $is_spendable, spending_block_id: $spending_block_id, spending_transaction_hash: $spending_transaction_hash, spending_index: $spending_index, spending_value_usd: $spending_value_usd, spending_sequence: $spending_sequence, spending_signature_hex: $spending_signature_hex, spending_witness: $spending_witness, lifespan: $lifespan, cdd: $cdd}) SET i.time = datetime({epochmillis: ms}), i.spending_time = datetime({epochmillis: ms2})",
                        new {time = time.ToString("yyyy-MM-dd HH:mm:ss"), spending_time = spending_time.ToString("yyyy-MM-dd HH:mm:ss"),  block_id, transaction_hash, index, value, value_usd, recipient, type, script_hex, is_from_coinbase, is_spendable, spending_block_id, spending_transaction_hash, spending_index, spending_value_usd, spending_sequence, spending_signature_hex, spending_witness, lifespan, cdd});
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.NodesCreated > 0 ? "ok" : "nok";
        }

        public override string ToString()
        {
            return $@"
                    block_id: {block_id}
                    transaction_hash: {transaction_hash}
                    index: {index}
                    time: {time.ToString("yyyy-MM-dd HH:mm:ss")}
                    value: {value}
                    value_usd: {value_usd}
                    recipient: {recipient}
                    type: {type}
                    script_hex: {script_hex}
                    is_from_coinbase: {is_from_coinbase}
                    is_spendable: {is_spendable}
                    spending_block_id: {spending_block_id}
                    spending_transaction_hash: {spending_transaction_hash}
                    spending_index: {spending_index}
                    spending_time: {spending_time.ToString("yyyy-MM-dd HH:mm:ss")}
                    spending_value_usd: {spending_value_usd}
                    spending_sequence: {spending_sequence}
                    spending_signature_hex: {spending_signature_hex}
                    spending_witness: {spending_witness}
                    lifespan: {lifespan}
                    cdd: {cdd}
                    ";
        }

    };


}