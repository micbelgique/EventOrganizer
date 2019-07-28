using EventOrganizer.Model;
using EventOrganizer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EventOrganizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OngController : ControllerBase
    {
        private readonly Context _context;

        public OngController(Context context)
        {
            _context = context;
        }

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