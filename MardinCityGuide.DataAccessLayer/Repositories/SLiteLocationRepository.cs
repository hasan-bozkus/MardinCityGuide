using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteLocationRepository : GenericRepository<EntityLayer.Concrete.Location>, ILocationDal
    {
        public SLiteLocationRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
        }
    }
}
