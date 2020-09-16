using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IRepository _repo;

        public PlayersController(ILogger<PlayersController> logger, IRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("Get/{id:Guid}")]
        public async Task<Player> Get(Guid id)
        {
            return await _repo.Get(id);
        }

        [HttpGet("GetAll")]
        public async Task<Player[]> GetAll()
        {
            return await _repo.GetAll();
        }
        [HttpPost("Create")]
        public async Task<Player> Create([FromBody] NewPlayer player)
        {
            Player newPlayer = new Player
            {
                Id = Guid.NewGuid(),
                Name = player.Name,
                CreationTime = DateTime.UtcNow
            };
            return await _repo.Create(newPlayer);
        }
        [HttpPost("Modify/{id:Guid}")]
        public async Task<Player> Modify(Guid id, [FromBody] ModifiedPlayer player)
        {
            return await _repo.Modify(id, player);
        }
        [HttpGet("Delete/{id:Guid}")]
        public async Task<Player> Delete(Guid id)
        {
            return await _repo.Delete(id);
        }

        public void Options() { }
    }
}