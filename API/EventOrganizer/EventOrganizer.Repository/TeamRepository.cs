using EventOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Repository
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly Context _context;

        public TeamRepository(Context context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.Include(x => x.Users);
        }

        public Team Get(long id)
        {
            return _context.Teams.FirstOrDefault(x => x.Id == id);
        }

        public Team GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(Team entity)
        {
            var verify = Get(entity.Id);
            if (verify != null)
                return false;
            try
            {
                _context.Teams.Add(entity);
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
                _context.Teams.Remove(getObject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Team entity)
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
    }
}
