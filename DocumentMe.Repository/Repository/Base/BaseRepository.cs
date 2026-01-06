using DocumentMe.DataAccessLayer.Database;

namespace DocumentMe.Repository.Repository.Base
{
    public class BaseRepository
    {
        protected readonly DocumentMeDBContext _context;
        //private readonly DbSet<T> _dbSet;

        public BaseRepository(DocumentMeDBContext context)
        {
            _context = context;
            //_dbSet = context.Set<T>();
        }

        //public async Task<T> AddAsync(T entity)
        //{
        //    entity.CreatedAt = DateTime.UtcNow;
        //    entity.UpdatedAt = null;
        //    await _dbSet.AddAsync(entity);
        //    await _context.SaveChangesAsync();
        //    return entity;
        //}

        //public async Task<T> UpdateAsync(T entity)
        //{
        //    entity.CreatedAt = DateTime.SpecifyKind(entity.CreatedAt, DateTimeKind.Utc);
        //    entity.UpdatedAt = DateTime.UtcNow;

        //    _dbSet.Update(entity);
        //    await _context.SaveChangesAsync();
        //    return entity;
        //}
    }
}
