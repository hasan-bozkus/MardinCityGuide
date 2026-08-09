using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteBazaarRepository : GenericRepository<Bazaar>, IBazaarDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteBazaarRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<Bazaar>> GetBazaarsWithCategoryAsync()
        {
            await _appDatabase.InitAsync();

            var bazaars = await _connection.Table<Bazaar>().ToListAsync();

            var categories = await _connection.Table<Category>().ToListAsync();

            foreach (var item in bazaars)
            {
                var matchingCategory = categories.FirstOrDefault(c => c.CategoryId == item.CategoryId);
                if (matchingCategory is not null)
                {
                    item.Category.CategoryName = matchingCategory.CategoryName;
                }
                else
                {
                    item.Category.CategoryName = "Bilinmiyor";
                }
            }

            return bazaars;
        }
    }
}
