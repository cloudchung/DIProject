using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisAPI.service;
using StackExchange.Redis;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RedisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly RedisConnectorHelper _redisConnectorHelper;

        public RedisController(RedisConnectorHelper redisConnectorHelper)
        {
            _redisConnectorHelper = redisConnectorHelper;
        }

        [HttpGet]
        [Route("GetRedis")]
        public string Get(string key)
        {
            return _redisConnectorHelper.getRedis(key);
        }
        [HttpPost]
        [Route("SetRedis")]
        public string Set(string key,string value)
        {
            return _redisConnectorHelper.setRedis(key,value);
        }
    }
}
