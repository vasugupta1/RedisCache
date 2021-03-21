using System;

namespace Redis_Cache_Services.Redis.Exceptions
{
    public class CachingServiceException : Exception
    {
        public CachingServiceException(string message, Exception innerException): base(message, innerException)
        {

        }
    }
}