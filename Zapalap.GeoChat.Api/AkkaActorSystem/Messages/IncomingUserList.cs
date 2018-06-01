using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Messages
{
    public class IncomingUserList
    {
        public IList<string> UserNames { get; }

        public IncomingUserList(IList<string> userNames)
        {
            UserNames = userNames;
        }
    }
}
