using EventOrganizer.Model;
using EventOrganizer.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly Context _context;

        public PicturesController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Picture> Get()
        {
            List<Picture> pictures;
            using (IRepository<Picture> picturesRepository = new PictureRepository(_context))
            {
                pictures = picturesRepository.GetAll().ToList();
            }

            return pictures;
        }
        [HttpGet("{id}")]
        public Picture Get(int id)
        {
            Picture picture;
            using (IRepository<Picture> pictureRepository = new PictureRepository(_context))
            {
                picture = pictureRepository.Get(id);
            }

            return picture;
        }
        [HttpPost("AddTable")]
        public async Task<bool> Add(Picture[] pictures)
        {
            using (IRepository<Picture> picturesRepository = new PictureRepository(_context))
            {
                try
                {
                    foreach (var itemPicture in pictures)
                    {
                        await picturesRepository.Insert(itemPicture);
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        [HttpPost("AddPicture")]
        public async Task<bool> Add(Picture picture)
        {
            bool result;
            using (IRepository<Picture> picturesRepository = new PictureRepository(_context))
            {
                result = await picturesRepository.Insert(picture);
            }

            return result;
        }
        [HttpPut("{id}")]
        public async Task<bool> BanPicture(int id)
        {
            using (IRepository<Picture> picturesRepository = new PictureRepository(_context))
            {
                var pic = picturesRepository.GetById(id);
                if (pic == null)
                    return false;
                pic.Removed = true;
                return await picturesRepository.Update(pic);
            }
        }
    }
}