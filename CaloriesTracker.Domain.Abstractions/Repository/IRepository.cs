using CaloriesTracker.Domain.Abstractions.Core;
using System.Threading.Tasks;

namespace CaloriesTracker.Domain.Abstractions.Repository
{
    public interface IRepository<TModel, TKey, TFilter>
        where TModel : Entity<TKey>
        where TFilter : IFilter
    {
        Task<TModel> GetAsync(TKey key);
        Task<PagedDataSource<TModel>> GetAsync(TFilter filter);
        Task<TModel> CreateAsync(TModel model);
        Task<TModel> UpdateAsync(TKey key, TModel model);
        Task<OperationResult> DeleteAsync(TKey key);
    }
}
