﻿using Akka.Actor;
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

        public RegionMaster(string region)
        {
            Region = region;

            Receive<NewRegionUser>(RegisterNewRegionUser);
            Receive<SendText>(HandleSendText);
        }

        private bool RegisterNewRegionUser(NewRegionUser message)
        {
            var child = Context.Child($"User:{message.UserName}");

            if (child == ActorRefs.Nobody)
            {
                Context.ActorOf(Props.Create(() => new UserWorker(message.UserName)), $"User:{message.UserName}");

                foreach (var user in Context.GetChildren())
                {
                    user.Tell(new IncomingText($"A new user has registered in our region - {message.UserName}", $"{Region} Master", Region));
                }

                return true;
            }

            return false;
        }

        private bool HandleSendText(SendText message)
        {
            foreach (var user in Context.GetChildren())
            {
                user.Tell(new IncomingText(message.Text, Context.Sender.Path.Name.Split(':').Last(), Region));
            }

            return true;
        }
    }
}
