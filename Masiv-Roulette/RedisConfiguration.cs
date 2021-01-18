using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv_Roulette.Models
{
    public class RedisConfiguration
    {
        public string Hostname { get; set; }

        public int Port { get; set; }

        public string ServerPassword { get; set; }

        public string Channel { get; set; }
    }
}
