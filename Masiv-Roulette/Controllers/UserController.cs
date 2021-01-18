using Masiv_Roulette.CacheData;
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
        public CachingData cachingData = new CachingData();

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/GetAllUsers")]
        public List<User> CreateUser()
        {
            cachingData.GetCasino();
            return cachingData.CurrentCasino.AllUsers;
        }

        [HttpPost]
        [Route("/CreateUser/{userName}")]
        public Guid CreateUser(string userName)
        {
            cachingData.GetCasino();
            Guid id = cachingData.CurrentCasino.CreateUserInCasino(userName);
            cachingData.SetCasino();
            return id;
        }

        [HttpPost]
        [Route("/AuthenticateUser")]
        public IActionResult AuthenticateUser()
        {
            cachingData.GetCasino();
            if (cachingData.CurrentCasino.AuthenticateRequest(Request))
                return Ok();
            else
                return BadRequest();
        }
    }
}
