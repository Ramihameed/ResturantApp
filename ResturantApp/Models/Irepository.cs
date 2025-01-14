﻿namespace ResturantApp.Models
{
    public interface Irepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id, QueryOptions<T> options);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

    }
}
