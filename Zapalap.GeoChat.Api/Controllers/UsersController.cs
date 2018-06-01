using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zapalap.GeoChat.Api.AkkaActorSystem.Messages;
using Zapalap.GeoChat.Api.Models;

namespace Zapalap.GeoChat.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ActorSystem ActorSystem;

        public UsersController(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        [HttpPost("regions/{region}")]
        public IActionResult PostUsers(string userName, int region)
        {
            var message = new NewRegionUser(userName);
            var selection = ActorSystem.ActorSelection($"/user/RegionMaster:{region}");
            selection.Tell(message);

            return Ok();
        }

        [HttpPost("{userName}/regions/{region}/shouts")]
        public IActionResult Shout([FromRoute]string userName, [FromRoute]int region, [FromBody]ShoutModel shout)
        {
            var message = new SendText(shout.Text);
            var selection = ActorSystem.ActorSelection($"/user/RegionMaster:{region}/User:{userName}");
            selection.Tell(message);

            return Ok();
        }
    }
}