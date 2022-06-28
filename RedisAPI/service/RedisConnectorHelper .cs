using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;

namespace RedisAPI.service
{
    public class RedisConnectorHelper
    {
        private readonly RedisClient _redisClient;

        public RedisConnectorHelper(RedisClient redisClient) 
        {
            _redisClient = redisClient;
        }
        public string getRedis(string key) 
        {
            var db = _redisClient.getConn().GetDatabase();

            if (string.IsNullOrEmpty(key)) 
            {
                return "查無資料";
            }
            var test = db.StringGet(key);
            return test.ToString();
        }
        public string setRedis(string key,string value)
        {
            var db = _redisClient.getConn().GetDatabase();
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                return "參數為空，請輸入正確資料";
            }
            else 
            {
                var test = db.StringSet(key, value);
                //var test = db.StringSet(key, value,TimeSpan.FromSeconds(300));
                return "設置成功";
            }
        }
    }
}
