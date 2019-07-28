using System;
using EventOrganizer.Model;
using EventOrganizer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OngController : ControllerBase
    {
        private readonly Context _context;

        public OngController(Context context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Ong> GetAllOngs()
        {
            using (IRepository<Ong> ongRepository = new OngRepository(_context))
            {
                return ongRepository.GetAll().ToList();
            }
        }
    }
}