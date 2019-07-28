using EventOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Repository
{
    public class TimeTableOngRepository : IRepository<TimeTableOng>
    {
        private readonly Context _context;

        public TimeTableOngRepository(Context context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public IEnumerable<TimeTableOng> GetAll()
        {
            return _context.TimeTableOngs;
        }

        public TimeTableOng Get(long id)
        {
            return _context.TimeTableOngs.Include(x => x.SelectedTeam).FirstOrDefault(x => x.Id == id);
        }

        public TimeTableOng GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(TimeTableOng entity)
        {
            var verify = Get(entity.Id);
            if (verify != null)
                return false;
            try
            {
                _context.TimeTableOngs.Add(entity);
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
                _context.TimeTableOngs.Remove(getObject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(TimeTableOng entity)
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
