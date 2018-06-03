using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Messages
{
    public class IncomingText
    {
        public  string Text { get; }
        public  string SenderName { get; }
        public  string Region { get; }

        public IncomingText(string text, string senderName, string region)
        {
            Text = text;
            SenderName = senderName;
            Region = region;
        }
    }
}
