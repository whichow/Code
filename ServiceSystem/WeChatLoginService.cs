using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class WeChatLoginService : ILoginService
    {
        public void Login(object args, Action<bool> onLogin)
        {
            Dictionary<string, string> loginArgs = args as Dictionary<string, string>;
            var id = loginArgs["Id"];
            var password = loginArgs["Password"];
            onLogin(true);
        }
    }
}
