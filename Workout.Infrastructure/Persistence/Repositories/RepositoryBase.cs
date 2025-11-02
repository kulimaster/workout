using Microsoft.EntityFrameworkCore;
using Workout.Shared.Domain;

namespace Workout.Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<T>(DbContext context) : IGenericRepository<T>
    where T : BaseEntity
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entityToDelete = await context.Set<T>()
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);

        if (entityToDelete is not null)
        {
            context.Set<T>().Remove(entityToDelete);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
