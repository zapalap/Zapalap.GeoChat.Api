using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Messages
{
    public class OutgoingText
    {
        public string Text { get; }

        public OutgoingText(string text)
        {
            Text = text;
        }
    }
}
