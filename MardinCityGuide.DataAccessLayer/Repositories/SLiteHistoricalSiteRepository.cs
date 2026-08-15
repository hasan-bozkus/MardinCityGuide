using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteHistoricalSiteRepository : GenericRepository<HistoricalSite>, IHistoricalSiteDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteHistoricalSiteRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<HistoricalSite> GetFirstHistoricalSiteWithImageGaleriesAsync()
        {
            await _appDatabase.InitAsync();

            var firstHistoricalSite = await _connection.Table<HistoricalSite>().FirstAsync();

            var imageGalleries = await _connection.Table<GalleryImage>().Where(x => x.HistoricalSiteId == firstHistoricalSite.HistoricalsiteId && x.IsActive == true).OrderBy(y => y.SortOrder).Take(1).ToListAsync();

            firstHistoricalSite.Images = imageGalleries;

            return firstHistoricalSite;
        }
    }
}
