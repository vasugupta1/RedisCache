using System;
using System.Threading.Tasks;
using Redis_Cache_Services.Redis.Interface;
using StackExchange.Redis;
using System.Text.Json;
using Redis_Cache_Services.Redis.Exceptions;
using OneOf;
using Redis_Cache_Services.Models;

namespace Redis_Cache_Services.Redis
{
    public class CachingService<T> : ICachingService<T>
    {
        private readonly IDatabase _database;
        public CachingService(IDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }
        public async Task SetCacheObject(T dataObject, string key)
        {
            try
            {
                await _database.StringSetAsync(key , JsonSerializer.Serialize<T>(dataObject));  
            }
            catch(Exception ex)
            {
                throw new CachingServiceException("Failed Setting Cache", ex);
            }
        }

        public async Task<OneOf<T, NotFoundCacheObject>> GetCachedObject(string key)
        {
            try
            {
                var response = await _database.StringGetAsync(key);
                if(!string.IsNullOrEmpty(response))
                {
                    return JsonSerializer.Deserialize<T>(response);
                }
                return new NotFoundCacheObject()
                {
                    Key = key,
                    Message = "Didn't find the cache object you were looking for please check the key"
                };
            }  
            catch(Exception ex)
            {
                throw new CachingServiceException("Something went wrong when getting Cache", ex);
            }        
        }
    }
}