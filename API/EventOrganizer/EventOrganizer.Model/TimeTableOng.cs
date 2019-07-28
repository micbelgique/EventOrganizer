using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EventOrganizer.Model
{
    public class TimeTableOng:Entity
    {
        public DateTime Time { get; set; }
        public Team SelectedTeam { get; set; }
    }
}
