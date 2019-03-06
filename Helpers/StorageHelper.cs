using System;
using Hanssens.Net;

namespace EchoBotWithCounter.Helpers
{
    public class StorageHelper
    {
        private static readonly LocalStorage _storage = new LocalStorage();

        public static void Set(string key, object value)
        {
            _storage.Store(key, value);
            _storage.Persist();
            _storage.Dispose();
        }

        public static TObject Get<TObject>(string key)
        {
            var value = default(TObject);
            try
            {
                value = _storage.Get<TObject>(key);
            }
            catch (Exception)
            {
                value = default(TObject);
            }

            return value;
        }

        public static void Clear()
        {
            _storage.Destroy();
            _storage.Dispose();
        }
    }
}
