using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv_Roulette
{
    public class Bet
    {
        public Guid ID { get; set; }

        public double BettingAmount { get; set; }

        public bool BetIsOnNumber { get; set; }

        public int BettingNumber { get; set; }

        public string BettingColor { get; set; }

        public Guid ID_Roulette { get; set; }

        public Guid ID_User { get; set; }

        public bool IsAWinningBet { get; set; }

        public bool BetClosed { get; set; }

        public const int MIN_BET_NUMBER = 0;

        public const int MAX_BET_NUMBER = 36;
        
        public const double MIN_BET_AMOUNT = 0;

        public const double MAX_BET_AMOUNT = 10000;

        public const string EVEN_NUMBERS_COLOR = "RED";

        public const string ODD_NUMBERS_COLOR = "NEGRO";

        public const double WINNING_NUMBER_MULTIPLIER = 5;

        public const double WINNING_COLOR_MULTIPLIER = 1.8;

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

        public bool CheckIfBetWon(Roulette roulette)
        {
            IsAWinningBet = false;
            if(CheckIfOpenBetAndIfIsFromThisRoulette(roulette)) { 
                CheckIfColorBetWon(roulette.CurrentResultColor);
                CheckIfNumberBetWon(roulette.CurrentResultNumber);
            }
            return IsAWinningBet;
        }

        public bool CheckIfOpenBetAndIfIsFromThisRoulette(Roulette roullete)
        {
            return roullete.ID == ID_Roulette && !BetClosed;
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

        public double MoneyToGive()
        {
            return MoneyToGiveBecauseNumberWon() + MoneyToGiveBecauseColorWon();
        }

        public double MoneyToGiveBecauseNumberWon()
        {
            if (BetIsOnNumber)
            {
                return BettingAmount * WINNING_NUMBER_MULTIPLIER;
            }
            return 0;
        }

        public double MoneyToGiveBecauseColorWon()
        {
            if (!BetIsOnNumber)
            {
                return BettingAmount * WINNING_COLOR_MULTIPLIER;
            }
            return 0;
        }
    }
}
