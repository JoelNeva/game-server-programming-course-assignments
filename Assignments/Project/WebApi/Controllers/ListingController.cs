using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("HighScores")]
    public class ListingController : ControllerBase
    {

        private readonly ILogger<ListingController> _logger;
        private readonly IRepository _repo;

        public ListingController(ILogger<ListingController> logger, IRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("GetAll")]
        public async Task<Listing[]> GetAll()
        {
            return await _repo.GetAll();
        }

        [HttpPost("New")]
        public async Task<Listing> Create([FromBody] Listing newListing)
        {
            return await _repo.NewHighScore(newListing);
        }

        [HttpDelete("Delete/{id:Guid}")]
        public async Task<Listing> Delete(Guid id)
        {
            return await _repo.Delete(id);
        }
    }
}