using EventOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Repository
{
    public class OngRepository : IRepository<Ong>
    {
        private readonly Context _context;

        public OngRepository(Context context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public IEnumerable<Ong> GetAll()
        {
            return _context.Ongs.Include(x => x.TimeTables);
        }

        public Ong Get(long id)
        {
            return _context.Ongs.FirstOrDefault(x => x.Id == id);
        }

        public Ong GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(Ong entity)
        {
            var verify = Get(entity.Id);
            if (verify != null)
                return false;
            try
            {
                _context.Ongs.Add(entity);
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
                _context.Ongs.Remove(getObject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Ong entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
