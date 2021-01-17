using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Masiv_Roulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Initializing Casino for testing porpuses.
        private readonly Casino firstCasino = new Casino()
        {
            ID = Guid.Parse("633ce647-409f-4409-ad9b-e9bf7b4455a2"),
            AllRoulletes = new List<Roulette>()
            {
                new Roulette() {
                    ID = Guid.Parse("268ee596-4133-451a-9f56-dc194ceb1f4c"),
                    IsRouletteOpen = false,
                    CurrentResultNumber = -1,
                    CurrentResultColor = ""
                },
                new Roulette() {
                    ID = Guid.Parse("c0282613-bca8-494c-9e2e-222c0159d115"),
                    IsRouletteOpen = true,
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

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("/CreateUser/{userName}")]
        public Guid CreateUser(string userName)
        {
            return firstCasino.CreateUserInCasino(userName);
        }

        [HttpPost]
        [Route("/AuthenticateUser")]
        public IActionResult AuthenticateUser()
        {
            if (firstCasino.AuthenticateRequest(Request))
                return Ok();
            else
                return BadRequest();
        }
    }
}
