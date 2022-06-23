using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4j.Driver;


namespace server


{
    public interface IBlockchainRepository
    {
        Task<List<Query1>> getQuery1(string hash);
        Task<Query2> postQuery2(string start, string end);
        Task<Query3> getQuery3(long block_id);
        Task<List<Query4>> postQuery4(string day, int N);
        Task<Query5> postQuery5(string day, string recipient);
        Task<List<Query6>> postQuery6(string start, string end, int K);
        Task<List<Query7>> postQuery7(string start, string end, int hops, int K);
        Task<Query8> postQuery8(long block_id);
        Task<List<Query9>> postQuery9(int K);
        Task<List<Query10>> postQuery10(string start, string end, int K);
        Task<Query11> postQuery11(string start, string end);
        Task<List<Query12>> postQuery12(string start, string end, int K);
    }

    public class BlockchainRepository : IBlockchainRepository
    {
        private readonly IDriver _driver;

        public BlockchainRepository(IDriver driver)
        {
            _driver = driver;
        }

        public async Task<List<Query1>> getQuery1(string hash)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new List<Query1>();
                    var reader = await tx.RunAsync(Query1.query, new {hash});

                    while (await reader.FetchAsync())
                    {
                        results.Add(new Query1() {recipient = reader.Current[0].As<string>(), value = reader.Current[1].As<long>(), value_usd = reader.Current[2].As<double>()});
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<Query2> postQuery2(string start, string end)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new Query2();
                    var reader = await tx.RunAsync(Query2.query, new {start, end});

                    while (await reader.FetchAsync())
                    {
                        results.recipients = reader.Current[0].As<List<string>>();
                        results.total_value = reader.Current[1].As<long>();
                        results.total_value_usd = reader.Current[2].As<double>();
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<Query3> getQuery3(long block_id)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new Query3();
                    var reader = await tx.RunAsync(Query3.query, new {block_id});

                    while (await reader.FetchAsync())
                    {
                        results.number_of_transactions = reader.Current[0].As<int>();
                        results.input_total = reader.Current[1].As<long>();
                        results.output_total = reader.Current[2].As<long>();
                        results.fee_total = reader.Current[3].As<long>();
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<Query4>> postQuery4(string day, int N)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new List<Query4>();
                    var reader = await tx.RunAsync(Query4.query, new {day, N});

                    while (await reader.FetchAsync())
                    {
                        results.Add(new Query4() {recipient = reader.Current[0].As<string>(), times = reader.Current[1].As<int>(), transaction_hashes = reader.Current[2].As<List<string>>()});
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<Query5> postQuery5(string day, string recipient)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new Query5();
                    var reader = await tx.RunAsync(Query5.query, new {day, recipient});

                    while (await reader.FetchAsync())
                    {
                        results.recipient = reader.Current[0].As<string>();
                        results.total_value_usd = reader.Current[1].As<double>();
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<Query6>> postQuery6(string start, string end, int K)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new List<Query6>();
                    var reader = await tx.RunAsync(Query6.query, new {start, end, K});

                    while (await reader.FetchAsync())
                    {
                        results.Add(new Query6() {recipient = reader.Current[0].As<string>(), day = reader.Current[1].As<string>(), hits = reader.Current[2].As<int>(), outputs = reader.Current[3].As<List<string>>()});
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }


        public async Task<List<Query7>> postQuery7(string start, string end, int hops, int K)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new List<Query7>();
                    var reader = await tx.RunAsync(Query7.createParametarizedQuery(hops * 3), new {start, end, K});

                    while (await reader.FetchAsync())
                    {
                        results.Add(new Query7() {recipientA = reader.Current[0].As<string>(), recipientB = reader.Current[1].As<string>(), total_value_usd = reader.Current[2].As<double>()});
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<Query8> postQuery8(long block_id)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new Query8();
                    var reader = await tx.RunAsync(Query8.query, new {block_id});

                    while (await reader.FetchAsync())
                    {
                        results.recipient = reader.Current[0].As<string>();
                        results.rate = reader.Current[1].As<double>();
                        results.total_satoshis = reader.Current[2].As<long>();
                        results.total_amount_in_dollars = reader.Current[3].As<double>();
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }


        // K >= 2
        public async Task<List<Query9>> postQuery9(int K)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new List<Query9>();
                    var reader = await tx.RunAsync(Query9.query, new {K = K - 1});

                    while (await reader.FetchAsync())
                    {
                        results.Add(new Query9() {recipient = reader.Current[0].As<string>(), consecutive_days = reader.Current[1].As<int>()});
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<Query10>> postQuery10(string start, string end, int K)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new List<Query10>();
                    var reader = await tx.RunAsync(Query10.query, new {start, end, K});

                    while (await reader.FetchAsync())
                    {
                        results.Add(new Query10() {recipient = reader.Current[0].As<string>(), day = reader.Current[1].As<string>(), average = reader.Current[2].As<double>()});
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<Query11> postQuery11(string start, string end)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new Query11();
                    var reader = await tx.RunAsync(Query11.query, new {start, end});

                    while (await reader.FetchAsync())
                    {
                        results.hash = reader.Current[0].As<string>();
                        results.recipients = reader.Current[1].As<List<string>>();
                        results.fee = reader.Current[2].As<double>();
                        results.time = reader.Current[3].As<string>();
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<Query12>> postQuery12(string start, string end, int K)
        {
            var session = _driver.AsyncSession();

            try
            {
                return await session.ReadTransactionAsync(async tx => 
                {
                    var results = new List<Query12>();
                    var reader = await tx.RunAsync(Query12.query, new {start, end, K});

                    while (await reader.FetchAsync())
                    {
                        results.Add(new Query12() {miner_name = reader.Current[0].As<string>(), total_reward = reader.Current[1].As<long>(), number_of_blocks = reader.Current[2].As<int>()});
                    }

                    return results;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }
    }


}