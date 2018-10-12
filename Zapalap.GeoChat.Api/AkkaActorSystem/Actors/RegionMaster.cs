using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.GeoChat.Api.AkkaActorSystem.Messages;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Actors
{
    public class RegionMaster : ReceiveActor
    {
        private readonly string Region;
        private int ActiveUsers = 0;

        public RegionMaster(string region)
        {
            Region = region;

            Receive<NewRegionUser>(RegisterNewRegionUser);
            Receive<OutgoingText>(HandleOutgoingText);
        }

        private bool RegisterNewRegionUser(NewRegionUser message)
        {
            var child = Context.Child($"User:{message.UserName}");

            if (child == ActorRefs.Nobody)
            {
                Context.ActorOf(Props.Create(() => new UserWorker(message.UserName)), $"User:{message.UserName}");

                ActiveUsers++;

                foreach (var user in Context.GetChildren())
                {
                    user.Tell(new IncomingText($"A new user has registered in our region - {message.UserName}", $"{Region} Master", Region));
                }

                return true;
            }

            return false;
        }

        private bool HandleOutgoingText(OutgoingText message)
        {
            if (HandleCommand(message))
                return true;

            foreach (var user in Context.GetChildren())
            {
                user.Tell(new IncomingText(message.Text, Context.Sender.Path.Name.Split(':').Last(), Region));
            }

            return true;
        }

        private bool HandleCommand(OutgoingText message)
        {
            if (message.Text.StartsWith("/countusers"))
            {
                Context.Sender.Tell(new IncomingText($"Currently there are {ActiveUsers} active users in {Region}", $"{Region} Master", Region));
                return true;
            }

            return false;
        }
    }
}
