namespace Redis_Cache_Api.Models.Routes
{
    public class CacheMeRoutes
    {
        public const string GetCachedObject = "cache/get/{key}";
        public const string SetCachedObject = "cache/set/{key}";
    }
}