using ApiPlayer.Models;
using ApiPlayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiPlayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpPost]
        public IActionResult AddPlayer([FromBody] Player player)
        { 
            _playerRepository.AddPlayer(player);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, [FromBody] Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _playerRepository.UpdatePlayer(player);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        { 
            _playerRepository.DeletePlayer(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(int id)
        { 
            var player = _playerRepository.GetPlayer(id);
            if (player == null)
            { 
                return NotFound();
            }
            return Ok(player);
        }

       
    }
}
