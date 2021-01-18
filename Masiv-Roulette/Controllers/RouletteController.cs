﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv_Roulette.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouletteController : ControllerBase
    {
        //Initializing Casino for testing porpuses.
        private readonly Casino firstCasino = new Casino()
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
                new User("johnnatanDEV")
                {
                    ID = Guid.Parse("45317153-6fc7-431a-91a9-11f6da3e6a96"),
                    Balance = 1231
                }
            }
        };

        private readonly ILogger<RouletteController> _logger;

        public RouletteController(ILogger<RouletteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/GetAllRoulettes")]
        public IEnumerable<Roulette> GetAllRoulettes()
        {
            return firstCasino.AllRoulletes;
        }

        [HttpPost]
        [Route("/CreateRoulette")]
        public Guid CreateRoulettes()
        {
            return firstCasino.CreateCasinoSingleRoulette();
        }

        [HttpGet]
        [Route("/IsRouletteOpenById/{id}")]
        public IActionResult IsRouletteOpenById(Guid id)
        {
            if (firstCasino.IsRouletteOpenById(id))
                return Ok();
            return NoContent();
        }

        [HttpPost]
        [Route("/RouletteBetting")]
        public IActionResult RouletteBetting([FromBody] Bet bettingContent)
        {
            if (firstCasino.PlaceValidBetInCasinoRoulleteWithAuthentication(bettingContent, Request))
                return Ok();
            return NoContent();
        }

        [HttpGet]
        [Route("/ClosingRoulette/{id}")]
        public List<Bet> ClosingRoulette(Guid id)
        {
            if (firstCasino.IsRouletteOpenById(id))
                return firstCasino.GetBetsOfClosedRoulette(id);
            return new List<Bet>();
        }

    }
}
