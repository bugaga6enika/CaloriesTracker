using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.Abstractions.Core
{
    public class PagedDataSource<T>
    {
        public PagedDataSource(int totalCount, IEnumerable<T> data)
        {
            if (totalCount < 0)
            {
                throw new System.ArgumentOutOfRangeException($"{TotalCount} cannot be negative");
            }

            TotalCount = totalCount;
            Data = data != null ? new ReadOnlyCollection<T>(data.ToList()) : new ReadOnlyCollection<T>(new List<T>());
        }

        public int TotalCount { get; }
        public IReadOnlyCollection<T> Data { get; }
    }
}
