using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4j.Driver;

namespace inserter
{
    public class Block : Node
    {
        public long id {get; set;}
        public string hash {get; set;}
        public DateTime time {get; set;}
        public DateTime median_time {get; set;}
        public int size {get; set;}
        public int stripped_size {get; set;}
        public int weight {get; set;}
        public int version {get; set;}
        public string version_hex {get; set;}
        public string version_bits {get; set;}
        public string merkle_root {get; set;}
        public long nonce {get; set;}
        public long bits {get; set;}
        public double difficulty {get; set;}
        public string chainwork {get; set;}
        public string coinbase_data_hex {get; set;}
        public int transaction_count {get; set;}
        public int witness_count {get; set;}
        public int input_count {get; set;}
        public int output_count {get; set;}
        public long input_total {get; set;}
        public double input_total_usd {get; set;}
        public long output_total {get; set;}
        public double output_total_usd {get; set;}
        public double fee_total {get; set;}
        public double fee_total_usd {get; set;}
        public double fee_per_kb {get; set;}
        public double fee_per_kb_usd {get; set;}
        public double fee_per_kwu {get; set;}
        public double fee_per_kwu_usd {get; set;}
        public double cdd_total {get; set;}
        public long generation {get; set;}
        public double generation_usd {get; set;}
        public long reward {get; set;}
        public double reward_usd {get; set;}
        public string guessed_miner {get; set;}
        


        public Block()
        {

        }

        public override async Task<string> CreateNode()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    var reader = await tx.RunAsync("WITH apoc.date.parse($time, 'ms', 'yyyy-MM-dd HH:mm:ss') as ms, apoc.date.parse($median_time, 'ms', 'yyyy-MM-dd HH:mm:ss') as ms2 CREATE (b:Block {id: $id, hash: $hash, size: $size, stripped_size: $stripped_size, weight: $weight, version: $version, version_hex: $version_hex, version_bits: $version_bits, merkle_root: $merkle_root, nonce: $nonce, bits: $bits, difficulty: $difficulty, chainwork: $chainwork, coinbase_data_hex: $coinbase_data_hex, transaction_count: $transaction_count, witness_count: $witness_count, input_count: $input_count, output_count: $output_count, input_total: $input_total, input_total_usd: $input_total_usd, output_total: $output_total, output_total_usd: $output_total_usd, fee_total: $fee_total, fee_total_usd: $fee_total_usd, fee_per_kb: $fee_per_kb, fee_per_kb_usd: $fee_per_kb_usd, cdd_total: $cdd_total, generation: $generation, generation_usd: $generation_usd, reward: $reward, reward_usd: $reward_usd, guessed_miner: $guessed_miner}) SET b.time = datetime({epochmillis: ms}), b.median_time = datetime({epochmillis: ms2})",
                        new {time = time.ToString("yyyy-MM-dd HH:mm:ss"), median_time = median_time.ToString("yyyy-MM-dd HH:mm:ss"), id, hash, size, stripped_size, weight, version, version_hex, version_bits, merkle_root, nonce, bits, difficulty, chainwork, coinbase_data_hex, transaction_count, witness_count, input_count, output_count, input_total, input_total_usd, output_total, output_total_usd, fee_total, fee_total_usd, fee_per_kb, fee_per_kb_usd, fee_per_kwu, fee_per_kwu_usd, cdd_total, generation, generation_usd, reward, reward_usd, guessed_miner});
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
                    id: {id}
                    hash: {hash}
                    time: {time.ToString("yyyy-MM-dd HH:mm:ss")}
                    median_time: {median_time.ToString("yyyy-MM-dd HH:mm:ss")}
                    size: {size}
                    stripped_size: {stripped_size}
                    weight: {weight}
                    version: {version}
                    version_hex: {version_hex}
                    version_bits: {version_bits}
                    merkle_root: {merkle_root}
                    nonce: {nonce}
                    bits: {bits}
                    difficulty: {difficulty}
                    chainwork: {chainwork}
                    coinbase_data_hex: {coinbase_data_hex}
                    transaction_count: {transaction_count}
                    witness_count: {witness_count}
                    input_count: {input_count}
                    output_count: {output_count}
                    input_total: {input_total}
                    input_total_usd: {input_total_usd}
                    output_total: {output_total}
                    output_total_usd: {output_total_usd}
                    fee_total: {fee_total}
                    fee_total_usd: {fee_total_usd}
                    fee_per_kb: {fee_per_kb}
                    fee_per_kb_usd: {fee_per_kb_usd}
                    fee_per_kwu: {fee_per_kwu}
                    fee_per_kwu_usd: {fee_per_kwu_usd}
                    cdd_total: {cdd_total}
                    generation: {generation}
                    generation_usd: {generation_usd}
                    reward: {reward}
                    reward_usd: {reward_usd}
                    guessed_miner: {guessed_miner}
                    ";
        }

    };


}