using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Messages
{
    public class NewRegionUser
    {
        public string UserName { get; }

        public NewRegionUser(string userName)
        {
            UserName = userName;
        }
    }
}
