using System;
using System.Collections.Generic;
using System.Text;
using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T: class, new()
    {
        private readonly IGenericDal<T> _genericDal;

        public GenericManager(IGenericDal<T> genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<List<T>> TGetListAllAsync()
        {
            return await _genericDal.GetListAllAsync();
        }

        public async Task TCreateAsync(T t)
        {
            await _genericDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(T t)
        {
            await _genericDal.DeleteAsync(t);
        }

        public async Task TUpdateAsync(T t)
        {
            await _genericDal.UpdateAsync(t);
        }

        public async Task<T> TGetByIdAsync(int id)
        {
            return await _genericDal.GetByIdAsync(id);
        }

    }
}
