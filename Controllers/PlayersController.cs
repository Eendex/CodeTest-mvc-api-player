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

        [HttpGet]
        public IActionResult GetPlayers()
        { 
            var players = _playerRepository.GetPlayers();
            return Ok(players);
        }

        [HttpGet("best")]
        public IActionResult GetBestPlayers([FromQuery] string position, [FromQuery] string skill)
        { 
            var players = _playerRepository.GetBestPlayers(position, skill);
            return Ok(players);
        }

        [HttpGet("filtered")]
        public IActionResult GetFilteredPlayers([FromQuery] string position, [FromQuery] string skillName, [FromQuery] int? minSkillValue)
        {
            var players = _playerRepository.GetFilteredPlayers(position, skillName, minSkillValue);
            return Ok(players);
        }

        // Changes based on rules given
        [HttpGet("best-by-position-and-skill")]
        public IActionResult GetBestPlayersByPositionAndSkill([FromQuery] Dictionary<string, string> positionSkillPairs)
        { 
            var players = _playerRepository.GetBestPlayersByPositionAndSkill(positionSkillPairs);
            return Ok(players);
        }

       
    }
}
