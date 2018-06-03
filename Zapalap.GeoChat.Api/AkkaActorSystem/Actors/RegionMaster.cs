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
        private readonly int RegionId;

        public RegionMaster(int regionId)
        {
            RegionId = regionId;

            Receive<NewRegionUser>(RegisterNewRegionUser);
            Receive<SendText>(HandleSendText);
            Receive<ListUsers>(HandleListUsers);
        }

        private bool RegisterNewRegionUser(NewRegionUser message)
        {
            var child = Context.Child($"User:{message.UserName}");

            if (child == ActorRefs.Nobody)
            {
                Context.ActorOf(Props.Create(() => new UserWorker(message.UserName)), $"User:{message.UserName}");

                foreach (var user in Context.GetChildren())
                {
                    user.Tell(new IncomingText($"A new user has registered in our region: {message.UserName}"));
                }

                return true;
            }

            return false;
        }

        private bool HandleSendText(SendText message)
        {
            foreach (var user in Context.GetChildren())
            {
                user.Tell(new IncomingText($"[Region {RegionId}] {Context.Sender.Path.Name}: {message.Text}"));
            }

            return true;
        }

        private bool HandleListUsers(ListUsers message)
        {
            var userNames = new List<string>();

            foreach (var user in Context.GetChildren())
            {
                userNames.Add(user.Path.Name);
            }

            Sender.Tell(new IncomingUserList(userNames));
            return true;
        }
    }
}
