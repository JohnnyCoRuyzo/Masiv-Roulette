using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Masiv_Roulette.CacheData
{
    public class CachingData
    {
        private const int EXPIRED_TIME_IN_SECONDS = 300;

        private readonly ConnectionMultiplexer muxer;

        private IDatabase conn;

        public Casino CurrentCasino { get; set; }

        public CachingData()
        {
            string redisConn = GetRedisConnectionString();
            this.muxer = ConnectionMultiplexer.Connect(redisConn);
            this.conn = muxer.GetDatabase();
        }

        public string GetRedisConnectionString()
        {
            DotNetEnv.Env.Load();
            DotNetEnv.Env.TraversePath().Load();
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "../../../.env";
            DotNetEnv.Env.Load(filePath);
            using (var stream = File.OpenRead(filePath))
            {
                DotNetEnv.Env.Load(stream);
            }
            return DotNetEnv.Env.GetString("REDIS_CONNECTION_STRING");
        }

        public void SetCasino()
        {
            this.conn.StringSet(
                GetRecordKey(),
                JsonSerializer.Serialize(CurrentCasino),
                GetExpiredTimeSpan()
            );
        }

        public Casino GetCasino()
        {
            var casinoSaved = this.conn.StringGet(GetRecordKey());
            if (!casinoSaved.HasValue)
            {
                CurrentCasino = InitialData.InitialData.GetDefaultCasino();
                SetCasino();
            }
            else
            {
                CurrentCasino = JsonSerializer.Deserialize<Casino>(casinoSaved.ToString());
            }
            return CurrentCasino;
        }

        public string GetRecordKey()
        {
            return "Masiv_Roulette_Register";
        }

        public TimeSpan GetExpiredTimeSpan()
        {
            return TimeSpan.FromSeconds(EXPIRED_TIME_IN_SECONDS);
        }
    }
}
