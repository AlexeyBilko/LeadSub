using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        DbContext context;
        DbSet<T> table;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        public async void Create(T entity)
        {
            await table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public T Get(int Id)
        {
            return table.Find(Id);
        }

        public IEnumerable<T> GetAll()
        {
            return table;
        }
        public async void SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
