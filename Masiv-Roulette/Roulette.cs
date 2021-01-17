using System;

namespace Masiv_Roulette
{
    public class Roulette
    {
        public Guid ID { get; set; }

        public bool IsRouletteOpen { get; set; }

        public int ResultNumber { get; set; }

        public string ResultColor { get; set; }

        public const int MIN_ROULETTE_RESULT = 0;

        public const int MAX_ROULETTE_RESULT = 36;

        public const string RED_COLOR = "Rojo";

        public const string BLACK_COLOR = "Negro";

        public const string GREEN_COLOR = "Verde";

        public Roulette()
        {
            ID = Guid.NewGuid();
            IsRouletteOpen = true;
            ResultColor = "";
            ResultNumber = -1;
        }

        public void SpinRoulette()
        {
            var randomRange = new Random();
            ResultNumber = randomRange.Next(MIN_ROULETTE_RESULT, MAX_ROULETTE_RESULT);
            GetResultingColorFromSpin();
        }

        public void GetResultingColorFromSpin()
        {
            if (ResultNumber == 0)
                ResultColor = GREEN_COLOR;
            else if (ResultNumber % 2 == 0)
                ResultColor = RED_COLOR;
            else
                ResultColor = BLACK_COLOR;
        }
    }
}
