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

        public bool BetIsOnNumber { get; set; }

        public int BettingNumber { get; set; }

        public string BettingColor { get; set; }

        public Guid ID_Roulette { get; set; }

        public Guid ID_User { get; set; }

        public bool IsAWinningBet { get; set; }

        public const int MIN_BET_NUMBER = 0;

        public const int MAX_BET_NUMBER = 36;

        public const decimal MIN_BET_AMOUNT = 0;

        public const decimal MAX_BET_AMOUNT = 10000;

        public const string EVEN_NUMBERS_COLOR = "RED";

        public const string ODD_NUMBERS_COLOR = "NEGRO";

        public bool BettingAmountIsValid()
        {
            return MIN_BET_AMOUNT < BettingAmount && BettingAmount <= MAX_BET_AMOUNT;
        }

        public bool BettingNumberIsValid()
        {
            if (BetIsOnNumber)
                return MIN_BET_NUMBER < BettingNumber && BettingNumber <= MAX_BET_NUMBER;
            return true;
        }

        public bool BettingColorIsValid()
        {
            if (!BetIsOnNumber)
                return EVEN_NUMBERS_COLOR == BettingColor.ToUpper() || ODD_NUMBERS_COLOR == BettingColor.ToUpper();
            return true;
        }

        public bool IsValidBet()
        {
            return BettingAmountIsValid()
                && BettingNumberIsValid()
                && BettingColorIsValid();
        }

        public bool CheckIfBetWon(Roulette roullete)
        {
            IsAWinningBet = false;
            CheckIfColorBetWon(roullete.CurrentResultColor);
            CheckIfNumberBetWon(roullete.CurrentResultNumber);
            return IsAWinningBet;
        }

        public void CheckIfColorBetWon(string roulleteColorResult)
        {
            if (!BetIsOnNumber)
            {
                if (roulleteColorResult == BettingColor)
                    IsAWinningBet = true;
            }
        }

        public void CheckIfNumberBetWon(int roulleteNumberResult)
        {
            if (BetIsOnNumber)
            {
                if (roulleteNumberResult == BettingNumber)
                    IsAWinningBet = true;
            }
        }
    }
}
