using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv_Roulette.InitialData
{
    public static class InitialData
    {
        public static Casino GetDefaultCasino()
        {
            return new Casino()
            {
                ID = Guid.Parse("633ce647-409f-4409-ad9b-e9bf7b4455a2"),
                AllRoulletes = new List<Roulette>()
                {
                    new Roulette() {
                        ID = Guid.Parse("268ee596-4133-451a-9f56-dc194ceb1f4c"),
                        IsRouletteOpen = true,
                        CurrentResultNumber = -1,
                        CurrentResultColor = "",
                        AllBets = new List<Bet>()
                        {
                            new Bet()
                            {
                                ID = Guid.Parse("6a7bc333-be5e-450d-9016-abf1a4771410"),
                                ID_Roulette = Guid.Parse("268ee596-4133-451a-9f56-dc194ceb1f4c"),
                                ID_User = Guid.Parse("45317153-6fc7-431a-91a9-11f6da3e6a96"),
                                BetIsOnNumber = false,
                                BettingAmount = 1000,
                                BettingColor = "BLACK",
                                BettingNumber = 2,
                                IsAWinningBet = false,
                                BetClosed = false
                            }
                        }
                    },
                    new Roulette() {
                        ID = Guid.Parse("c0282613-bca8-494c-9e2e-222c0159d115"),
                        IsRouletteOpen = false,
                        CurrentResultNumber = -1,
                        CurrentResultColor = ""
                    },
                },
                AllUsers = new List<User>()
                {
                    new User()
                    {
                        ID = Guid.Parse("45317153-6fc7-431a-91a9-11f6da3e6a96"),
                        UserName = "johnnatanDEV",
                        Balance = 1231,
                        AllBets = new List<Bet>()
                        {
                            new Bet()
                            {
                                ID = Guid.Parse("6a7bc333-be5e-450d-9016-abf1a4771410"),
                                ID_Roulette = Guid.Parse("268ee596-4133-451a-9f56-dc194ceb1f4c"),
                                ID_User = Guid.Parse("45317153-6fc7-431a-91a9-11f6da3e6a96"),
                                BetIsOnNumber = false,
                                BettingAmount = 1000,
                                BettingColor = "BLACK",
                                BettingNumber = 2,
                                IsAWinningBet = false,
                                BetClosed = false
                            }
                        }
                    }
                }
            };
        }
    }
}
