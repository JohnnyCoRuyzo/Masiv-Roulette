using System;
using System.Collections.Generic;

namespace Masiv_Roulette
{
    public class Roulette
    {
        public Guid ID { get; set; }

        public bool IsRouletteOpen { get; set; }

        public int CurrentResultNumber { get; set; }

        public string CurrentResultColor { get; set; }

        public List<Bet> AllBets { get; set; }

        public const int MIN_ROULETTE_RESULT = 0;

        public const int MAX_ROULETTE_RESULT = 36;

        public const string RED_COLOR = "Rojo";

        public const string BLACK_COLOR = "Negro";

        public const string GREEN_COLOR = "Verde";

        public Roulette()
        {
            ID = Guid.NewGuid();
            IsRouletteOpen = true;
            CurrentResultColor = "";
            CurrentResultNumber = -1;
            AllBets = new List<Bet>();
        }

        public void SpinRoulette()
        {
            var randomRange = new Random();
            CurrentResultNumber = randomRange.Next(MIN_ROULETTE_RESULT, MAX_ROULETTE_RESULT);
            GetResultingColorFromSpin();
        }

        public void GetResultingColorFromSpin()
        {
            if (CurrentResultNumber == 0)
                CurrentResultColor = GREEN_COLOR;
            else if (CurrentResultNumber % 2 == 0)
                CurrentResultColor = RED_COLOR;
            else
                CurrentResultColor = BLACK_COLOR;
        }

        public bool ValidInsertBetIntoRouletteHistoryBets(Bet bet)
        {
            try
            {
                AllBets.Add(bet);
                return true;
            }
            catch (Exception e)
            {
                _ = e.ToString();
                return false;
            }
        }

        public void OpenRoulette()
        {
            IsRouletteOpen = true;
            CurrentResultColor = "";
            CurrentResultNumber = -1;
            AllBets = new List<Bet>();
        }
    }
}
