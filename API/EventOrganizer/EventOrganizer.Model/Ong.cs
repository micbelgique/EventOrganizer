using System.Collections.Generic;

namespace EventOrganizer.Model
{
    public class Ong : Entity
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public List<TimeTableOng> TimeTables { get; set; }
    }
}
