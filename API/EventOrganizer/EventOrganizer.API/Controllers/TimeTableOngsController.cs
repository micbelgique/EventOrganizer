using System.Threading.Tasks;
using EventOrganizer.Model;
using EventOrganizer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeTableOngsController : ControllerBase
    {
        private readonly Context _context;

        public TimeTableOngsController(Context context)
        {
            _context = context;
        }

        [HttpPut]
        public async Task<bool> UpdateTimeTableOng([FromBody] TimeTableOng timeTableOng)
        {
            if (timeTableOng.SelectedTeam == null) return false;
            using (IRepository<TimeTableOng> timeTableOngRepository = new TimeTableOngRepository(_context))
            using(IRepository<Team> teamRepository = new TeamRepository(_context))
            {
                var timeTableOngDb = timeTableOngRepository.Get(timeTableOng.Id);
                if (timeTableOngDb.SelectedTeam != null) return false;
                var team = teamRepository.Get(timeTableOng.SelectedTeam.Id);
                timeTableOngDb.SelectedTeam = team;
                return await timeTableOngRepository.Update(timeTableOngDb);
            }
        }
    }
}