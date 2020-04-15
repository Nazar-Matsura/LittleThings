using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Infrastructure;
using LittleThingsToDo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LittleThingsToDo.Infrastructure.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity: class, IIdentifiedEntity
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetSingle(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities
                .Where(expression)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllByIds(List<Guid> ids)
        {
            return await _entities
                .Where(e => ids.Contains(e.Id))
                .ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(TEntity entity)
        {
            _entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveById(Guid id)
        {
            var entity = await GetSingle(id);
            await Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRangeOfIds(List<Guid> ids)
        {
            var entities = await GetAllByIds(ids);
            await RemoveRange(entities);
        }

        public async Task Update(TEntity entity)
        {
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.AnyAsync(expression);
        }
    }
}
