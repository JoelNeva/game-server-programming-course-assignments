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

        public PlayersController(ILogger<PlayersController> logger, FileRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("GetTest")]
        public int GetTest()
        {
            return 1;
        }


        [HttpGet("Get/{id:Guid}")]
        public Player Get(Guid id)
        {
            return _repo.Get(id).Result;
        }
        [HttpGet("GetAll")]
        public Player[] GetAll()
        {
            return _repo.GetAll().Result;
        }
        [HttpPost("Create")]
        public void Create([FromBody] NewPlayer player)
        {
            _repo.Create(player);
        }
        [HttpPost("Modify/{id:Guid}")]
        public void Modify(Guid id, [FromBody] ModifiedPlayer player)
        {
            _repo.Modify(id, player);
        }
        [HttpGet("Delete/{id:Guid}")]
        public void Delete(Guid id)
        {
            _repo.Delete(id);
        }

        public void Options() { }
    }
}