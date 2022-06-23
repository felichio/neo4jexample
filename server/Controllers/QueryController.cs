using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;

namespace server.Controllers
{
    [ApiController]
    [Route("")]
    public class QueryController : ControllerBase
    {
        private readonly IBlockchainRepository _bcRepo;
        private readonly ILogger<QueryController> _logger;

        public QueryController(ILogger<QueryController> logger, IBlockchainRepository bcRepo)
        {
            _bcRepo = bcRepo;
            _logger = logger;
        }

        [HttpGet("query1/{hash}")]
        public async Task<JsonResult> GetQuery1(string hash)
        {   
            return new JsonResult(await _bcRepo.getQuery1(hash));
        }

        [HttpPost("query2")]
        public async Task<JsonResult> PostQuery2(Query2.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery2(input.start, input.end));
        }

        [HttpGet("query3/{block_id}")]
        public async Task<JsonResult> GetQuery3(long block_id)
        {   
            return new JsonResult(await _bcRepo.getQuery3(block_id));
        }

        [HttpPost("query4")]
        public async Task<JsonResult> PostQuery4(Query4.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery4(input.day, input.N));
        }

        [HttpPost("query5")]
        public async Task<JsonResult> PostQuery5(Query5.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery5(input.day, input.recipient));
        }

        [HttpPost("query6")]
        public async Task<JsonResult> PostQuery6(Query6.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery6(input.start, input.end, input.K));
        }

        [HttpPost("query7")]
        public async Task<JsonResult> PostQuery7(Query7.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery7(input.start, input.end, input.hops, input.K));
        }

        [HttpPost("query8")]
        public async Task<JsonResult> PostQuery8(Query8.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery8(input.block_id));
        }

        [HttpPost("query9")]
        public async Task<JsonResult> PostQuery9(Query9.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery9(input.K));
        }

        [HttpPost("query10")]
        public async Task<JsonResult> PostQuery10(Query10.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery10(input.start, input.end, input.K));
        }

        [HttpPost("query11")]
        public async Task<JsonResult> PostQuery11(Query11.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery11(input.start, input.end));
        }

        [HttpPost("query12")]
        public async Task<JsonResult> PostQuery12(Query12.Input input)
        {   
            return new JsonResult(await _bcRepo.postQuery12(input.start, input.end, input.K));
        }
    }
}
