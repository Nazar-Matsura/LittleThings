using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LittleThingsToDo.Domain.Interfaces;

namespace LittleThingsToDo.Application.Interfaces.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : IIdentifiedEntity
    {
        TEntity GetSingle(Guid id);

        TEntity GetAll(Expression<Func<TEntity, bool>> expression);

        TEntity GetAllByIds(List<Guid> ids);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveById(Guid id);

        void RemoveRange(IEnumerable<TEntity> entities);

        void RemoveRangeOfIds(List<Guid> ids);

        void Update(TEntity entity);
    }
}