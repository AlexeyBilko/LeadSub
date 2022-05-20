using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        void Create(T entity);
        void SaveChangesAsync();
        void SaveChanges();
        void Delete(T entity);
        void Update(T entity);
        T Get(int Id);

    }
}
