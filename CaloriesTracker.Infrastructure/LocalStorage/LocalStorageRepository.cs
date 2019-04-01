using Akavache;
using System;
using System.Reactive;

namespace CaloriesTracker.Infrastructure.LocalStorage
{
    internal abstract class LocalStorageRepository<T> where T : new()
    {
        private IBlobCache _cacheProvider => BlobCache.LocalMachine;
        private string _key;

        protected LocalStorageRepository(string key)
        {
            _key = key;
        }

        protected IObservable<T> Get()
        {
            return _cacheProvider.GetOrCreateObject(_key, () => new T());
        }

        protected IObservable<Unit> Save(T instance)
        {
            return _cacheProvider.InsertObject(_key, instance);
        }

        protected IObservable<Unit> ForseWrite()
        {
            return _cacheProvider.Flush();
        }
    }
}
