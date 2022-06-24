using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DIDapperAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        public CacheController(ILogger<MovieController> logger,IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet(Name = "cache")]
        public string Get()
        {
            var now = _cache.Get<string>("cacheNow");
            if (now == null)//如果沒有該緩存
            {
                now = DateTime.Now.ToString();
                _cache.Set("cacheNow", now);
                return now;
            }
            else
            {
                return now;
            }
        }
    }
}
