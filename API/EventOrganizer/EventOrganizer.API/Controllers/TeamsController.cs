using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.API.Services;
using EventOrganizer.Model;
using EventOrganizer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamsController : ControllerBase
    {
        private readonly Context _context;
        public TeamsController(Context context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Team> GetAllTeams()
        {
            using (IRepository<Team> teamRepository = new TeamRepository(_context))
            {
                return teamRepository.GetAll().Select(
                    x =>
                    {
                        foreach (var user in x.Users)
                        {
                            user.Password = null;
                            user.UserTeam = null;
                        }
                        return x;
                    }).ToList();
            }
        }

        [HttpPost]
        public async Task<Team> CreateTeam([FromBody] Team team)
        {
            using (IRepository<Team> teamRepository = new TeamRepository(_context))
            {
                if (await teamRepository.Insert(team))
                    return team;
                return null;
            }
        }
        [HttpPut("{userId}")]
        public async Task<bool> UpdateTeam(long userId,[FromBody] Team team)
        {
            using (IRepository<Team> teamRepository = new TeamRepository(_context))
            using(IRepository<User> userRepository = new UserRepository(_context))
            {
                var t = teamRepository.Get(team.Id);
                var user = userRepository.Get(userId);
                if (t.Users.Contains(user))
                {
                    t.Users.Remove(user);
                }
                else
                    t.Users.Add(user);
                return await teamRepository.Update(t);
            }
        }

        [HttpDelete]
        public async Task<bool> DeleteTeam(long id)
        {
            using (IRepository<Team> teamRepository = new TeamRepository(_context))
            {
                return await teamRepository.Delete(id);
            }
        }
    }
}