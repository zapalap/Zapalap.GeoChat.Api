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
        public  int RegionId { get; }

        public IncomingText(string text, string senderName, int regionId)
        {
            Text = text;
            SenderName = senderName;
            RegionId = regionId;
        }
    }
}
