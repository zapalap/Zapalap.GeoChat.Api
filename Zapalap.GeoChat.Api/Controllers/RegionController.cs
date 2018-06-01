using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zapalap.GeoChat.Api.AkkaActorSystem.Messages;

namespace Zapalap.GeoChat.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RegionsController : Controller
    {
        private readonly ActorSystem ActorSystem;

        public RegionsController(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        [HttpGet("{region}/users")]
        public async Task<IActionResult> GetUserList(int region)
        {
            var message = new ListUsers();
            var regionMaster = ActorSystem.ActorSelection($"/user/RegionMaster:{region}");

            var response = await regionMaster.Ask(message) as IncomingUserList;

            if (response == null)
                return BadRequest();

            return Ok(response.UserNames);
        }
    }
}