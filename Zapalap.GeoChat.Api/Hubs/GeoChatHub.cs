using Akka.Actor;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.GeoChat.Api.AkkaActorSystem.Messages;

namespace Zapalap.GeoChat.Api.Hubs
{
    public class GeoChatHub : Hub
    {
        private readonly ActorSystem ActorSystem;

        public GeoChatHub(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        public async Task SendMessage(string text, string userName, string region)
        {
            var message = new SendText(text);
            var selection = ActorSystem.ActorSelection($"/user/RegionMaster:{region}/User:{userName}");
            selection.Tell(message);
        }

        public async Task JoinRegion(string userName, string region)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"RegionMaster:{region}/User:{userName}");

            var message = new NewRegionUser(userName);
            var selection = ActorSystem.ActorSelection($"/user/RegionMaster:{region}");
            selection.Tell(message);
        }
    }
}
