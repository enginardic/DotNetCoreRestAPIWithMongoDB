using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Application
    {
        public static Application Instance { get; set; }

        public IConfiguration Configuration { get; set; }

        public ConnectionMultiplexer Redis { get; set; }

        public void Load()
        {
            Data.ConnectionSettings.Instance = new Data.ConnectionSettings
            {
                ConnectionString = Configuration.GetConnectionString("Default")
            }; 

            //Redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
            //{
            //    EndPoints = { { Configuration.GetSection("Redis")["Endpoint"] } },
            //    Password = Configuration.GetSection("Redis")["Password"]
            //});
            //var server = Redis.GetServer(Redis.GetEndPoints().First());
        }
    }
}
