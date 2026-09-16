using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class, new()
    {
        Task<List<T>> GetListAllAsync();
        Task CreateAsync(T t);
        Task DeleteAsync(T t);
        Task UpdateAsync(T t);
        Task<T> GetByIdAsync(int id);
    }
}
