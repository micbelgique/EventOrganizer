using EventOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Repository
{
    public class PictureRepository : IRepository<Picture>
    {
        private readonly Context _context;
        public PictureRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<Picture> GetAll()
        {
            return _context.Pictures.Include(e => e.User);
        }

        public Picture Get(long id)
        {
            return _context.Pictures.FirstOrDefault(picture => picture.IdFromPlat == id);
        }
        public Picture GetById(int id)
        {
            return _context.Pictures.FirstOrDefault(e => e.Id == id);
        }
        public async Task<bool> Insert(Picture entity)
        {
            var verify = Get(entity.IdFromPlat);
            if (verify != null)
                return false;
            try
            {
                _context.Pictures.Add(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Delete(long id)
        {
            var getObject = Get(id);
            if (getObject == null)
                return false;
            try
            {
                _context.Pictures.Remove(getObject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Picture entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
