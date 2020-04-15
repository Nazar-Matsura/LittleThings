using System;
using System.Linq.Expressions;
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

        public static Func<BaseEntity, bool> CreatedBySpec(Guid userId)
        {
            return entity => entity.CreatedBy == userId;
        }
    }
}
