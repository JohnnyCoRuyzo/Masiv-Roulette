using Microsoft.AspNetCore.Mvc;
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
                new Roulette(){
                    ID = Guid.Parse("268ee596-4133-451a-9f56-dc194ceb1f4c"),
                    IsRouletteOpen = false,
                    ResultNumber = -1,
                    ResultColor = ""
                },
                new Roulette(){
                    ID = Guid.Parse("c0282613-bca8-494c-9e2e-222c0159d115"),
                    IsRouletteOpen = true,
                    ResultNumber = -1,
                    ResultColor = ""
                },
            }
        };

        private readonly ILogger<RouletteController> _logger;

        public RouletteController(ILogger<RouletteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Roulette> Get()
        {
            return firstCasino.AllRoulletes;
        }

        [HttpGet]
        [Route("/IsRouletteOpenById/{id}")]
        public IActionResult IsRouletteOpenById(Guid id)
        {
            if (firstCasino.RoulleteExists(id))
            {
                Roulette roullete = firstCasino.GetRouletteById(id);
                if (roullete.IsRouletteOpen)
                    return Ok();
            }
            return NoContent();
        }

    }
}
