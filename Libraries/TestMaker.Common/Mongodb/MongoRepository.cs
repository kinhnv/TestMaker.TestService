using MongoDB.Driver;

namespace TestMaker.Common.Mongodb
{
    public class MongoRepository<T> : IMongoRepository<T> where T : MongoEntity
    {
        protected readonly IMongoDatabase Database;
        protected readonly IMongoCollection<T> DbSet;

        protected MongoRepository(IMongoContext context)
        {
            Database = context.Database;
            DbSet = Database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<int> CountAsync()
        {
            var count = await DbSet.CountDocumentsAsync(Builders<T>.Filter.Empty);
            return (int)count;
        }

        public async Task<int> CountAsync(FilterDefinition<T> filter)
        {
            var count = await DbSet.CountDocumentsAsync(filter);
            return (int)count;
        }

        public async Task CreateAsync(T entity)
        {
            await DbSet.InsertOneAsync(entity);
        }

        public async Task CreateAsync(List<T> entities)
        {
            await DbSet.InsertManyAsync(entities);
        }

        public async Task DeleteAsync(string id)
        {
            await DbSet.DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, id));
        }

        public async Task DeleteAsync(FilterDefinition<T> filter)
        {
            await DbSet.DeleteOneAsync(filter);
        }

        public async Task<List<T>> GetAsync()
        {
            var all = await DbSet.FindAsync(Builders<T>.Filter.Empty);
            return all.ToList();
        }

        public async Task<T?> GetAsync(string id)
        {
            var query = await DbSet.FindAsync(Builders<T>.Filter.Eq(x => x.Id, id));
            return await query.SingleOrDefaultAsync();
        }

        public async Task<List<T>> GetAsync(FilterDefinition<T> filter)
        {
            var query = await DbSet.FindAsync(filter);
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await DbSet.ReplaceOneAsync(Builders<T>.Filter.Eq(x => x.Id, entity.Id), entity);
        }

        public async Task UpdateAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                await DbSet.ReplaceOneAsync(Builders<T>.Filter.Eq(x => x.Id, entity.Id), entity);
            }
        }
    }
}
