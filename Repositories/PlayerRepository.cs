using ApiPlayer.Models;
using Microsoft.AspNetCore.OutputCaching;
using System.Collections.Generic;
using System.Linq;

namespace ApiPlayer.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _players = new List<Player>();

        public void AddPlayer(Player player)
        {
            player.Id = _players.Count > 0 ? _players.Max(p => p.Id) + 1 : 1;
            _players.Add(player);
        }

        public void UpdatePlayer(Player player)
        { 
            var existingPlayer = _players.FirstOrDefault(p  => p.Id == player.Id);

            if (existingPlayer != null)
            { 
                existingPlayer.Name = player.Name;
                existingPlayer.Position = player.Position;
                existingPlayer.Skills = player.Skills;
            }
        }

        public void DeletePlayer(int id)
        {
            var player = _players.FirstOrDefault(p => p.Id == id);
            if (player != null)
            { 
                _players.Remove(player);
            }
        }

        public Player GetPlayer(int id)
        {
            return _players.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Player> GetPlayers() { return _players; }

        public IEnumerable<Player> GetBestPlayers(string position, string skill)
        {
            return _players
                .Where(p => p.Position == position)
                .OrderByDescending(p => p.Skills.FirstOrDefault(s => s.Name == skill)?.Value ?? 0)
                .Take(11); // Assuming a typical football team size
        }

        public IEnumerable<Player> GetFilteredPlayers(string position = null, string skillName = null, int? minSkillValue = null)
        {
            var query = _players.AsQueryable();

            if (!string.IsNullOrEmpty(position))
            { 
                query = query.Where(p => p.Position == position);
            }

            if (!string.IsNullOrEmpty(skillName))
            { 
                query = query.Where(p => p.Skills.Any(s => s.Name == skillName && (minSkillValue == null || s.Value >= minSkillValue)));
            }

            return query.ToList();
        }

        public IDictionary<string, Player> GetBestPlayersByPositionAndSkill(IDictionary<string, string> positionSkillPairs)
        { 
            var result = new Dictionary<string, Player>();

            foreach (var pair in positionSkillPairs)
            { 
                var position = pair.Key;
                var skill = pair.Value;

                var bestPlayer = _players
                    .Where(p => p.Position == position)
                    .OrderByDescending(p => p.Skills.FirstOrDefault(s => s.Name == skill)?.Value ?? 0)
                    .FirstOrDefault();

                if (bestPlayer != null)
                {
                    result[position] = bestPlayer;
                }
            }

            return result;
        }
    }
}
