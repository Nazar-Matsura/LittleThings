using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LittleThingsToDo.Domain.Interfaces;

namespace LittleThingsToDo.Application.Interfaces.Infrastructure
{
    public interface IRepository<TEntity> 
        where TEntity : class, IIdentifiedEntity
    {
        Task<TEntity> GetSingle(Guid id);

        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> GetAllByIds(List<Guid> ids);

        Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        Task Remove(TEntity entity);

        Task RemoveById(Guid id);

        Task RemoveRange(IEnumerable<TEntity> entities);

        Task RemoveRangeOfIds(List<Guid> ids);

        Task Update(TEntity entity);

        Task<bool> Any(Expression<Func<TEntity, bool>> expression);
    }
}