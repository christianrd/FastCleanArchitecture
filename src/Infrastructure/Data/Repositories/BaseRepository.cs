using FastCleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace FastCleanArchitecture.Infrastructure.Data.Repositories;

internal abstract class BaseRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext Context;

    protected BaseRepository(ApplicationDbContext context) => Context = context;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public void Add(T entity) => Context.Add(entity);
}