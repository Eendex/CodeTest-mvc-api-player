using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPlayer.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        private List<Skill> _skills;
        public List<Skill> Skills 
        {
            get => _skills;
            set
            { 
                if (value == null || !value.Any())
                    throw new ArgumentException("Player must have at least one skill.");
                _skills = value;
            }
        }
    }
}
