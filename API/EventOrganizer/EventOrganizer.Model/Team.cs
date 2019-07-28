using System.Collections.Generic;

namespace EventOrganizer.Model
{
    public class Team : Entity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
