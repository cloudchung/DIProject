using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace DIDapperAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly CosmosClient _client;
        private readonly IDistributedCache _cache;

        public HomeController(ILogger logger, CosmosClient client, IDistributedCache distributedCache)
        {
            _logger = logger;
            _client = client;
            _cache = distributedCache;
        }

        //[HttpGet("setvalue")]
        //public async Task<string> setvalue(string key) 
        //{
        //    var valueByte = await _distributedCache.GetAsync(key);
        //    if (valueByte == null)
        //    {
        //        await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes("value"), new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3000)));
        //        valueByte = await _distributedCache.GetAsync(key);
        //    }
        //    var valueString = Encoding.UTF8.GetString(valueByte);
        //    return valueString;
        //    //await _distributedCache.SetStringAsync(key, value);
        //    //return "success";
        //}

        //[HttpGet("getredisvalue")]
        //public async Task<string> getredisvalue(string key) 
        //{
        //    return await _distributedCache.GetStringAsync(key);
        //}

        //[HttpGet(Name = "getcache")]
        //public string Get(string key)
        //{
        //    var now = _cache.Get(key);
        //    if (now == null)//如果沒有該緩存
        //    {
        //        return "沒有這個值";
        //    }
        //    else
        //    {
        //        return now.ToString();
        //    }
        //}
    }
}
