using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisAPI
{
    public class RedisClient
    {

        public ConnectionMultiplexer getConn() 
        {
            var conn = ConnectionMultiplexer.Connect("172.19.20.171:6379");
            ConfigurationOptions config = ConfigurationOptions.Parse("172.19.20.171:6379,allowAdmin=true");
            var conn2 = ConnectionMultiplexer.Connect(config.ToString());
            return conn2;
        }

    }
}
