using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Model;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.Repository
{
    public class UserPictureRepository : IRepository<UserPicture>
    {
        private readonly Context _context;

        public UserPictureRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<UserPicture> GetAll()
        {
            return _context.UserPictures;
        }

        public UserPicture Get(long id)
        {
            return _context.UserPictures.FirstOrDefault(userPicture => userPicture.IdFromPlat == id);
        }

        public UserPicture GetById(int id)
        {
            return _context.UserPictures.FirstOrDefault(e => e.Id == id);
        }
        public async Task<bool> Insert(UserPicture entity)
        {
            var verify = Get(entity.IdFromPlat);
            if (verify != null)
                return false;
            try
            {
                _context.UserPictures.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception )
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
                _context.UserPictures.Remove(getObject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public async Task<bool> Update(UserPicture entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception )
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
