﻿

namespace ElectricCarStore_DAL.IRepository
{
    // IRepository.cs
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DisableAsync(int id);
    }

}
