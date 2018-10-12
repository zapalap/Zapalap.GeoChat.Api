using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zapalap.GeoChat.Api.AkkaActorSystem.Messages
{
    public class NewRegionUser
    {
        public string UserName { get; }
        public bool Silent { get; }

        public NewRegionUser(string userName, bool silent = false)
        {
            UserName = userName;
            Silent = silent;
        }
    }
}
