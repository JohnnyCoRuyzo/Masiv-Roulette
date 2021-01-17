using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv_Roulette
{
    public class Bet
    {
        public Guid ID { get; set; }

        public decimal BettingAmount { get; set; }

        public int BettingNumber { get; set; }

        public Guid ID_Roulette { get; set; }

        public Guid ID_User { get; set; }

        public const int MIN_BET_NUMBER = 0;

        public const int MAX_BET_NUMBER = 36;

        public const decimal MIN_BET_AMOUNT = 0;

        public const decimal MAX_BET_AMOUNT = 10000;

        public const string EVEN_NUMBERS_COLOR = "Rojo";

        public const string ODD_NUMBERS_COLOR = "Negro";

        public bool BettingAmountIsValid()
        {
            return MIN_BET_AMOUNT < BettingAmount && BettingAmount <= MAX_BET_AMOUNT;
        }

        public bool BettingNumberIsValid()
        {
            return MIN_BET_NUMBER < BettingNumber && BettingNumber <= MAX_BET_NUMBER;
        }
    }
}
