using ApiPlayer.Models;
using System.Collections.Generic;

namespace ApiPlayer.Repositories
{
    public interface IPlayerRepository
    {
        void AddPlayer(Player player);
        void UpdatePlayer(Player player);
        void DeletePlayer(int id);
        Player GetPlayer(int id);
        IEnumerable<Player> GetPlayers();
        IEnumerable<Player> GetBestPlayers(string position, string skill);
        IEnumerable<Player> GetFilteredPlayers(string position = null, string skillName = null, int? minSkillValue = null);
    }
}
