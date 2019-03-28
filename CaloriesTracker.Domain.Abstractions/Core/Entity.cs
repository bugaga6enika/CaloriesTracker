namespace CaloriesTracker.Domain.Abstractions.Core
{
    public abstract class Entity<T>
    {
        protected Entity(T id)
        {
            Id = id;
        }

        public T Id { get; }

        public override bool Equals(object obj)
        {
            var otherId = (T)obj;

            if (ReferenceEquals(otherId, null))
            {
                return false;
            }

            if (ReferenceEquals(this, otherId))
            {
                return true;
            }

            if (GetType() != otherId.GetType())
            {
                return false;
            }

            if (Id.Equals(default(T)) || otherId.Equals(default(T)))
            {
                return false;
            }

            return Id.Equals(otherId);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
