using Akka.Actor;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.GeoChat.Api.AkkaActorSystem.Messages;
using Zapalap.GeoChat.Api.Hubs;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Actors
{
    public class UserWorker : ReceiveActor
    {
        private readonly string userName;
        private IHubContext<GeoChatHub> HubContext;

        protected override void PreStart()
        {
            HubContext = Startup
                .ServiceProvider
                .GetService(typeof(IHubContext<GeoChatHub>)) as IHubContext<GeoChatHub>;
        }

        public UserWorker(string userName)
        {
            this.userName = userName;

            Receive<IncomingText>(async f => await HandleIncomingText(f));
            Receive<SendText>(HandleSendText);
        }

        private bool HandleSendText(SendText message)
        {
            Context.Parent.Tell(message);
            return true;
        }

        private async Task<bool> HandleIncomingText(IncomingText message)
        {
            await HubContext
                .Clients
                .Group($"{Context.Parent.Path.Name}/{Context.Self.Path.Name}")
                .SendAsync("IncomingMessage", message.Text, message.SenderName, message.Region);

            return true;
        }
    }
}
