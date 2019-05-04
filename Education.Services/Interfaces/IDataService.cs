using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Entityes.EF.BaseEntityes;

namespace Education.Services.Interfaces
{
    public interface IDataService<T> where T : BaseEntity
    {
        IQueryable<T> Items { get; }

        Task<T> GetByIdAsync(int id);

        Task<int> AddAsync(T item);

        Task<T> RemoveAsync(int id);

        Task<T> EditAsync(T Value);

        Task SaveChangesAsync();
    }
}
