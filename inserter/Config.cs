using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace inserter
{

    public enum Element
    {
        Transaction,
        Input,
        Output,
        Block
    };

    public class Config
    {

        public static Dictionary<Element, List<string>> index = new Dictionary<Element, List<string>>();

        static Config()
        {
            string year = "2016";
            string month = "05";
            
            string filename = $"blockchair_bitcoin_transactions_${year}${month}";
            string postfix = ".tsv";

            string cwd = Directory.GetCurrentDirectory();
            string fixpath = "/../../data/";
            
            foreach (Element element in Enum.GetValues(typeof(Element)))
            {
                List<string> names = new List<string>();
                for (int z = 1; z < 32; z++)
                {
                    string name = $"{cwd}{fixpath}{getFixedFilename(getElementMapping(element), year, month, postfix, z)}";
                    names.Add(name);
                }
                index.Add(element, names);
            }
        }

        public static Tuple<string, string, string, string> settings()
        {
            return Tuple.Create("localhost", "7687", "neo4j", "test");
        }

        private static string getElementMapping(Element el)
        {
            switch (el)
            {
                case Element.Transaction:
                    return "transactions";
                case Element.Input:
                    return "inputs";
                case Element.Output:
                    return "outputs";
                case Element.Block:
                    return "blocks";
                default:
                    return "";
            }
        }

        private static string getFixedFilename(string type_, string year, string month, string postfix, int day)
        {
            return $"blockchair_bitcoin_{type_}_{year}{month}{numbering(day)}{postfix}";
        }

        private static string numbering(int i)
        {
            if (i < 10)
            {
                return "0" + i.ToString();
            }
            return i.ToString();
        }

        public static IEnumerable<Node> read(string filename, Element element)
        {
            
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                line = sr.ReadLine(); // skip first line
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split('\t');
                    
                    switch (element)
                    {
                        case Element.Transaction:
                            yield return new Transaction()
                            {
                                block_id = Int64.Parse(tokens[0]),
                                hash = tokens[1],
                                time = DateTime.ParseExact(tokens[2], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                size = Int32.Parse(tokens[3]),
                                weight = Int32.Parse(tokens[4]),
                                version = Int32.Parse(tokens[5]),
                                lock_time = Int64.Parse(tokens[6]),
                                is_coinbase = Int32.Parse(tokens[7]) == 1 ? true : false,
                                has_witness = Int32.Parse(tokens[8]) == 1 ? true : false,
                                input_count = Int32.Parse(tokens[9]),
                                output_count = Int32.Parse(tokens[10]),
                                input_total = Int64.Parse(tokens[11]),
                                input_total_usd = Convert.ToDouble(tokens[12]),
                                output_total = Int64.Parse(tokens[13]),
                                output_total_usd = Convert.ToDouble(tokens[14]),
                                fee = Convert.ToDouble(tokens[15]),
                                fee_usd = Convert.ToDouble(tokens[16]),
                                fee_per_kb = Convert.ToDouble(tokens[17]),
                                fee_per_kb_usd = Convert.ToDouble(tokens[18]),
                                fee_per_kwu = Convert.ToDouble(tokens[19]),
                                fee_per_kwu_usd = Convert.ToDouble(tokens[20]),
                                cdd_total = Convert.ToDouble(tokens[21]),
                            };
                            break;
                        case Element.Block:
                            yield return new Block()
                            {
                                id = Int64.Parse(tokens[0]),
                                hash = tokens[1],
                                time = DateTime.ParseExact(tokens[2], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                median_time = DateTime.ParseExact(tokens[3], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                size = Int32.Parse(tokens[4]),
                                stripped_size = Int32.Parse(tokens[5]),
                                weight = Int32.Parse(tokens[6]),
                                version = Int32.Parse(tokens[7]),
                                version_hex = tokens[8],
                                version_bits = tokens[9],
                                merkle_root = tokens[10],
                                nonce = Int64.Parse(tokens[11]),
                                bits = Int64.Parse(tokens[12]),
                                difficulty = Convert.ToDouble(tokens[13]),
                                chainwork = tokens[14],
                                coinbase_data_hex = tokens[15],
                                transaction_count = Int32.Parse(tokens[16]),
                                witness_count = Int32.Parse(tokens[17]),
                                input_count = Int32.Parse(tokens[18]),
                                output_count = Int32.Parse(tokens[19]),
                                input_total = Int64.Parse(tokens[20]),
                                input_total_usd = Convert.ToDouble(tokens[21]),
                                output_total = Int64.Parse(tokens[22]),
                                output_total_usd = Convert.ToDouble(tokens[23]),
                                fee_total = Convert.ToDouble(tokens[24]),
                                fee_total_usd = Convert.ToDouble(tokens[25]),
                                fee_per_kb = Convert.ToDouble(tokens[26]),
                                fee_per_kb_usd = Convert.ToDouble(tokens[27]),
                                fee_per_kwu = Convert.ToDouble(tokens[28]),
                                fee_per_kwu_usd = Convert.ToDouble(tokens[29]),
                                cdd_total = Convert.ToDouble(tokens[30]),
                                generation = Int64.Parse(tokens[31]),
                                generation_usd = Convert.ToDouble(tokens[32]),
                                reward = Int64.Parse(tokens[33]),
                                reward_usd = Convert.ToDouble(tokens[34]),
                                guessed_miner = tokens[35],
                            };
                            break;
                        case Element.Input:
                            yield return new Input()
                            {
                                block_id = Int64.Parse(tokens[0]),
                                transaction_hash = tokens[1],
                                index = Int32.Parse(tokens[2]),
                                time = DateTime.ParseExact(tokens[3], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                value = Int64.Parse(tokens[4]),
                                value_usd = Convert.ToDouble(tokens[5]),
                                recipient = tokens[6],
                                type = tokens[7],
                                script_hex = tokens[8],
                                is_from_coinbase = Int32.Parse(tokens[9]) == 1 ? true : false,
                                is_spendable = Int32.Parse(tokens[10]) == 1 ? true : false,
                                spending_block_id = Int64.Parse(tokens[11]),
                                spending_transaction_hash = tokens[12],
                                spending_index = Int32.Parse(tokens[13]),
                                spending_time = DateTime.ParseExact(tokens[14], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                spending_value_usd = Convert.ToDouble(tokens[15]),
                                spending_sequence = Int64.Parse(tokens[16]),
                                spending_signature_hex = tokens[17],
                                spending_witness = tokens[18],
                                lifespan = Int64.Parse(tokens[19]),
                                cdd = Convert.ToDouble(tokens[20]),
                            };
                            break;
                        case Element.Output:
                            yield return new Output()
                            {
                                block_id = Int64.Parse(tokens[0]),
                                transaction_hash = tokens[1],
                                index = Int32.Parse(tokens[2]),
                                time = DateTime.ParseExact(tokens[3], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                value = Int64.Parse(tokens[4]),
                                value_usd = Convert.ToDouble(tokens[5]),
                                recipient = tokens[6],
                                type = tokens[7],
                                script_hex = tokens[8],
                                is_from_coinbase = Int32.Parse(tokens[9]) == 1 ? true : false,
                                is_spendable = Int32.Parse(tokens[10]) == 1 ? true : false,
                            };
                            break;
                    }
                }
            }
        }



    }
}