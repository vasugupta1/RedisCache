using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Redis_Cache_Api.Models.Routes;
using Redis_Cache_Services.Redis.Interface;
using Redis_Cache_Common.Models;
using Redis_Cache_Services.Redis.Exceptions;
using OneOf;
using Redis_Cache_Services.Models;

namespace Redis_Cache_Api.Controllers
{
    [ApiController]
    [Route("")]
    public class CacheMeController : ControllerBase
    {
        private readonly ILogger<CacheMeController> _logger;
        private readonly ICachingService<DataObject> _cachingService;
       
        public CacheMeController(ILogger<CacheMeController> logger, ICachingService<DataObject> cachingService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cachingService = cachingService ?? throw new ArgumentNullException(nameof(cachingService));
        }

        [HttpGet(CacheMeRoutes.GetCachedObject)]
        public async Task<IActionResult> GetCachedObject(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"Cannot be null or empty : {nameof(key)}");
            }
            try
            {
                var oneOfResponse = await _cachingService.GetCachedObject(key);

                return oneOfResponse.Match<IActionResult>(
                    DataObject => new OkObjectResult(DataObject), 
                    NotFoundCacheObject => new NotFoundObjectResult(NotFoundCacheObject));
            }
            catch(CachingServiceException csex)
            {
                _logger.LogError($"Error {nameof(CachingServiceException)}",csex);
                return StatusCode(500);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error {nameof(Exception)}",ex);
                return StatusCode(500);
            }     
        }   

        [HttpPost(CacheMeRoutes.SetCachedObject)]
        public async Task<IActionResult> SetCachedObject(string key, [FromBody] DataObject  data)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"Cannot be null or empty : {nameof(key)}");
            }
            if(data is null)
            {
                throw new ArgumentException($"Cannot be null or empty : {nameof(data)}");
            }   
            try
            {
               await _cachingService.SetCacheObject(data, key);
            }
            catch(CachingServiceException csex)
            {
                _logger.LogError($"Error {nameof(CachingServiceException)}",csex);
                return StatusCode(500);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error {nameof(Exception)}",ex);
                return StatusCode(500);
            }     
            return NoContent();
        }
    }
}
