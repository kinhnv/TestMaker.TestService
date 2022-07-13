using MongoDB.Driver;

namespace TestMaker.Common.Mongodb
{
    public interface IMongoRepository<T> where T : MongoEntity
    {
        Task<List<T>> GetAsync();

        Task<List<T>> GetAsync(FilterDefinition<T> filter);

        Task<int> CountAsync();

        Task<int> CountAsync(FilterDefinition<T> filter);

        Task<T?> GetAsync(string id);

        Task CreateAsync(T entity);

        Task CreateAsync(List<T> entities);

        Task UpdateAsync(T entity);

        Task UpdateAsync(List<T> entities);

        Task DeleteAsync(string id);

        Task DeleteAsync(FilterDefinition<T> filter);
    }
}
