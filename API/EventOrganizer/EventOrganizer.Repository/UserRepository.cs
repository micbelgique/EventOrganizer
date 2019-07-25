using EventOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.UserTeam);
        }

        public User Get(long id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(User entity)
        {
            var verify = Get(entity.Id);
            if (verify != null)
                return false;
            try
            {
                _context.Users.Add(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
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
                _context.Users.Remove(getObject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(User entity)
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
