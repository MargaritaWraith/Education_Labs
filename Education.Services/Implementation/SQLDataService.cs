using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.DAL.Context;
using Education.Entityes.EF.BaseEntityes;
using Education.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Education.Services.Implementation
{
    public abstract class SQLDataService<T> : IDataService<T> where T : BaseEntity
    {
        protected readonly EducationDB _db;

        protected DbSet<T> Entityes => _db.Set<T>();

        protected SQLDataService(EducationDB db) => _db = db;

        public virtual IQueryable<T> Items => Entityes;

        public async Task<T> GetByIdAsync(int id) => await Entityes.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<int> AddAsync(T item) => (await Entityes.AddAsync(item)).Entity.Id;

        public async Task<T> RemoveAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item is null) return null;
            Entityes.Remove(item);
            return item;
        }

        public abstract Task<T> EditAsync(T Value);

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
