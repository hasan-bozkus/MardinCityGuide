using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using SQLite;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteCategoryRepository : GenericRepository<Category>, ICategoryDal
    {
        public SLiteCategoryRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
        }
    }
}
