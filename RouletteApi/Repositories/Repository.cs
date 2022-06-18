using Microsoft.EntityFrameworkCore;
using RouletteApi.Models.Entities;
using System.Linq.Expressions;

namespace RouletteApi.Repositories
{
    public class Repository<T> where T : Entity
    {
        protected readonly RouletteDbContext DbContext;

        public Repository(RouletteDbContext rouletteDbContext)
        {
            DbContext = rouletteDbContext;
        }

        public async Task<int> CreateAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbContext.FindAsync<T>(id);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Where(expression);
        }

        public async Task UpdateAsync(T entity)
        {
            entity.Updated = DateTime.UtcNow;

            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteIfAnyAsync(int id)
        {
            var entity = await DbContext.FindAsync<T>(id);
            if (entity is null)
                return;

            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }


        public async Task<int> CountAsync()
        {
            return await DbContext.Set<T>().CountAsync();
        }

        public async Task<bool> AnyAsync()
        {
            return await DbContext.Set<T>().AnyAsync();
        }
    }
}
