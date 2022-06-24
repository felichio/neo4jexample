using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Neo4j.Driver;

namespace inserter
{
    public class Relationships
    {
        
        IDriver _driver = Node._driver;


        public async Task<string> CreateBlockTransaction()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("Making Block<-[:VALIDATED_FROM]-Transaction relationships");
                    var reader = await tx.RunAsync("MATCH (b:Block), (t:Transaction) WHERE b.id = t.block_id CREATE (b)<-[:VALIDATED_FROM]-(t);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.RelationshipsCreated > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateInputTransaction()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("Making Transaction<-[:FEEDS]-Input relationships");
                    var reader = await tx.RunAsync("MATCH (t:Transaction), (i:Input) WHERE t.hash = i.spending_transaction_hash CREATE (t)<-[:FEEDS]-(i);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.RelationshipsCreated > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateOutputTransaction()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("Making Output<-[:PAYS]-Transaction relationships");
                    var reader = await tx.RunAsync("MATCH (t:Transaction), (o:Output) WHERE t.hash = o.transaction_hash CREATE (t)-[:PAYS]->(o);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.RelationshipsCreated > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateOutputInput()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("Making Input<-[:SPENDS]-Output relationships");
                    var reader = await tx.RunAsync("MATCH (o:Output), (i:Input) WHERE o.transaction_hash = i.transaction_hash and o.recipient = i.recipient and o.index = i.index CREATE (o)-[:SPENDS]->(i);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.RelationshipsCreated > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateGuessedMinerBlock()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("Making GuessedMiner<-[:GUESSED_FROM]-Block relationships");
                    var reader = await tx.RunAsync("MATCH (b:Block) MERGE (g:GuessedMiner {name: b.guessed_miner}) MERGE (b)-[:GUESSED_FROM]->(g);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.RelationshipsCreated > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateTransactionHashIndex()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("Create transaction hash index");
                    var reader = await tx.RunAsync("CREATE INDEX FOR (t:Transaction) ON (t.hash);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.IndexesAdded > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateTransactionDateIndex()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("create transaction time index");
                    var reader = await tx.RunAsync("CREATE INDEX FOR (t:Transaction) ON (t.time);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.IndexesAdded > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateInputHashIndex()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("create input transaction_hash index");
                    var reader = await tx.RunAsync("CREATE INDEX FOR (i:Input) ON (i.transaction_hash);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.IndexesAdded > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateOutputHashIndex()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("create output transaction_hash index");
                    var reader = await tx.RunAsync("CREATE INDEX FOR (o:Output) ON (o.transaction_hash);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.IndexesAdded > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateInputRecipientIndex()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("create input recipient index");
                    var reader = await tx.RunAsync("CREATE INDEX FOR (i:Input) ON (i.recipient);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.IndexesAdded > 0 ? "ok" : "nok";
        }

        public async Task<string> CreateOutputRecipientIndex()
        {
            var session = _driver.AsyncSession();
            IResultSummary result;
            
            try
            {
                result = await session.WriteTransactionAsync(async tx => {
                    Console.WriteLine("create output recipient index");
                    var reader = await tx.RunAsync("CREATE INDEX FOR (o:Output) ON (o.recipient);");
                    return await reader.ConsumeAsync();
                });
                
            }
            finally
            {
                await session.CloseAsync();
            }
            return result.Counters.IndexesAdded > 0 ? "ok" : "nok";
        }

    };

}