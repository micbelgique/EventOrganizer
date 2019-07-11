using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventOrganizer.Repository
{
    public interface IRepository<T>:IDisposable
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        Task<bool> Insert(T entity);
        Task<bool> Delete(long id);
        Task<bool> Update(T entity);
    }
}
