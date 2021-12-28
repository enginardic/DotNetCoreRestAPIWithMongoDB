using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace Domain
{
    public abstract class DomainServiceBase : IDomainService
    {

        public IDatabase CacheRepo { get; set; } = Application.Instance.Redis.GetDatabase(0);

        public T GetCacheObject<T>(string key)
        {
            if (CacheRepo.KeyExists(key))
            {
                string value = CacheRepo.StringGet(key);
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default(T);
        }

        public void SetCacheObject<T>(string key, T value, int timeoutMin = 0)
        {
            string stringValue = JsonConvert.SerializeObject(value);
            CacheRepo.StringSet(new RedisKey(key), new RedisValue(stringValue), new TimeSpan(0, 0, timeoutMin, 0));
        }
    }
}
