﻿namespace CaloriesTracker.Domain.Abstractions.Core
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        protected AggregateRoot(T id) : base(id)
        {
        }
    }
}