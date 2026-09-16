using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class, new()
    {
        Task<List<T>> TGetListAllAsync();
        Task TCreateAsync(T t);
        Task TDeleteAsync(T t);
        Task TUpdateAsync(T t);
        Task<T> TGetByIdAsync(int id);
    }
}
