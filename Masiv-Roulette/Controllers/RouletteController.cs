using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Masiv_Roulette.CacheData;

namespace Masiv_Roulette.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouletteController : ControllerBase
    {
        public CachingData cachingData = new CachingData();

        private readonly ILogger<RouletteController> _logger;

        public RouletteController(ILogger<RouletteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/GetAllRoulettes")]
        public IEnumerable<Roulette> GetAllRoulettes()
        {
            cachingData.GetCasino();
            return cachingData.CurrentCasino.AllRoulletes;
        }

        [HttpPost]
        [Route("/CreateRoulette")]
        public Guid CreateRoulettes()
        {
            cachingData.GetCasino();
            Guid id = cachingData.CurrentCasino.CreateCasinoSingleRoulette();
            cachingData.SetCasino();
            return id;
        }

        [HttpGet]
        [Route("/IsRouletteOpenById/{id}")]
        public IActionResult IsRouletteOpenById(Guid id)
        {
            cachingData.GetCasino();
            if (cachingData.CurrentCasino.IsRouletteOpenById(id))
                return Ok();
            return NoContent();
        }

        [HttpPost]
        [Route("/RouletteBetting")]
        public IActionResult RouletteBetting([FromBody] Bet bettingContent)
        {
            cachingData.GetCasino();
            if (cachingData.CurrentCasino.PlaceValidBetInCasinoRoulleteWithAuthentication(bettingContent, Request))
            {
                cachingData.SetCasino();
                return Ok();
            }
            return NoContent();
        }

        [HttpGet]
        [Route("/ClosingRoulette/{id}")]
        public List<Bet> ClosingRoulette(Guid id)
        {
            cachingData.GetCasino();
            if (cachingData.CurrentCasino.IsRouletteOpenById(id))
            {
                cachingData.SetCasino();
                return cachingData.CurrentCasino.GetBetsOfClosedRoulette(id);
            }
            return new List<Bet>();
        }

    }
}
