using System;
using LittleThingsToDo.Domain.Common;
using LittleThingsToDo.Domain.Interfaces;

namespace LittleThingsToDo.Domain.Entities
{
    public abstract class BaseEntity : IIdentifiedEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public static Spec<TEntity> CreatedBySpec<TEntity>(Guid userId)
            where TEntity : BaseEntity
        {
            return new Spec<TEntity>(entity => entity.CreatedBy == userId);
        }
    }
}
