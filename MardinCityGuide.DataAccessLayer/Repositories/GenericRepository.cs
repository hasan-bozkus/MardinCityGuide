using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using SQLite;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class, new()
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public GenericRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task CreateAsync(T t)
        {
            await _appDatabase.InitAsync();
            await _connection.InsertAsync(t);
        }

        public async Task DeleteAsync(T t)
        {
            await _appDatabase.InitAsync();
            await _connection.DeleteAsync(t);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            await _appDatabase.InitAsync();
            var value = await _connection.FindAsync<T>(id);
            return value;
        }

        public async Task<List<T>> GetListAllAsync()
        {
            await _appDatabase.InitAsync();
            var values = await _connection.Table<T>().ToListAsync();
            return values;
        }

        public async Task UpdateAsync(T t)
        {
            await _appDatabase.InitAsync();
            await _connection.UpdateAsync(t);
        }
    }
}
