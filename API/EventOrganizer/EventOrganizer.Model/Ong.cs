using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.Model
{
    public class Ong:Entity
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public List<TimeTableOng> TimeTables { get; set; }
    }
}
