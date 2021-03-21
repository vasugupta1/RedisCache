using System.Threading.Tasks;
using OneOf;
using Redis_Cache_Services.Models;

namespace Redis_Cache_Services.Redis.Interface
{
    public interface ICachingService<T>
    {
         Task SetCacheObject(T dataObject, string key);
         Task<OneOf<T,NotFoundCacheObject>> GetCachedObject(string key);
    }
}