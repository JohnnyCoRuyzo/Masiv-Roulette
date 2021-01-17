using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv_Roulette
{
    public class Casino
    {
        public Guid ID { get; set; }

        public List<Roulette> AllRoulletes { get; set; }

        public const int LOT_DEFAULT_SIZE = 5;

        public Casino()
        {
            ID = Guid.NewGuid();
            AllRoulletes = CreateCasinoRoulettesByLot(LOT_DEFAULT_SIZE);
        }

        public List<Roulette> CreateCasinoRoulettesByLot(int lotSize)
        {
            var rng = new Random();
            AllRoulletes = Enumerable.Range(1, lotSize).Select(index => new Roulette()).ToList();
            return AllRoulletes;
        }

        public Roulette GetRouletteById(Guid Id)
        {
            return AllRoulletes.Where(roulette => roulette.ID == Id).FirstOrDefault();
        }

        public bool RoulleteExists(Guid Id)
        {
            return AllRoulletes.Where(roulette => roulette.ID == Id).Count() > 0;
        }
    }
}
