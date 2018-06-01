using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Messages
{
    public class IncomingText
    {
        public  string Text { get; }

        public IncomingText(string text)
        {
            Text = text;
        }
    }
}
