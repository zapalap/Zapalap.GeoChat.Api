using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.GeoChat.Api.AkkaActorSystem.Messages;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Actors
{
    public class UserWorker : ReceiveActor
    {
        private readonly string userName;

        public UserWorker(string userName)
        {
            this.userName = userName;

            Receive<IncomingText>(HandleIncomingText);
            Receive<SendText>(HandleSendText);
        }

        private bool HandleSendText(SendText message)
        {
            Context.Parent.Tell(message);
            return true;
        }

        private bool HandleIncomingText(IncomingText message)
        {
            Debug.WriteLine($"{userName}: Received -> {message.Text}");
            return true;
        }
    }
}
