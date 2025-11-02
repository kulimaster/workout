namespace Workout.Shared.Domain;

public interface IGenericRepository<T>
    where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
