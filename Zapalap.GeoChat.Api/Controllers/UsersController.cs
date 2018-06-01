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
    public class UsersController : Controller
    {
        private readonly ActorSystem ActorSystem;

        public UsersController(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        [HttpPost]
        [Route("regions/{region}")]
        public IActionResult PostUsers(string userName, int region)
        {
            var message = new NewRegionUser(userName);
            var selection = ActorSystem.ActorSelection($"/user/RegionMaster:{region}");
            selection.Tell(message);

            return Ok();
        }

        [HttpPost]
        [Route("{userName}/regions/{region}/messages/{text}")]
        public IActionResult PostMessage(string userName, int region, [FromBody]string text)
        {
            var message = new SendText(text);
            var selection = ActorSystem.ActorSelection($"/user/RegionMaster:{region}/User:{userName}");
            selection.Tell(message);

            return Ok();
        }
    }
}