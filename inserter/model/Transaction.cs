using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4j.Driver;

namespace inserter
{
    public class Transaction : Node
    {
        public long block_id {get; set;}
        public string hash {get; set;}
        public DateTime time {get; set;}
        public int size {get; set;}
        public int weight {get; set;}
        public int version {get; set;}
        public long lock_time {get; set;}
        public bool is_coinbase {get; set;}
        public bool has_witness {get; set;}
        public int input_count {get; set;}
        public int output_count {get; set;}
        public long input_total {get; set;}
        public double input_total_usd {get; set;}
        public long output_total {get; set;}
        public double output_total_usd {get; set;}
        public double fee {get; set;}
        public double fee_usd {get; set;}
        public double fee_per_kb {get; set;}
        public double fee_per_kb_usd {get; set;}
        public double fee_per_kwu {get; set;}
        public double fee_per_kwu_usd {get; set;}
        public double cdd_total {get; set;}


        public Transaction()
        {

        }

        public override async Task<string> CreateNode()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    var reader = await tx.RunAsync("WITH apoc.date.parse($time, 'ms', 'yyyy-MM-dd HH:mm:ss') as ms CREATE (t:Transaction {block_id: $block_id, hash: $hash, size: $size, weight: $weight, version: $version, lock_time: $lock_time, is_coinbase: $is_coinbase, has_witness: $has_witness, input_count: $input_count, output_count: $output_count, input_total: $input_total, input_total_usd: $input_total_usd, output_total: $output_total, output_total_usd: $output_total_usd, fee: $fee, fee_usd: $fee_usd, fee_per_kb: $fee_per_kb, fee_per_kb_usd: $fee_per_kb_usd, fee_per_kwu: $fee_per_kwu, fee_per_kwu_usd: $fee_per_kwu_usd, cdd_total: $cdd_total}) SET t.time = datetime({epochmillis: ms})",
                        new {time = time.ToString("yyyy-MM-dd HH:mm:ss"), block_id, hash, size, weight, version, lock_time, is_coinbase, has_witness, input_count, output_count, input_total, input_total_usd, output_total, output_total_usd, fee, fee_usd, fee_per_kb, fee_per_kb_usd, fee_per_kwu, fee_per_kwu_usd, cdd_total});
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
                    hash: {hash}
                    time: {time.ToString("yyyy-MM-dd HH:mm:ss")}
                    size: {size}
                    weight: {weight}
                    version: {version}
                    lock_time: {lock_time}
                    is_coinbase: {is_coinbase}
                    has_witness: {has_witness}
                    input_count: {input_count}
                    output_count: {output_count}
                    input_total: {input_total}
                    input_total_usd: {input_total_usd}
                    output_total: {output_total}
                    output_total_usd: {output_total_usd}
                    fee: {fee}
                    fee_usd: {fee_usd}
                    fee_per_kb: {fee_per_kb}
                    fee_per_kb_usd: {fee_per_kb_usd}
                    fee_per_kwu: {fee_per_kwu}
                    fee_per_kwu_usd: {fee_per_kwu_usd}
                    cdd_total: {cdd_total}
                    ";
        }

    };


}