using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Messages
{
    public class SendText
    {
        public string Text { get; }

        public SendText(string text)
        {
            Text = text;
        }
    }
}
