using System;

namespace Calendar.Core.Domain.Commons
{
    public abstract class BaseEntity
    {

    }

    public abstract class Entity<T> : BaseEntity
    {
        public T Id { get; set; }
    }
}
