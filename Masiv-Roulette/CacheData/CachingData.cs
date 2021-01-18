using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            this.muxer = ConnectionMultiplexer.Connect("redis-13730.c241.us-east-1-4.ec2.cloud.redislabs.com:13730,password=fRJChLU99wCykjK5r29DFPQ2qoh7WrQd");
            this.conn = muxer.GetDatabase();
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
